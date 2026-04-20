using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class EnemyScript : MonoBehaviour
{
    public GameObject Laser_Aim;
    public GameObject Laser_Attack;
    public GameObject Laser_Attack_BG;
    public GameObject Aim_Parent;
    public float MoveTime;
    public float timer;
    public float stopTime;
    public float RespawnPosition_x = 13f;
    public float RespawnPosition_y;
    public int Health = 100;
    public EnemyManager enemyManager;
    public GameObject managerObject;
    public string poolIndex;
    public float targetPosition_x;
    public float targetPosition_y;
    public float attackCoolTime = 3f;
    public GameObject player;
    public GameObject explosionObject;
    [Header("得分模块")]
    public GameObject scoreCounter;
    public ScoreCount scoreCount;

    void Awake()
    {
        // 获取对象及其脚本
        managerObject = GameObject.Find("EnemyManager");
        enemyManager = managerObject.GetComponent<EnemyManager>();
        // 获取玩家对象
        player = GameObject.Find("Jet");
        scoreCounter = GameObject.Find("ScoreCounter");
        scoreCount = scoreCounter.GetComponent<ScoreCount>();
        // 设置生命值
        Health = 100;
    }

    void OnEnable()
    {
        Health = 100;
        // 每次激活时重新启动协程
        StartCoroutine(MoveToTargetPoint());
        StartCoroutine(AttackPlayer());
    }

    void Start()
    {
        // 设置子物体状态
        GetComponent<SpriteRenderer>().color = Color.yellow;
        Laser_Aim.SetActive(false);
        Laser_Attack.SetActive(false);
        Laser_Attack_BG.SetActive(false);
        explosionObject.SetActive(false);
        // 初始化生成位置之纵坐标
        RespawnPosition_y = Random.Range(-3f , 4f);
        // 初始化生成位置
        transform.position = new Vector3(RespawnPosition_x , RespawnPosition_y , 0f);
        // 获取编号
        poolIndex = GetComponent<Text>().text;
        // 设置生命值
        Health = 100;
    }

    #region 移动至目标位置
    IEnumerator MoveToTargetPoint()
    {
        while (Health > 0)
        {
            stopTime = Random.Range(0.5f , 3f);
            MoveTime = Random.Range(0.1f , 0.5f);
            targetPosition_x = Random.Range(1.5f , 7f);
            targetPosition_y = Random.Range(-3f , 3.5f);
            GetComponent<Transform>().DOMove(new Vector3(targetPosition_x , targetPosition_y , 0f) , MoveTime);

            yield return new WaitForSeconds(stopTime);
        }
    }
    #endregion

    #region 攻击行为
    IEnumerator AttackPlayer()
    {
        while (Health > 0)
        {   
            Laser_Aim.SetActive(true);
            float angle = GetAngle();
            Aim_Parent.transform.rotation = Quaternion.Euler(0f , 0f , 0f);
            Aim_Parent.transform.Rotate(0f , 0f , angle);
            StartCoroutine(ShootLaser());

            yield return new WaitForSeconds(attackCoolTime);
        }
    }
    #endregion

    #region 激光发射效果
    IEnumerator ShootLaser()
    {
        Laser_Attack.SetActive(true);
        Laser_Attack_BG.SetActive(true);
        Laser_Attack_BG.GetComponent<SpriteRenderer>().DOFade(0.5f , 0f);
        Laser_Attack.GetComponent<Transform>().DOScaleY(0.2f , 0.2f);
        Laser_Attack_BG.GetComponent<Transform>().DOScaleY(0.71f , 0.4f);
        Laser_Attack_BG.GetComponent<SpriteRenderer>().DOFade(0f , 0.3f);
        DOVirtual.DelayedCall(0.4f , () =>
        {
            Laser_Attack.SetActive(false);
            Laser_Attack_BG.SetActive(false);
        });

        yield return null;
    }
    #endregion

    #region 同玩家相对角度计算
    float GetAngle()
    {
        float angle = 0f;
        if (transform.position.y > player.transform.position.y)
        {
            float distance = Vector3.Distance(transform.position , player.transform.position);
            float dis_Y = transform.position.y - player.transform.position.y;
            angle = Mathf.Asin(dis_Y / distance);
            angle *= Mathf.Rad2Deg * -1;
        }
        else if (transform.position.y < player.transform.position.y)
        {
            float distance = Vector3.Distance(transform.position , player.transform.position);
            float dis_Y = (transform.position.y - player.transform.position.y) * -1;
            angle = Mathf.Asin(dis_Y / distance);
            angle *= Mathf.Rad2Deg;
        }
        else if (transform.position.y == player.transform.position.y) angle = 0f;
        return angle;
    }
    #endregion

    #region 受击效果
    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Bullet"))
        {
            Health -= 10;
            GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("敌人被命中！当前生命值：" + Health);
        }
        else if (other.CompareTag("Missile_lock") || other.CompareTag("Missile"))
        {
            Health -= 50;
            GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("敌人被命中！当前生命值：" + Health);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (Health <= 0) Destroy();
    }
    #endregion
    
    #region 销毁
    void Destroy()
    {
        if (Health <= 0)
        {
            scoreCount.score += 1;
            if (scoreCount.score >= GameData.Instance.GetSecondSceneScore())
            {
                GameData.Instance.UpdateSecondSceneScore(scoreCount.score);
            }
            GetComponent<SpriteRenderer>().color = Color.yellow;
            Health = 100;
            explosionObject.SetActive(true);
            // 爆炸动画
            explosionObject.GetComponent<SpriteRenderer>().DOFade(0.5f , 0.5f);
            explosionObject.GetComponent<Transform>().DOScale(10f , 0.5f);
            DOVirtual.DelayedCall(0.5f , () =>
            {
                explosionObject.GetComponent<Transform>().DOScale(0.6f , 0f);
                explosionObject.GetComponent<SpriteRenderer>().DOFade(1f , 0f);
                explosionObject.SetActive(false);
                enemyManager.ReturnEnemy(poolIndex , gameObject);
            });
        }
    }
    #endregion
}
