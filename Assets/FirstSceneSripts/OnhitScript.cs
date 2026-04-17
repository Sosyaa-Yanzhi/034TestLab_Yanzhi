using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnhitScript : MonoBehaviour
{
    [Header("受击反馈")]
    public GameObject hitEffection;
    public GameObject mainCamera;
    private float animationDuration = 0.5f;
    private float maxOpacity = 0.5f;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        hitEffection.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stone"))
        {
            Debug.Log("被陨石击中！");
            StartCoroutine(PlayAnimation());
        }
    }

    IEnumerator PlayAnimation()
    {
        mainCamera.GetComponent<Transform>().DOShakePosition(animationDuration * 2 , 3f);
        hitEffection.SetActive(true);
        hitEffection.GetComponent<SpriteRenderer>().DOFade(maxOpacity , animationDuration);
        yield return new WaitForSeconds(animationDuration);
        hitEffection.GetComponent<SpriteRenderer>().DOFade(0f , animationDuration);
        yield return new WaitForSeconds(animationDuration);
        hitEffection.SetActive(false);
        yield return null;
    }
}
