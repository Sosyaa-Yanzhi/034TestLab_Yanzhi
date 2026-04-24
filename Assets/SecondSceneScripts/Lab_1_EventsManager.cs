using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab_1_EventsManager : MonoBehaviour
{
    // 创建单例实例
    public static Lab_1_EventsManager Instance {get; private set;}
    public bool isGameOver = false;

    void Awake()
    {
        isGameOver = false;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver(bool state)
    {
        isGameOver = state;
    }
    public bool GetGameState() => isGameOver;
}
