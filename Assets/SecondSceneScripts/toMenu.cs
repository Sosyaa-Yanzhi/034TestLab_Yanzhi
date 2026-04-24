using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class toMenu : MonoBehaviour
{
    [Header("返回主菜单UI")]
    public Image backToMenuImage;
    public Button confirmButton;
    public Button cancelButton;

    private bool isUICalled = false;

    void Start()
    {
        backToMenuImage.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isUICalled = false;
        confirmButton.onClick.AddListener(OnConfirmClick);
        cancelButton.onClick.AddListener(OnCancelClick);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            {
                isUICalled = !isUICalled;
                if (isUICalled)
                {
                    // 暂停游戏
                    Time.timeScale = 0f;
                    backToMenuImage.gameObject.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1f;
                    backToMenuImage.gameObject.SetActive(false);
                }
            }
    }

    void OnConfirmClick()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1f;
    }

    void OnCancelClick()
    {
        backToMenuImage.gameObject.SetActive(false);
        isUICalled = false;
        Time.timeScale = 1f;
    }
}
