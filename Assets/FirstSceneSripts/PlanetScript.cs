using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlanetScript: MonoBehaviour
{
    public float planetRadius; // ����뾶
    public float planetWeigth; // ��������
    public GameObject spacecraft;
    private float Distance; // ���������ɴ��ľ���
    public float planetPosition_X;  // �����ʼ X ����
    public float planetPosition_Y;  // �����ʼ Y ����
    public Renderer planetRenderer;  // ������ɫ
    public Rigidbody2D spacecraftRB;  // �ɴ��ĸ����������

    void Awake()
    {
        #region ��ʼ���߼�
        spacecraft = GameObject.Find("Spacecraft");  // ��ȡ��ҷɴ�����

        planetWeigth = Random.Range(10f , 30f);   // ���������������

        int Right_or_Left = Random.Range(0 , 2);  // ��������������ɴ��·�λ
        if (Right_or_Left == 0)   // �������������
        {
            planetPosition_X = Random.Range(-9f , -5f);   // ���ѡȡ�������ɵ� X ����

            if (planetPosition_X < -7f)   // �����������ɵľ���λ�ã���һ��ȷ����뾶ȡֵ��Χ
            {
                planetRadius = Random.Range(4f , 6.6f);  // ����ȡֵ��Χȷ������֮�뾶
                this.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
            }
            else if (planetPosition_X > -7f)
            {
                planetRadius = Random.Range(2.7f , 3.73f);  // ����ȡֵ��Χȷ������֮�뾶
                this.transform.localScale = new Vector3(planetRadius , planetRadius , planetRadius);
            }
        }
        else if (Right_or_Left == 1)
        {
            planetPosition_X = Random.Range(5f , 9f);   // ���ѡȡ�������ɵ� X ����

            if (planetPosition_X > 7f)   // �����������ɵľ���λ�ã���һ��ȷ����뾶ȡֵ��Χ
            {
                planetRadius = Random.Range(4f, 6.6f);  // ����ȡֵ��Χȷ������֮�뾶
                this.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
            }
            else if (planetPosition_X < 7f)
            {
                planetRadius = Random.Range(2.7f, 3.73f);  // ����ȡֵ��Χȷ������֮�뾶
                this.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
            }
        }

        planetPosition_Y = Random.Range(5.86f, 11.7f);   // ���ѡȡ�������ɵ� Y ����

        planetRenderer = GetComponent<Renderer>();
        planetRenderer.material.color = Random.ColorHSV();  // ���ѡ���������ɫ

        // ======����Ч��======
        spacecraftRB = spacecraft.GetComponent<Rigidbody2D>();  // ��ȡ�ɴ������������
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(planetPosition_X , planetPosition_Y , 0f);   // ��ָ��λ����������
        AddCollider();
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(this.transform.position , spacecraft.transform.position);  // ��������������ɴ�֮����

        SpacecraftMove();

        PlanetDestroy();

        ForceEffect();
    }

    void SpacecraftMove()   //  ʵ�ַɴ�������֮�������˶�
    {
        this.transform.position -= this.transform.up * PhysicalConstants.spacecraftSpeed * Time.deltaTime;  // ���ݷɴ���ǰ�ƶ��ٶȣ������������˶�
    }

    void ForceEffect()  // ����Ч��
    {
        float totalForce = PhysicalConstants.G_constant * planetWeigth * PhysicalConstants.spacecraftWeight / (Distance * Distance);   // ����������С
        // ����ɴ����������������ĽǶ�
        float angle_cos = (this.transform.position.x - spacecraft.transform.position.x) / (Distance * Distance);
        float angle_sin = (this.transform.position.y - spacecraft.transform.position.y) / (Distance * Distance);
        Vector2 forceDirection = new Vector2 (angle_cos, angle_sin);
        
        spacecraftRB.AddForce(forceDirection * totalForce , ForceMode2D.Force);
    }
     
    void PlanetDestroy()   // ����ɾ��
    {
        if (this.transform.position.y < -12f)   // ��������˶�������Ļ��Ŀ���
        {
            Destroy(gameObject);  // ����ʵ��
        }
    }

    #region 添加碰撞体
    void AddCollider()
    {
        if (GetComponent<CircleCollider2D>() == null)
        {
            CircleCollider2D circleCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
            circleCollider2D.radius = 0.55f;
        }
    }
    #endregion
}
