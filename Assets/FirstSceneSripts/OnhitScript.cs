using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnhitScript : MonoBehaviour
{
    [Header("受击反馈")]
    public GameObject hitEffection;
    public GameObject mainCamera;
    private float animationDuration = 0.5f;
    private float maxOpacity = 0.5f;
    [Header("生命值")]
    public int health = 100;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        hitEffection.SetActive(false);
        health = 100;
    }

    void Update()
    {
        if (health <= 0)
        {
            Lab_1_EventsManager.Instance.GameOver(true);
            Debug.Log("Spacecraft explosed!!!");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stone"))
        {
            Debug.Log("被陨石击中！");
            StartCoroutine(PlayAnimation());
            health -= 20;
        }
        if (collision.CompareTag("Planet"))
        {
            Lab_1_EventsManager.Instance.GameOver(true);
            Debug.Log("Spacecraft explosed!!!(Hit on planet)");
        }
    }

    IEnumerator PlayAnimation()
    {
        mainCamera.GetComponent<Transform>().DOShakePosition(animationDuration * 2 , 3f);
        hitEffection.SetActive(true);
        hitEffection.GetComponent<SpriteRenderer>().DOFade(maxOpacity , animationDuration);
        yield return new WaitForSeconds(animationDuration);
        hitEffection.GetComponent<SpriteRenderer>().DOFade(0f , animationDuration);
        yield return new WaitForSeconds(animationDuration);
        hitEffection.SetActive(false);
        yield return null;
    }
}
