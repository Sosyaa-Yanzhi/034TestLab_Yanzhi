using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    [Header("分数统计UI")]
    public Text scoreText;
    [Header("生命条UI")]
    public Image HPImage;
    private Image imageComponent;
    [Header("导弹可用状态UI")]
    public Text missileUsabilty;
    [Header("脚本引用")]
    public JetControlScript jetControlScript;
    public ScoreCount scoreCount;

    void Start()
    {
        imageComponent = HPImage.GetComponent<Image>();
        imageComponent.fillAmount = 1f;
    }

    void Update()
    {
        ChangeFillAmount();
        UpdateMissileState();
        UpdateScore();
    }

    void ChangeFillAmount()
    {
        imageComponent.fillAmount = (float)jetControlScript.hp / (float)100;
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
