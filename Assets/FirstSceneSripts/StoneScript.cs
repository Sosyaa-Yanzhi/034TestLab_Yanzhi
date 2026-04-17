using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoneScript : MonoBehaviour
{
    [Header("transform信息")]
    public float ResPos_X;
    public float ResPos_Y;
    public float rotationSpeed;
    public int rotationDirection; // 1 or -1
    public float speed;
    public GameObject stoneObj;
    public EventSystemScript eventSystem;
    public GameObject eventObject;
    [Header("警示标识")]
    public GameObject warningMark;
    private bool disappear = true;
    void Awake()
    {
        eventObject = GameObject.Find("EventSystem");
        eventSystem = eventObject.GetComponent<EventSystemScript>();
        warningMark.SetActive(false);
        disappear = true;
    }
    void OnEnable()
    {
        ResPos_X = Random.Range(-8.6f , 8.87f);
        ResPos_Y = Random.Range(6.23f , 7.84f);
        rotationSpeed = Random.Range(10f , 50f);
        rotationDirection = Random.Range(0 , 2) * 2 - 1;
        transform.position = new Vector3(ResPos_X , ResPos_Y , 0f);
        speed = 10f;
        warningMark.SetActive(true);
        warningMark.transform.position = new Vector3(gameObject.transform.position.x , 5f , 0f);
        disappear = false;
        Invoke("Update" , 1f);
    }

    void Update()
    {
        warningMark.transform.position = new Vector3(gameObject.transform.position.x , 4f , 0f);
        // if (!disappear) return;
        transform.position -= transform.up * speed * Time.deltaTime;
        stoneObj.transform.Rotate(0 , 0 , rotationDirection * Time.deltaTime * rotationSpeed);
        if (transform.position.y <= - 10f)
        {
            Destroy();
        }
    }

    void Destroy()
    {
        GameObjectPool.Instance.ReturnObject(gameObject , eventSystem.objectPool);
    }

    void CanDisappear()
    {
        disappear = true;
    }
}
