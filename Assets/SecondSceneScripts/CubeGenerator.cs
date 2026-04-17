using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube_ : MonoBehaviour
{
    [Header("待生成的方块预制体 ")]
    public GameObject cube_prefab;
    [Header("水平阵列排布数量")]
    public int X_number = 0;
    [Header("竖向阵列排布数量")]
    public int Y_number = 0;
    [Header("Z向阵列排布数量")]
    public int Z_number = 0;
    [Header("坐标偏移量")]
    public float offset = 0f;

    // X 坐标储存
    private float[] X_array = new float[50];
    // Y 坐标储存
    private float[] Y_array = new float[50];
    // Z 坐标储存
    private float[] Z_array = new float[50];


    // Start is called before the first frame update
    void Start()
    {
        // 生成方块矩阵
        // 首先是X轴方向生成
        X_array[0] = this.transform.position.x;
        Z_array[0] = this.transform.position.z;
        Y_array[0] = this.transform.position.y;


        for (int i = 1; i < X_number; i++)
        {
            float x_position;
            if ( i == 1 )
            {
                x_position = this.transform.position.x + offset;
            }
            else
            {
                x_position = X_array[i - 1] + offset;
            }
            GameObject cube = Instantiate(cube_prefab);
            cube.transform.position = new Vector3(x_position, this.transform.position.y, this.transform.position.z);
            cube.transform.SetParent(this.transform);
            X_array[i] = x_position;
        }
        // 然后是Z轴方向生成
        for (int i = 1; i < Z_number; i++)
        {
            float z_position;
            if (i == 1)
            {
                z_position = this.transform.position.z + offset;
            }
            else
            {
                z_position= Z_array[i - 1] + offset;
            }
            for (int j = 0; j < X_number; j++)
            {
                GameObject cube_1 = Instantiate(cube_prefab);
                cube_1.transform.position = new Vector3(X_array[j] , this.transform.position.y , z_position);
                cube_1.transform.SetParent(this.transform);
            }
            Z_array[i] = z_position;
        }
        // 最后是Y轴方向生成！
        for (int i = 1; i < Y_number; i++)
        {
            float y_position;
            y_position = Y_array[i - 1] + offset;
            Y_array[i] = y_position;
            for (int j = 0; j < Z_number; j++)
            {
                for (int m = 0; m < X_number; m++)
                {
                    GameObject cube_2 = Instantiate(cube_prefab);
                    cube_2.transform.position = new Vector3(X_array[m] , Y_array[i] , Z_array[j]);
                    cube_2.transform.SetParent(this.transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
