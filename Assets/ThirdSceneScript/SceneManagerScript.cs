using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public int CreateNumber;
    public Queue<GameObject> cloudPool;
    public GameObject cloudPrefab;
    public float Timer;
    public float coolTime;
    void Start()
    {
        // 设置场景编号为2
        Data.SceneCode = 2;
        // 队列初始化
        cloudPool = new Queue<GameObject>();
        // 初始化计时器
        Timer = 0f;
        // 初始化冷却时间
        coolTime = Random.Range(1f , 5f);
        // 建立云朵对象池
        CreateCloudPool();
    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= coolTime)
        {
            GameObject cloud = GetCloud();
            Timer = 0f;
            coolTime = Random.Range(1f , 5f);
        }
    }

    #region 建立云朵对象池
    public void CreateCloudPool()
    {
        Debug.Log("建立云朵对象池！");
        for (int i = 0 ; i < CreateNumber ; i++)
        {
            GameObject cloud = Instantiate(cloudPrefab);
            cloud.SetActive(false);
            cloudPool.Enqueue(cloud);
        }
    }
    #endregion

    #region 生成云朵
    public GameObject GetCloud()
    {
        if (cloudPool.Count > 0)
        {
            GameObject cloud = cloudPool.Dequeue();
            cloud.SetActive(true);
            return cloud; 
        }

        GameObject newcloud = Instantiate(cloudPrefab);
        return newcloud;
    }
    #endregion

    #region 回收云朵
    public void ReturnCloud(GameObject cloud , Queue<GameObject> cloudPool)
    {
        cloud.SetActive(false);
        cloudPool.Enqueue(cloud);
    }
    #endregion
}
