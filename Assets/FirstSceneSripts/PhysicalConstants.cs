using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalConstants : MonoBehaviour
{
    static public float spacecraftWeight = 0.1f;  // 飞船质量
    static public float spacecraftSpeed = 1f; // 飞船恒定速度
    static public float G_constant = 0.667f; // 引力常量（根据游戏内效果来决定）
    static public float starMoveSpeed = 10f;  // 星星移动速度
}
