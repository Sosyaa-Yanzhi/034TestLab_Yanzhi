using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class JetControlScript : MonoBehaviour
{
    public int JetDurabilty = 100;
    public int BulletAmount = 1000;
    public Image LockUI;
    public GameObject bulletPrefab;
    public int CreateNumber = 10;
    public Queue<GameObject> left_platePool = new Queue<GameObject>();
    public Queue<GameObject> right_platePool = new Queue<GameObject>();
    public GameObject LeftGun;
    public GameObject RightGun;
    public GameObject camera;
    public GameObject HitEffect;
    public int hp = 100;

    [Header("导弹模块")]
    public GameObject crosshairPrefab;
    public GameObject missilePrefab;
    public float missileLoadTimer = 0f;
    public bool isMissileAvailable = false;


    void Start()
    {
        // 创建子弹对象池
        CreatePool();
        camera = GameObject.Find("Main Camera");
        HitEffect.GetComponent<SpriteRenderer>().DOFade(0.5f , 0f);
        HitEffect.SetActive(false);
        hp = 100;
    }

    void Update()
    {
        MovementControl();
        if (Input.GetKeyDown(KeyCode.Space)) ShootBullet();

        // 导弹计时 & 发射
        missileLoadTimer += Time.deltaTime;
        if (missileLoadTimer >= 5f) isMissileAvailable = true;
        else isMissileAvailable = false;
        if (Input.GetKey(KeyCode.F) && missileLoadTimer >= 5f)
        {
            LaunchMissile();
            missileLoadTimer = 0f;
        }

        if (Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene("StartScene");
    }

    #region 创建子弹对象池
    void CreatePool()
    {
        left_platePool.Clear();
        for (int i = 0; i < CreateNumber; i++)
        {
            GameObject plate = Instantiate(bulletPrefab , LeftGun.transform.position , Quaternion.identity);
            BulletScript bulletScript = plate.GetComponent<BulletScript>();
            bulletScript.Pool = "left";
            plate.SetActive(false);
            left_platePool.Enqueue(plate);  // 将子弹预制体放入对象池中
        }

        right_platePool.Clear();
        for (int i = 0; i < CreateNumber; i++)
        {
            GameObject plate = Instantiate(bulletPrefab , RightGun.transform.position , Quaternion.identity);
            BulletScript bulletScript = plate.GetComponent<BulletScript>();
            bulletScript.Pool = "right";
            plate.SetActive(false);
            right_platePool.Enqueue(plate);  // 将子弹预制体放入对象池中
        }
    }
    #endregion

    #region 从对象池取物
    public GameObject GetPlate()
    {
        // 如果对象池中有子弹，直接生成
        if (left_platePool.Count > 0)
        {
            GameObject plate = left_platePool.Dequeue();  // 从池子中取出对象（子弹预制体）
            plate.transform.position = LeftGun.transform.position;
            plate.SetActive(true);
            return plate; 
        }
        // 如果对象池中没有子弹，就生成一个进行返回(默认为激活状态)
        GameObject newPlate = Instantiate(bulletPrefab , LeftGun.transform.position , Quaternion.identity);
        return newPlate;
    }
    public GameObject GetPlate_1()
    {
        // 如果对象池中有子弹，直接生成
        if (right_platePool.Count > 0)
        {
            GameObject plate = right_platePool.Dequeue();  // 从池子中取出对象（子弹预制体）
            plate.transform.position = RightGun.transform.position;
            plate.SetActive(true);
            return plate; 
        }
        // 如果对象池中没有子弹，就生成一个进行返回(默认为激活状态)
        GameObject newPlate = Instantiate(bulletPrefab , RightGun.transform.position , Quaternion.identity);
        return newPlate;
    }
    #endregion

    #region 将子弹归还于对象池中
    public void ReturnPlate(GameObject plate , Queue<GameObject> platePool)
    {
        plate.SetActive(false);
        platePool.Enqueue(plate);
    }
    #endregion

    #region  机炮开火
    void ShootBullet()
    {
        // 按下空格就发射子弹
        GameObject bullet = GetPlate();
        GameObject bullet_1 = GetPlate_1();

        // 判断子弹销毁条件
        BulletDestroy(bullet , left_platePool);
        BulletDestroy(bullet_1 , right_platePool);
    }
    #endregion

    #region 子弹销毁
    public void BulletDestroy(GameObject bullet , Queue<GameObject> platePool)
    {
        if (bullet.transform.position.x >= 10f)  // 如果子弹到了屏幕外，销毁
        {
            // 将子弹放回对象池中
            ReturnPlate(bullet , platePool);
        }
    }
    #endregion

    #region 移动控制
    void MovementControl()
    {
        // 上下爬升
        if (Input.GetKey(KeyCode.S) && transform.position.y >= -4.3f)
        {
            transform.position += 10f * Time.deltaTime * transform.right;
        }
        if (Input.GetKey(KeyCode.W) && transform.position.y <= 4.2f)
        {
            transform.position -= 10f * Time.deltaTime * transform.right;
        }

        // 滚转
        float maxScaleX = 1.4f;
        float minScaleX = -1.4f;
        bool isUpside = true;
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.localScale.x <= maxScaleX) transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.localScale.x >= minScaleX) transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y, transform.localScale.z);
        }
        if (0f <= transform.localScale.x)
        {
            isUpside = true;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else if (transform.localScale.x < 0f)
        {
            isUpside = false;
            GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
    #endregion

    #region 受击效果
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("EnemyLaser"))
        {
            Debug.Log("机体受损！");
            hp -= 10;
            CameraShake_Effect();
        }
    }
    void CameraShake_Effect()
    {
        camera.GetComponent<Transform>().DOShakePosition(0.5f, 0.5f, 3, 90, false, true);
        HitEffect.SetActive(true);
        HitEffect.GetComponent<SpriteRenderer>().DOFade(0f , 0.5f);
        DOVirtual.DelayedCall(0.5f , () =>
        {
            HitEffect.GetComponent<SpriteRenderer>().DOFade(0.5f , 0f);
            HitEffect.SetActive(false);
        });
    }
    #endregion

    #region 导弹模块
    void LaunchMissile()
    {
        StartCoroutine(LaunchMissile_S());
    }
    IEnumerator LaunchMissile_S()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newCrosshair = Instantiate(crosshairPrefab);
            yield return StartCoroutine(LockDown(newCrosshair));
        }
    }
    IEnumerator LockDown(GameObject crosshair)
    {
        crosshair.SetActive(true);
        crosshair.GetComponent<Transform>().DOScale(3f , 0.5f);
        crosshair.GetComponent<SpriteRenderer>().DOFade(1f , 0.5f);

        yield return new WaitForSeconds(0.3f);
    }
    #endregion
}
