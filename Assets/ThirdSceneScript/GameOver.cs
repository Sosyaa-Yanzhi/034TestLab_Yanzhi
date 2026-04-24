using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameOver : MonoBehaviour
{
    [Header("引用脚本")]
    public JetControlScript jetControlScript;
    [Header("计时器")]
    public float timer;
    [Header("失败UI")]
    public Image coverImage;
    private CanvasGroup imageCanvasGroup;
    [Header("返回主菜单UI")]
    public Image backToMenuImage;
    public Button confirmButton;
    public Text timerText;

    void Start()
    {
        coverImage.gameObject.SetActive(false);
        backToMenuImage.gameObject.SetActive(false);
        imageCanvasGroup = coverImage.GetComponent<CanvasGroup>();
        imageCanvasGroup.DOFade(0f , 0f);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
    }
    void Update()
    {
        if (jetControlScript != null)
        {
            if (jetControlScript.hp <= 0)
            {
                // 飞船被击毁后触发失败界面
                GameOverFun();
                timer += Time.deltaTime; // 开始计时
                if (timer >= 7f)
                {
                    SceneManager.LoadScene("StartScene");  // 计时结束后自动返回主菜单
                }
            }
        }
    }

    void GameOverFun()
    {
        coverImage.gameObject.SetActive(true);
        imageCanvasGroup.DOFade(1f , 2f);
        DOVirtual.DelayedCall(2f , () =>
        {
            backToMenuImage.gameObject.SetActive(true);
            UpdateTimerText();
        });
    }

    void OnConfirmButtonClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    void UpdateTimerText()
    {
        timerText.text = (timer - 2f).ToString();
    }
}
