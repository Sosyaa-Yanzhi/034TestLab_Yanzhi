using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject E_prefab;
    public List<Queue<GameObject>> EnemyPoolList = new List<Queue<GameObject>>();
    public int createNumber = 5;
    public int poolNumber = 3;
    public List<float> CoolTime = new List<float>();
    public List<float> Timer = new List<float>();
    public int enemyCount = 0;

    void Start()
    {
        // 创建对象池
        CreatePool();
        // 初始化时间数组
        for (int i = 0 ; i < poolNumber ; i++)
        {
            float coolTime = Random.Range(1f , 3f);
            CoolTime.Add(coolTime);
            float timer = 0f;
            Timer.Add(timer);
        }
    }

    void Update()
    {
        // 生成敌人
        for (int i = 0 ; i < poolNumber ; i++)
        {
            Timer[i] += Time.deltaTime;
            if (Timer[i] >= CoolTime[i] && enemyCount <= 5)
            {
                GetEnemy(EnemyPoolList[i]);
                enemyCount++;
                Timer[i] = 0f;
            }
        }
    }

    #region 创建敌人对象池
    void CreatePool()
    {
        // 创建敌人对象池
        for (int i = 0 ; i < poolNumber ; i++)
        {
            Queue<GameObject> EnemyPool = new Queue<GameObject>();
            EnemyPoolList.Add(EnemyPool);
            for (int j = 0 ; j < createNumber ; j++)
            {
                GameObject newE_prefab = Instantiate(E_prefab);
                newE_prefab.GetComponent<Text>().text = i.ToString();
                newE_prefab.SetActive(false);
                EnemyPool.Enqueue(newE_prefab);
            }
        }
    }
    #endregion

    #region 从对象池取物
    public GameObject GetEnemy(Queue<GameObject> EnemyPool)
    {
        if (EnemyPool.Count > 0)
        {
            GameObject enemy = EnemyPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }


        GameObject newEnemy = Instantiate(E_prefab);
        newEnemy.SetActive(true);
        return newEnemy;
    }
    #endregion

    #region 归还对象
    public void ReturnEnemy(string poolIndex , GameObject enemy)
    {
        int i = int.Parse(poolIndex);
        enemy.SetActive(false);
        EnemyPoolList[i].Enqueue(enemy);
        enemyCount--;
    }
    #endregion
}
