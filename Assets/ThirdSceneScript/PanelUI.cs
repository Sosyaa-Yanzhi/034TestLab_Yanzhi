using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelUI : MonoBehaviour
{
    [Header("分数统计UI")]
    public Text scoreText;
    [Header("生命条UI")]
    public Image HPImage;
    public Image HPImage_2;
    private Image imageComponent;
    private Image imageComponent_2;
    [Header("导弹可用状态UI")]
    public Text missileUsabilty;
    [Header("脚本引用")]
    public JetControlScript jetControlScript;
    public ScoreCount scoreCount;

    void Start()
    {
        imageComponent = HPImage.GetComponent<Image>();
        imageComponent_2 = HPImage_2.GetComponent<Image>();
        imageComponent.fillAmount = 1f;
        imageComponent_2.fillAmount = 1f;
    }

    void Update()
    {
        ChangeFillAmount();
        UpdateMissileState();
        UpdateScore();
    }

    void ChangeFillAmount()
    {
        imageComponent.DOFillAmount((float)jetControlScript.hp / 100f, 0.5f);
        imageComponent_2.DOFillAmount((float)jetControlScript.hp / 100f, 2f);
    }

    void UpdateMissileState()
    {
        if (jetControlScript.isMissileAvailable) missileUsabilty.text = "Available";
        else missileUsabilty.text = "Unavailable";
    }

    void UpdateScore()
    {
        scoreText.text = scoreCount.score.ToString();
    }
}
