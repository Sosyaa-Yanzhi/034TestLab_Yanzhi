using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MissileScript : MonoBehaviour
{
    public GameObject explosionEffect;
    public GameObject explosionObject;
    public bool explosed =false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(MissileExplosion(gameObject));
            explosed = true;
            DOVirtual.DelayedCall(0.7f , () =>
            {
                Destroy(gameObject);
                Destroy(explosionObject);
            });
        }
    }

    IEnumerator MissileExplosion(GameObject missile)
    {
        explosionObject = Instantiate(explosionEffect , missile.transform.position , Quaternion.identity);
        explosionObject.GetComponent<SpriteRenderer>().DOFade(0.5f , 0f);
        explosionObject.GetComponent<SpriteRenderer>().DOFade(1f , 0.3f);
        explosionObject.GetComponent<Transform>().DOScale(5f , 0.3f);
        yield return new WaitForSeconds(0.3f);
        explosionObject.GetComponent<SpriteRenderer>().DOFade(0f , 0.3f);
        explosionObject.GetComponent<Transform>().DOScale(7.3f , 0.3f);
        yield return new WaitForSeconds(0.3f);
        Destroy(explosionObject);


        yield return null;
    }
}
