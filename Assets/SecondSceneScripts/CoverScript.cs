using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoverScript : MonoBehaviour
{
    [Header("遮盖UI")]
    public GameObject Cover; // 于inspector中进行赋值

    // Start is called before the first frame update
    void Start()
    {
        Cover.gameObject.SetActive(true);
        // 设置初始透明度
        Cover.GetComponent<CanvasGroup>().DOFade(1f , 0f);
        DOVirtual.DelayedCall(1f , () =>
        {
            // 场景切换到此场景后，播放淡出动画
            Cover.GetComponent<CanvasGroup>().DOFade(0f, 1.5f);
            DOVirtual.DelayedCall(1.5f, () =>
            {
                Cover.gameObject.SetActive(false);
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
