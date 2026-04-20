using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneSelectionScript : MonoBehaviour
{
    [Header("背景UI")]
    public Button bg_1;
    public Vector3 targetPos_1 = new Vector3(-4.721649f , -15.4502f , -0.8884582f);
    public Vector3 originPos_1 = new Vector3(-5.64f , -14.26f , -0.34f);
    public Button bg_2;
    public Vector3 targetPos_2 = new Vector3(-2.675011f , -15.4502f , -2.118828f);
    public Vector3 originPos_2 = new Vector3(-2.04f , -14.26f , -2.5f);
    public Button bg_3;
    public Vector3 targetPos_3 = new Vector3(-2.675011f , -17.94861f , -2.118828f);
    public Vector3 originPos_3 = new Vector3(-2.04f , -18.36f , -2.52f);
    public Button bg_4;
    public Vector3 targetPos_4 = new Vector3(-4.721649f , -17.94861f , -0.8884582f);
    public Vector3 originPos_4 = new Vector3(-5.64f , -18.34f , -0.31f);

    [Header("按钮控件")]
    public Button Lab_1;
    public Button Lab_2;
    [Header("分数显示文本")]
    public Text firstSceneScoreText;
    public Text secondSceneScoreText;

    void Start()
    {
        // 设置背景Ui初始位置
        bg_1.GetComponent<RectTransform>().anchoredPosition = originPos_1; 
        bg_2.GetComponent<RectTransform>().anchoredPosition = originPos_2;
        bg_3.GetComponent<RectTransform>().anchoredPosition = originPos_3;
        bg_4.GetComponent<RectTransform>().anchoredPosition = originPos_4;
        // 设置背景UI初始透明度
        bg_1.GetComponent<CanvasGroup>().alpha = 0f;
        bg_2.GetComponent<CanvasGroup>().alpha = 0f;
        bg_3.GetComponent<CanvasGroup>().alpha = 0f;
        bg_4.GetComponent<CanvasGroup>().alpha = 0f;
        Lab_1.GetComponent<CanvasGroup>().alpha = 0f;
        Lab_2.GetComponent<CanvasGroup>().alpha = 0f;
        firstSceneScoreText.GetComponent<CanvasGroup>().alpha = 0f;
        secondSceneScoreText.GetComponent<CanvasGroup>().alpha = 0f;
        // 为按钮添加监听器
        Lab_1.onClick.AddListener(Lab_1_click);
        Lab_2.onClick.AddListener(Lab_2_click);

        // 显示分数记录
        ShowScore_1(firstSceneScoreText);
        ShowScore_2(secondSceneScoreText);
    }

    public IEnumerator UIAppearAnimation()
    {
        // 设置背景Ui初始位置
        bg_1.GetComponent<RectTransform>().anchoredPosition = originPos_1; 
        bg_2.GetComponent<RectTransform>().anchoredPosition = originPos_2;
        bg_3.GetComponent<RectTransform>().anchoredPosition = originPos_3;
        bg_4.GetComponent<RectTransform>().anchoredPosition = originPos_4;
        // 设置背景UI初始透明度
        bg_1.GetComponent<CanvasGroup>().alpha = 0f;
        bg_2.GetComponent<CanvasGroup>().alpha = 0f;
        bg_3.GetComponent<CanvasGroup>().alpha = 0f;
        bg_4.GetComponent<CanvasGroup>().alpha = 0f;
        Lab_1.GetComponent<CanvasGroup>().alpha = 0f;
        Lab_2.GetComponent<CanvasGroup>().alpha = 0f;
        // 背景UI移动到目标位置
        bg_1.GetComponent<RectTransform>().DOAnchorPos(targetPos_1 , 0.5f);
        bg_2.GetComponent<RectTransform>().DOAnchorPos(targetPos_2 , 0.5f);
        bg_3.GetComponent<RectTransform>().DOAnchorPos(targetPos_3 , 0.5f);
        bg_4.GetComponent<RectTransform>().DOAnchorPos(targetPos_4 , 0.5f);
        // 背景UI淡入
        bg_1.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
        bg_2.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
        bg_3.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
        bg_4.GetComponent<CanvasGroup>().DOFade(1f , 0.5f).OnComplete(() =>
        {
            Lab_1.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
            Lab_2.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
            firstSceneScoreText.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
            secondSceneScoreText.GetComponent<CanvasGroup>().DOFade(1f , 0.5f);
        });

        yield return null;
    }

    void Update()
    {
        onButtonHover(Lab_1);
        onButtonHover(Lab_2);
    }

    public IEnumerator UIDisappearAnimation()
    {
        Lab_1.DOKill();
        Lab_2.DOKill();
        Lab_1.GetComponent<CanvasGroup>().DOFade(0f , 0f);
        Lab_2.GetComponent<CanvasGroup>().DOFade(0f , 0f);
        firstSceneScoreText.DOKill();
        secondSceneScoreText.DOKill();
        firstSceneScoreText.GetComponent<CanvasGroup>().DOFade(0f , 0f);
        secondSceneScoreText.GetComponent<CanvasGroup>().DOFade(0f , 0f);

        // 背景UI移动回初始位置
        bg_1.GetComponent<RectTransform>().DOAnchorPos(originPos_1 , 0.5f);
        bg_2.GetComponent<RectTransform>().DOAnchorPos(originPos_2 , 0.5f);
        bg_3.GetComponent<RectTransform>().DOAnchorPos(originPos_3 , 0.5f);
        bg_4.GetComponent<RectTransform>().DOAnchorPos(originPos_4 , 0.5f);
        // 背景UI淡出
        bg_1.GetComponent<CanvasGroup>().DOFade(0f , 0.5f);
        bg_2.GetComponent<CanvasGroup>().DOFade(0f , 0.5f);
        bg_3.GetComponent<CanvasGroup>().DOFade(0f , 0.5f);
        bg_4.GetComponent<CanvasGroup>().DOFade(0f , 0.5f);

        yield return null;
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
            button.GetComponent<RectTransform>().DOScale(0.009f , 0.2f);
        }
        else
        {
            button.GetComponent<RectTransform>().DOScale(0.01f , 0.2f);
        }
    }

    void ShowScore_1(Text scoreText)
    {
        scoreText.text = GameData.Instance.GetFirstSceneScore().ToString();
    }
    void ShowScore_2(Text scoreText)
    {
        scoreText.text = GameData.Instance.GetSecondSceneScore().ToString();
    }
    

    void Lab_1_click()
    {
        SceneManager.LoadScene("SpaceTravelScene_1");
    }

    void Lab_2_click()
    {
        SceneManager.LoadScene("RunScene");
    }
}
