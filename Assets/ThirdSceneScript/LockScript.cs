using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class LockScript : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject targetEnemy;
    public EnemyScript enemyScript;
    public GameObject missilePrefab;
    public GameObject missile;
    public GameObject player;
    [Header("导弹贝塞尔曲线")]
    public Vector3 startPoint;
    public Vector3 controlPoint;
    public Vector3 endPoint;
    [Header("导弹爆炸动画")]
    public GameObject explosionEffect;
    public MissileScript missileScript;
    public GameObject explosionObject;

    void Start()
    {
        // 获取所有敌人
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (enemies.Length == 0)
        {
            Debug.Log("导弹雷达：目标丢失！");
            return;
        }
        
        int randomIndex = UnityEngine.Random.Range(0, enemies.Length);
        targetEnemy = enemies[randomIndex];
        enemyScript = targetEnemy.GetComponent<EnemyScript>();

        // 实例化导弹对象
        player = GameObject.Find("Jet");
        missile = Instantiate(missilePrefab , new Vector3(player.transform.position.x , player.transform.position.y , 0f) , Quaternion.identity);
        
        // 获取目标坐标并计算导弹飞行轨迹（只进行初步计算加大难度）
        float controlY = 0f;
        if (player.transform.position.y > targetEnemy.transform.position.y) controlY = targetEnemy.transform.position.y - player.transform.position.y;
        else if (player.transform.position.y < targetEnemy.transform.position.y) controlY = player.transform.position.y - targetEnemy.transform.position.y;
        startPoint = new Vector3(player.transform.position.x , player.transform.position.y - 0.5f , 0f);
        endPoint = new Vector3(targetEnemy.transform.position.x , targetEnemy.transform.position.y , 0f);
        controlPoint = new Vector3((targetEnemy.transform.position.x + player.transform.position.x) / 2 , controlY - 1f , 0f);
        
        // 播放导弹发射动画
        StartCoroutine(MissileLaunch());
    }

    IEnumerator MissileLaunch()
    {
        // 一阶段动画
        missile.tag = "Missile";
        missile.GetComponent<Transform>().DOMoveY(player.transform.position.y - 3f , 0.3f);
        // yield return new WaitForSeconds(0.3f);
        missile.GetComponent<Transform>().DOMoveX(player.transform.position.x + 2f , 0.5f);
        yield return new WaitForSeconds(0.5f);
        MissileChase();
    }

    void MissileChase()
    {
        Vector3[] path = new Vector3[] { startPoint, controlPoint, endPoint };
        missile.transform.DOPath(path, 1f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                StartCoroutine(MissileExplose(missile));
                missile.tag = "Missile_lock";
                missile.SetActive(false);
                Debug.Log("导弹命中");
                DOVirtual.DelayedCall(0.7f , () =>
                {
                    Destroy(missile);
                    Destroy(gameObject);
                });
        });

        // 若途中命中：
        missileScript = missile.GetComponent<MissileScript>();
        if (missileScript.explosed)
        {
            DOVirtual.DelayedCall(0.7f , () =>
            {
                Destroy(gameObject);
                Destroy(explosionObject);
            });
        }
    }

    IEnumerator MissileExplose(GameObject missile)
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

    void Update()
    {
        if (!targetEnemy.activeInHierarchy || enemyScript == null || targetEnemy == null || enemyScript.Health <= 0)
        {
            Debug.Log("丢失目标！");
            Destroy(gameObject);
        }
        else
        {
            transform.position = targetEnemy.transform.position;
        }
    }
}
