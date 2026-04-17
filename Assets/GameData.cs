using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class Data
{
    static public int SceneCode = 0;
    static public int FirstSceneScore = 0;
    public int ThirdSceneScore = 0;
}


public class GameData : MonoBehaviour
{
    public static int Score = 0;
    // 保存文件名称
    private string saveFileName = "gamesave.json";
    // 获取完整的文件保存路径
    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath , saveFileName);
    }

    // 保存数据
    public void SaveData()
    {
        // 创建数据对象
        Data data = new Data();
        data.ThirdSceneScore = Score;
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
    public int LoadData()
    {
        string savePath = GetSavePath();

        // 检查文件路径是否存在
        if (File.Exists(savePath))
        {
            try
            {
                // 读取Json字符串
                string jsonString = File.ReadAllText(savePath);
                // 将Json字符串转换为对象
                Data data = JsonUtility.FromJson<Data>(jsonString);
                // 返回分数
                return data.ThirdSceneScore;
            }
            catch (System.Exception e)
            {
                Debug.Log($"加载失败：{e.Message}");
                return 0;
            }
        }
        else
        {
            // 如果文档不存在，则返回默认值
            return 0;
        }
    }

    void Start()
    {
        Score = LoadData();
    }
}