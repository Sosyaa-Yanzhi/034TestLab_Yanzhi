using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartCamControl : MonoBehaviour
{
    public float mouseX;
    public float mouseY;
    public float mouseSensitivity = 100f;
    public float xRotation;
    public float yRotation;

    void Start()
    {
        // 锁定光标到屏幕中心
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetMouseMovement();
    }

    void GetMouseMovement()
    {
        // 获取鼠标坐标增量
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    
        // 垂直旋转相机
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation , -15f , 15f);  // 限制上下角度
        yRotation = Mathf.Clamp(yRotation + mouseX , -10f , 10f);  // 限制水平角度

        // 应用旋转
        transform.localRotation = Quaternion.Euler(xRotation , yRotation , 0f);
    }
}
