using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventSystemScript : MonoBehaviour
{
    [Header("要生成的对象")]
    public GameObject prefabObject;
    [Header("生成数量")]
    public int createNumber;
    [Header("队列")]
    public Queue<GameObject> objectPool = new Queue<GameObject>();
    [Header("陨石生成计时器")]
    public float timer = 0f;
    public float targetTime;
    void Start()
    {
        // 创建对象池
        GameObjectPool.Instance.CreatePool(createNumber , prefabObject , objectPool);
        timer = 0f;
        targetTime = Random.Range(3f , 5f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        CreateStone();
    }

    void CreateStone()
    {
        if (timer < targetTime) return;
        GameObject stone = GameObjectPool.Instance.GetObject(objectPool , prefabObject);
        timer = 0f;
    }
}
