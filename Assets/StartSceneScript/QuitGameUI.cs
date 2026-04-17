using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class QuitGameUI : MonoBehaviour
{
    [Header("背景UI")]
    public GameObject bg_1;
    public Vector3 originPosition_1 = new Vector3(-0.85f , 1.76f , 1.79f);
    public Vector3 targetPosition_1 = new Vector3(-0.2f , 0.61f , 1.4f);
    public GameObject bg_2;
    public Vector3 originPosition_2 = new Vector3(2.4f , 1.7f , -0.16f);
    public Vector3 targetPosition_2 = new Vector3(1.94f , 0.61f , 0.12f);
    public GameObject bg_3;
    public Vector3 originPosition_3 = new Vector3(2.4f , -2.5f , -0.1f);
    public Vector3 targetPosition_3 = new Vector3(1.94f , -1.8f , 0.12f);
    public GameObject bg_4;
    public Vector3 originPosition_4 = new Vector3(-0.9f , -2.4f , 1.8f);
    public Vector3 targetPosition_4 = new Vector3(-0.2f , -1.8f , 1.4f);

    [Header("按钮控件")]
    public Button confirmButton;
    public Button cancelButton;

    void Start()
    {
        // 设置背景UI初始透明度
        bg_1.GetComponent<CanvasGroup>().alpha = 0f;
        bg_2.GetComponent<CanvasGroup>().alpha = 0f;
        bg_3.GetComponent<CanvasGroup>().alpha = 0f;
        bg_4.GetComponent<CanvasGroup>().alpha = 0f;
        // 设置按钮初始透明度
        confirmButton.GetComponent<CanvasGroup>().alpha = 0f;
        cancelButton.GetComponent<CanvasGroup>().alpha = 0f;
        // 设置背景UI初始位置
        bg_1.GetComponent<RectTransform>().anchoredPosition = originPosition_1;
        bg_2.GetComponent<RectTransform>().anchoredPosition = originPosition_2;
        bg_3.GetComponent<RectTransform>().anchoredPosition = originPosition_3;
        bg_4.GetComponent<RectTransform>().anchoredPosition = originPosition_4;

        // 为按钮添加监听器
        confirmButton.onClick.AddListener(ConfirmButton_click);
        cancelButton.onClick.AddListener(CancelButton_click);
    }

    void Update()
    {
        onButtonHover(confirmButton);
        onButtonHover(cancelButton);
    }

    bool IsPointerOverButton(Button button)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData , results);
        foreach (var result in results)
        {
            if (result.gameObject == button.gameObject)
                return true;
        }
        return false;
    }
    void onButtonHover(Button button)
    {
        Vector3 currentPos = new Vector3();
        Vector3 targetPos = currentPos + new Vector3(0f , 0f , 1f);
        if (IsPointerOverButton(button))
        {
            button.GetComponent<RectTransform>().DOScale(0.9f , 0.2f);
        }
        else
        {
            button.GetComponent<RectTransform>().DOScale(1f , 0.2f);
        }
    }

    public IEnumerator UIAppearAnimation()
    {
        // 设置背景UI初始透明度
        bg_1.GetComponent<CanvasGroup>().alpha = 0f;
        bg_2.GetComponent<CanvasGroup>().alpha = 0f;
        bg_3.GetComponent<CanvasGroup>().alpha = 0f;
        bg_4.GetComponent<CanvasGroup>().alpha = 0f;
        // 设置按钮初始透明度
        confirmButton.GetComponent<CanvasGroup>().alpha = 0f;
        cancelButton.GetComponent<CanvasGroup>().alpha = 0f;
        // 设置背景UI初始位置
        bg_1.GetComponent<RectTransform>().anchoredPosition = originPosition_1;
        bg_2.GetComponent<RectTransform>().anchoredPosition = originPosition_2;
        bg_3.GetComponent<RectTransform>().anchoredPosition = originPosition_3;
        bg_4.GetComponent<RectTransform>().anchoredPosition = originPosition_4;
        // 背景UI移动到目标位置
        bg_1.GetComponent<RectTransform>().DOAnchorPos(targetPosition_1 , 0.5f);
        bg_2.GetComponent<RectTransform>().DOAnchorPos(targetPosition_2 , 0.5f);
        bg_3.GetComponent<RectTransform>().DOAnchorPos(targetPosition_3 , 0.5f);
        bg_4.GetComponent<RectTransform>().DOAnchorPos(targetPosition_4 , 0.5f);
        // 背景UI淡入
        bg_1.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
        bg_2.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
        bg_3.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
        bg_4.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);

        // 按钮淡入
        DOVirtual.DelayedCall(0.5f , () =>
        {
            confirmButton.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
            cancelButton.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
        });

        yield return null;
    }

    public IEnumerator UIDisappearAnimation()
    {
        // 背景UI移动回初始位置
        bg_1.GetComponent<RectTransform>().DOAnchorPos(originPosition_1 , 0.5f);
        bg_2.GetComponent<RectTransform>().DOAnchorPos(originPosition_2 , 0.5f);
        bg_3.GetComponent<RectTransform>().DOAnchorPos(originPosition_3 , 0.5f);
        bg_4.GetComponent<RectTransform>().DOAnchorPos(originPosition_4 , 0.5f);
        // 背景UI淡出
        bg_1.GetComponent<CanvasGroup>().DOFade(0f , 0.5f);
        bg_2.GetComponent<CanvasGroup>().DOFade(0f , 0.5f);
        bg_3.GetComponent<CanvasGroup>().DOFade(0f , 0.5f);
        bg_4.GetComponent<CanvasGroup>().DOFade(0f , 0.5f);

        confirmButton.GetComponent<CanvasGroup>().DOFade(0f , 0.1f);
        cancelButton.GetComponent<CanvasGroup>().DOFade(0f , 0.1f);

        yield return null;
    }

    void ConfirmButton_click()
    {
        // 退出游戏
        Application.Quit();
    }

    void CancelButton_click()
    {
        StartCoroutine(UIDisappearAnimation());
    }
}
