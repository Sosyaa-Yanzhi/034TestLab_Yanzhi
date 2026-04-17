using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public float floatSpeed;
    public int layerIndex;
    public float opacity;
    public float positionY;
    public float positionX;
    public float radius;
    public SceneManagerScript sceneManagerScript;
    public GameObject sceneManager;
    void Start()
    {
        // 随机获取参数值
        floatSpeed = Random.Range(5f , 15f);
        opacity = Random.Range(0.1f , 1f);
        layerIndex = Random.Range(1 , 5);
        positionY = Random.Range(-4.3f , 4.2f);
        positionX = Random.Range(13f , 19f);
        radius = Random.Range(1.77f , 6f);

        // 设置属性
        GetComponent<SpriteRenderer>().sortingOrder = layerIndex;
        GetComponent<SpriteRenderer>().color = new Color(1f , 1f , 1f , opacity);
        GetComponent<Transform>().localScale = new Vector3(radius , radius , radius);
        transform.position = new Vector3(positionX , positionY , 0f);

        // 获取管理脚本
        sceneManager = GameObject.Find("CloudManager");
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
    }
    void Update()
    {
        // 云飘动
        transform.position -= transform.right * floatSpeed * Time.deltaTime;
        // 销毁
        CloudDestroy();
    }

    #region 销毁云朵
    void CloudDestroy()
    {
        if (transform.position.x <= -15f)
        {
            transform.position = new Vector3(positionX , positionY , 0f);
            sceneManagerScript.ReturnCloud(gameObject , sceneManagerScript.cloudPool);
        }
    }
    #endregion
}
