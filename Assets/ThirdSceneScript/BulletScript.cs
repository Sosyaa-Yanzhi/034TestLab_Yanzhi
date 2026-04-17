using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public JetControlScript jetControlScript;
    public string Pool;
    public GameObject bullet;
    public GameObject jet;
    public GameObject hitEffect;
    public GameObject hitE_prefab;
    public bool isGonnaDes = false;
    void Start()
    {
        jet = GameObject.Find("Jet");
        jetControlScript = jet.GetComponent<JetControlScript>();
        Debug.Log("子弹预制体生成！");
    }

    void Update()
    {
        BulletFly();

        // 子弹销毁条件判断
        if (transform.position.x >= 10f)
        {
            if (Pool == "left")
            {
                jetControlScript.BulletDestroy(bullet , jetControlScript.left_platePool);
            }
            if (Pool == "right")
            {
                jetControlScript.BulletDestroy(bullet , jetControlScript.right_platePool);
            }
        }
    }

    #region 子弹命中后销毁
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isGonnaDes)
        {
            bulletSpeed = 0f;
            gameObject.GetComponent<SpriteRenderer>().DOFade(0f , 0f);
            StartCoroutine(BulletHit());
            isGonnaDes = true;
            DOVirtual.DelayedCall(0.5f , () =>
            {
                Destroy(gameObject);
            });
        }
    }
    IEnumerator BulletHit()
    {
        hitEffect = Instantiate(hitE_prefab , gameObject.transform.position , Quaternion.identity);
        hitEffect.GetComponent<SpriteRenderer>().DOFade(0f , 0f);
        hitEffect.GetComponent<SpriteRenderer>().DOFade(1f , 0.3f);
        hitEffect.GetComponent<Transform>().DOScale(1.25f , 0.3f);
        yield return new WaitForSeconds(0.3f);
        hitEffect.GetComponent<SpriteRenderer>().DOFade(0f , 0.2f);
        hitEffect.GetComponent<Transform>().DOScale(1.49f , 0.2f);
        DOVirtual.DelayedCall(0.2f , () =>
        {
            hitEffect.SetActive(false);
        });

        yield return null;
    }
    #endregion

    #region 子弹飞行
    void BulletFly()
    {
        this.transform.position += this.transform.right * bulletSpeed * Time.deltaTime;
    }
    #endregion
}
