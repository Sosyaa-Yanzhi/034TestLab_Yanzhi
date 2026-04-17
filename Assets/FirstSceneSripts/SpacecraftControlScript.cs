using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftControlScript : MonoBehaviour
{
    public float Left_RCS_Force;   // 左侧矢量喷口推力
    public float Right_RCS_Force;  // 右侧矢量喷口推力
    public float Up_RCS_Force;  // 前进喷口推力
    public float Down_RCS_Force;  // 后退喷口推力 
    private Rigidbody2D spacecraftRB;  // 飞船刚体物理组件

    void Awake()
    {
        #region 飞船物理属性初始化
        spacecraftRB = GetComponent<Rigidbody2D>();  // 获取飞船刚体物理组件
        spacecraftRB.mass = PhysicalConstants.spacecraftWeight;  // 设置飞船质量
        spacecraftRB.drag = 0f;  // 设置飞船于太空中的空气阻力为0
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        // 设置两侧矢量喷口初始推力
        Left_RCS_Force = 0f;
        Right_RCS_Force = 0f;

        // 设置前后喷口初始推力
        Up_RCS_Force = 0f;
        Down_RCS_Force = 0f;
    }

    void FixedUpdate()
    {
        DirectionControl();

        // 添加左右喷口产生的推力
        spacecraftRB.AddForce(Left_RCS_Force * Vector2.left , ForceMode2D.Force);
        spacecraftRB.AddForce(Right_RCS_Force * Vector2.right , ForceMode2D.Force);

        // 添加前后喷口产生的推力
        spacecraftRB.AddForce(Up_RCS_Force * Vector2.up , ForceMode2D.Force);
        spacecraftRB.AddForce(Down_RCS_Force * Vector2.down , ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DirectionControl()
    {
        if (Input.GetKey(KeyCode.A))  // 按下 A 键，左侧矢量喷口加力
        {
            Left_RCS_Force = 1f;
        }
        else if (!Input.GetKey(KeyCode.A))  // 否则：停止产生推力
        {
            Left_RCS_Force = 0f;
        }

        if (Input.GetKey(KeyCode.D))  // 按下 D 键，右侧矢量喷口加力
        {
            Right_RCS_Force = 1f;
        }
        else if (!Input.GetKey(KeyCode.D))  // 否则：停止产生推力
        {
            Right_RCS_Force = 0f;
        }

        if (Input.GetKey(KeyCode.W))   // 按下 W 键，前进喷口产生推力
        {
            Up_RCS_Force = 1f;
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            Up_RCS_Force = 0;
        }

        if (Input.GetKey(KeyCode.S))   // 按下 S 键，后退喷口产生推力
        {
            Down_RCS_Force = 1f;
        }
        else if (!Input.GetKey(KeyCode.S))
        {
            Down_RCS_Force = 0f;
        }
    }
}
