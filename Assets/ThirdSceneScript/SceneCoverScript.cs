using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneCoverScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().DOFade(0f , 1f);
        DOVirtual.DelayedCall(1f , () =>
        {
            gameObject.SetActive(false);
        });
    }
}
