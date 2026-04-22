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

    void Start()
    {
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
            if (Timer[i] >= CoolTime[i])
            {
                Instantiate(E_prefab);
                Timer[i] = 0f;
            }
        }
    }
}
