using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [Header("左栏主控件")]
    public Button StartGameButton;
    public bool isGameStarted = false;
    public Button SettingsButton;
    public bool isSettingCalled = false;
    public Button QuitGameButton;
    public bool isQuitCalled = false;

    [Header("右侧功能控件")]
    [Header("关卡选择控件")]
    public GameObject LabSelection;

    [Header("设置控件")]
    // 设置控件引用

    [Header("退出游戏控件")]
    public GameObject QuitGame;

    [Header("控件脚本引用")]
    public SceneSelectionScript sceneSelectionScript;
    public QuitGameUI quitGameUI;

    void Start()
    {
        // 设置右侧功能控件初始状态
        LabSelection.gameObject.SetActive(false);
        QuitGame.gameObject.SetActive(false);
        
        // 为按键添加监听器
        StartGameButton.onClick.AddListener(StartGameButton_click);
        // 设置按键 - 监听器
        QuitGameButton.onClick.AddListener(QuitGameButton_click);
        // Json测试
        Debug.Log($"第二关卡得分纪录：{GameData.Instance.GetSecondSceneScore()}");
    }

    void StartGameButton_click()
    {
        isGameStarted = !isGameStarted;
        isQuitCalled = false;
        isSettingCalled = false;
        // 当此按键被按下，隐藏其他功能的UI
        QuitGame.gameObject.SetActive(false);
        // 隐藏设置界面的UI

        LabSelection.gameObject.SetActive(true);
        // 播放UI出现动画
        if (isGameStarted) StartCoroutine(sceneSelectionScript.UIAppearAnimation());
        else
        {
            StartCoroutine(sceneSelectionScript.UIDisappearAnimation());
            DOVirtual.DelayedCall(0.5f , () =>
            {
                LabSelection.gameObject.SetActive(false);
            });
        }
    }

    void QuitGameButton_click()
    {
        isQuitCalled = !isQuitCalled;
        isGameStarted = false;
        isSettingCalled = false;
        LabSelection.gameObject.SetActive(false);
        // 隐藏设置界面UI

        QuitGame.gameObject.SetActive(true);
        // 播放UI出现动画
        if (isQuitCalled) StartCoroutine(quitGameUI.UIAppearAnimation());
        else
        {
            StartCoroutine(quitGameUI.UIDisappearAnimation());
            DOVirtual.DelayedCall(0.5f , () =>
            {
                QuitGame.gameObject.SetActive(false);
            });
        }
    }
}
