using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    // 星星预制体生成坐标
    public float resPosition_X;
    public float resPosition_Y;


    void Awake()
    {
        #region 初始化属性
        // 随机选取星星生成位置
        resPosition_X = Random.Range(-10f , 10f);
        resPosition_Y = Random.Range(5.26f , 8f);
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3 (resPosition_X , resPosition_Y , 0f);
    }

    // Update is called once per frame
    void Update()
    {
        StarMove();

        PlanetDestroy();
    }

    void StarMove()
    {
        this.transform.position -= this.transform.up * Time.deltaTime * PhysicalConstants.starMoveSpeed;   // 星星和飞船进行相对运动
    }

    void PlanetDestroy()   // 星星删除
    {
        if (this.transform.position.y < -12f)   // 如果星星运动到了屏幕外目标点
        {
            Destroy(gameObject);  // 销毁实例
        }
    }
}
