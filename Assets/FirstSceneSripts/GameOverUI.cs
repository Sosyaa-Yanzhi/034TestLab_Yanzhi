using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("游戏结束结算UI")]
    public GameObject gameOverUIManager; // 父级
    public Image bgImage_up;
    public Image bgImage_down;
    public List<Text> gameOverText;
    public Text textPrefab;

    [Header("UI位置")]
    public float upImage_posY_origin = 337f;
    public float downImage_posY_origin = -337f;
    public float upImage_posY_target = 112.5f;
    public float downImage_posY_target = -112.5f;
    public Vector3 textOriginPos = new Vector3(137f , -87f , 0f);
    public Vector3 textTargetPos = new Vector3(0f , 0f , 0f);
    private int textCount = 5;


    void Start()
    {
        gameOverUIManager.SetActive(false);
        bgImage_up.rectTransform.anchoredPosition = new Vector3(-0.6348f , upImage_posY_origin , 0f);
        bgImage_down.rectTransform.anchoredPosition= new Vector3(-0.63556f , downImage_posY_origin , 0f);
        bgImage_up.GetComponent<CanvasGroup>().alpha = 0f;
        bgImage_down.GetComponent<CanvasGroup>().alpha = 0f;

        // Add Text to List.
        for (int i = 0 ; i < textCount ; i++)
        {
            Text newText = Instantiate(textPrefab , textOriginPos , Quaternion.identity);
            newText.transform.SetParent(gameOverUIManager.transform , false);
            newText.rectTransform.anchoredPosition = textOriginPos;
            if (newText.GetComponent<CanvasGroup>() == null) newText.gameObject.AddComponent<CanvasGroup>();
            newText.GetComponent<CanvasGroup>().alpha = 0f;
            gameOverText.Add(newText);
        }

        for (int i = 0 ; i < gameOverText.Count ; i++)
        {
            gameOverText[i].GetComponent<RectTransform>().DOAnchorPos(new Vector2(137f , -87f) , 0f);
        }
    }

    void Update()
    {
        if (Lab_1_EventsManager.Instance.GetGameState() == true)
        {
            Debug.Log("Game is over!!!");
            PlayGameOverAnimation();
        }
    }

    void PlayGameOverAnimation()
    {
        gameOverUIManager.SetActive(true);

        bgImage_up.GetComponent<RectTransform>().DOAnchorPosY(upImage_posY_target , 1f);
        bgImage_down.GetComponent<RectTransform>().DOAnchorPosY(downImage_posY_target , 1f);
        bgImage_up.GetComponent<CanvasGroup>().DOFade(1f , 1f);
        bgImage_down.GetComponent<CanvasGroup>().DOFade(1f , 1f);

        StartCoroutine(TextAnimation());

        Invoke("BackToMenu" , 3f);
    }

    IEnumerator TextAnimation()
    {
        for (int i = 0 ; i < gameOverText.Count ; i++)
        {
            Text text = gameOverText[i];
            text.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f , 0f) , 1f);
            text.GetComponent<CanvasGroup>().DOFade(1f , 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
