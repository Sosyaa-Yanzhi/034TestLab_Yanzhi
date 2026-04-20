using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Video;

[System.Serializable]
public class Data
{
    public int FirstSceneScore = 0;
    public int SecondSceneScore = 0;
    public int ThirdSceneScore = 0;

    public Data(int firstSceneScore , int secondSceneScore , int thirdSceneScore)
    {
        this.FirstSceneScore = firstSceneScore;
        this.SecondSceneScore = secondSceneScore;
        this.ThirdSceneScore = thirdSceneScore;
    }
}


public class GameData : MonoBehaviour
{
    // 单例实例
    public static GameData Instance {get; private set;}
    private int firstSceneScore = 0;
    private int secondSceneScore = 0;
    private int thirdSceneScore = 0;
    // 保存文件名称
    private string saveFileName = "gamesave.json";
    void Awake()
    {
        // 单例初始化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // 获取完整的文件保存路径
    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath , saveFileName);
    }

    // 保存数据
    public void SaveData()
    {
        // 创建数据对象
        Data data = new Data(firstSceneScore , secondSceneScore , thirdSceneScore);
        // 将对象转为Json字符串
        string jsonString = JsonUtility.ToJson(data , true); // true表示格式化输出，更为美观
        // 获取保存路径
        string savePath = GetSavePath();
        // 写入文件
        try
        {
            File.WriteAllText(savePath , jsonString);
        }
        catch (System.Exception e)
        {
            Debug.Log($"保存失败！{e.Message}");
        }
    }

    // 加载数据
    public void LoUpdateata()
    {
        string savePath = GetSavePath();
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Data data = JsonUtility.FromJson<Data>(json);
            ApplyData(data);
        }
        else
        {
            firstSceneScore = 0;
            secondSceneScore = 0;
            thirdSceneScore = 0;
        }
    }

    void ApplyData(Data Data)
    {
        firstSceneScore = Data.FirstSceneScore;
        secondSceneScore = Data.SecondSceneScore;
        thirdSceneScore = Data.ThirdSceneScore;
    }

    #region 公开接口
    public void UpdateFirstSceneScore(int newScore)
    {
        firstSceneScore = newScore;
        SaveData();
    }
    public void UpdateSecondSceneScore(int newScore)
    {
        secondSceneScore = newScore;
        SaveData();
    }
    public void UpdateThirdSceneScore(int newScore)
    {
        thirdSceneScore = newScore;
        SaveData();
    }
    public int GetFirstSceneScore() => firstSceneScore;
    public int GetSecondSceneScore() => secondSceneScore;
    public int GetThirdSceneScore() => thirdSceneScore;
    #endregion

    void Start()
    {
        LoUpdateata();
    }
}