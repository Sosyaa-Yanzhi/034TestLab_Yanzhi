using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    // 单例实例
    public static GameObjectPool Instance { get; private set; }

    private void Awake()
    {
        // 设置单例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景时不摧毁
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreatePool(int objectNumber , GameObject newGameObject , Queue<GameObject> objectPool)
    {
        for (int i = 0 ; i < objectNumber ; i++)
        {
            GameObject newObject = Instantiate(newGameObject);
            newObject.SetActive(false);
            objectPool.Enqueue(newObject);
        }
    }

    public GameObject GetObject(Queue<GameObject> objectPool , GameObject prefabObject)
    {
        if (objectPool.Count > 0)
        {
            GameObject newObject = objectPool.Dequeue();
            newObject.SetActive(true);
            return newObject;
        }
        
        GameObject newObject_1 = Instantiate(prefabObject);
        return newObject_1;
    }

    public void ReturnObject(GameObject prefabObject , Queue<GameObject> objectPool)
    {
        prefabObject.SetActive(false);
        objectPool.Enqueue(prefabObject);
    }

    
}
