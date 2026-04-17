using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PrefabsRespwanScript : MonoBehaviour
{
    [Header("要实例化的预制体")]
    public GameObject planetPrefab;   // 行星预制体
    public GameObject starPrefab;   // 星星预制体（背景美化用）
    public float planetResTime = 5f;   // 行星预制体生成间隔时间
    public float starResTime = 0.5f;    // 星星预制体生成间隔时间

    [Header("场景切换遮挡")]
    public GameObject Cover;

    void Awake()
    {
        Cover = GameObject.Find("Cover");  // 获取遮盖物体
        Cover.GetComponent<SpriteRenderer>().DOFade(0f , 0f);  // 设置遮盖物初始透明度
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("游戏开始！");

        InvokeRepeating(nameof(PlanetInstantiate), 0f , planetResTime);  // 间隔一定时间生成行星

        InvokeRepeating(nameof(StarInstantiate) , 0f , starResTime);  // 间隔一定时间生成星星
        InvokeRepeating(nameof(StarInstantiate_1), 0f, starResTime + 0.1f);  // 间隔一定时间生成星星

        InvokeRepeating("SpeedUp", 0f, 5f);  // 每隔五秒进行一次加速

        Invoke("ChangeScene" , 30f);   // 30s 计时结束后，屏幕变黑，切换到3D场景
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlanetInstantiate()
    {
        GameObject planet = Instantiate(planetPrefab);
    }

    void StarInstantiate()
    {
        GameObject star = Instantiate(starPrefab);
    }

    void StarInstantiate_1()
    {
        GameObject star_1 = Instantiate(starPrefab);
    }

    void SpeedUp()
    {
        PhysicalConstants.starMoveSpeed += 10f;
    }

    void ChangeScene()
    {
        Cover.GetComponent<SpriteRenderer>().DOFade(1f , 1f);  // 遮盖物淡入
        // 遮盖物淡入动画完成后，切换场景
        DOVirtual.DelayedCall(1.5f , () =>
        {
            SceneManager.LoadScene("3DScene");   // 切换场景
        });
    }
}
