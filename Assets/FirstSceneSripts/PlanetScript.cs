using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlanetScript: MonoBehaviour
{
    public float planetRadius; // 星球半径
    public float planetWeigth; // 星球质量
    public GameObject spacecraft;
    private float Distance; // 星球地心与飞船的距离
    public float planetPosition_X;  // 星球初始 X 坐标
    public float planetPosition_Y;  // 星球初始 Y 坐标
    public Renderer planetRenderer;  // 星球颜色
    public Rigidbody2D spacecraftRB;  // 飞船的刚体物理组件

    void Awake()
    {
        #region 初始化逻辑
        spacecraft = GameObject.Find("Spacecraft");  // 获取玩家飞船对象

        planetWeigth = Random.Range(10f , 30f);   // 随机决定行星质量

        int Right_or_Left = Random.Range(0 , 2);  // 随机决定星球生成大致方位
        if (Right_or_Left == 0)   // 星球在左侧生成
        {
            planetPosition_X = Random.Range(-9f , -5f);   // 随机选取星球生成的 X 坐标

            if (planetPosition_X < -7f)   // 根据星球生成的具体位置，进一步确定其半径取值范围
            {
                planetRadius = Random.Range(4f , 6.6f);  // 根据取值范围确定星球之半径
                this.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
            }
            else if (planetPosition_X > -7f)
            {
                planetRadius = Random.Range(2.7f , 3.73f);  // 根据取值范围确定星球之半径
                this.transform.localScale = new Vector3(planetRadius , planetRadius , planetRadius);
            }
        }
        else if (Right_or_Left == 1)
        {
            planetPosition_X = Random.Range(5f , 9f);   // 随机选取星球生成的 X 坐标

            if (planetPosition_X > 7f)   // 根据星球生成的具体位置，进一步确定其半径取值范围
            {
                planetRadius = Random.Range(4f, 6.6f);  // 根据取值范围确定星球之半径
                this.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
            }
            else if (planetPosition_X < 7f)
            {
                planetRadius = Random.Range(2.7f, 3.73f);  // 根据取值范围确定星球之半径
                this.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
            }
        }

        planetPosition_Y = Random.Range(5.86f, 11.7f);   // 随机选取星球生成的 Y 坐标

        planetRenderer = GetComponent<Renderer>();
        planetRenderer.material.color = Random.ColorHSV();  // 随机选定星球的颜色

        // ======物理效果======
        spacecraftRB = spacecraft.GetComponent<Rigidbody2D>();  // 获取飞船刚体物理组件
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(planetPosition_X , planetPosition_Y , 0f);   // 于指定位置生成星球
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(this.transform.position , spacecraft.transform.position);  // 计算星球球心与飞船之距离

        SpacecraftMove();

        PlanetDestroy();

        ForceEffect();
    }

    void SpacecraftMove()   //  实现飞船与星球之间的相对运动
    {
        this.transform.position -= this.transform.up * PhysicalConstants.spacecraftSpeed * Time.deltaTime;  // 根据飞船当前移动速度，星球进行相对运动
    }

    void ForceEffect()  // 引力效果
    {
        float totalForce = PhysicalConstants.G_constant * planetWeigth * PhysicalConstants.spacecraftWeight / (Distance * Distance);   // 计算引力大小
        // 计算飞船和星球球心向量的角度
        float angle_cos = (this.transform.position.x - spacecraft.transform.position.x) / (Distance * Distance);
        float angle_sin = (this.transform.position.y - spacecraft.transform.position.y) / (Distance * Distance);
        Vector2 forceDirection = new Vector2 (angle_cos, angle_sin);
        
        spacecraftRB.AddForce(forceDirection * totalForce , ForceMode2D.Force);
    }
     
    void PlanetDestroy()   // 行星删除
    {
        if (this.transform.position.y < -12f)   // 如果行星运动到了屏幕外目标点
        {
            Destroy(gameObject);  // 销毁实例
        }
    }
}
