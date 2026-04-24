using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftControlScript : MonoBehaviour
{
    public float Left_RCS_Force;   // ���ʸ���������
    public float Right_RCS_Force;  // �Ҳ�ʸ���������
    public float Up_RCS_Force;  // ǰ���������
    public float Down_RCS_Force;  // ����������� 
    private Rigidbody2D spacecraftRB;  // �ɴ������������

    void Awake()
    {
        #region �ɴ��������Գ�ʼ��
        spacecraftRB = GetComponent<Rigidbody2D>();  // ��ȡ�ɴ������������
        spacecraftRB.mass = PhysicalConstants.spacecraftWeight;  // ���÷ɴ�����
        spacecraftRB.drag = 0f;  // ���÷ɴ���̫���еĿ�������Ϊ0
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        // ��������ʸ����ڳ�ʼ����
        Left_RCS_Force = 0f;
        Right_RCS_Force = 0f;

        // ����ǰ����ڳ�ʼ����
        Up_RCS_Force = 0f;
        Down_RCS_Force = 0f;
    }

    void FixedUpdate()
    {
        DirectionControl();

        // ����������ڲ���������
        spacecraftRB.AddForce(Left_RCS_Force * Vector2.left , ForceMode2D.Force);
        spacecraftRB.AddForce(Right_RCS_Force * Vector2.right , ForceMode2D.Force);

        // ����ǰ����ڲ���������
        spacecraftRB.AddForce(Up_RCS_Force * Vector2.up , ForceMode2D.Force);
        spacecraftRB.AddForce(Down_RCS_Force * Vector2.down , ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DirectionControl()
    {
        if (Input.GetKey(KeyCode.A))  // ���� A �������ʸ����ڼ���
        {
            Left_RCS_Force = 1f;
        }
        else if (!Input.GetKey(KeyCode.A))  // ����ֹͣ��������
        {
            Left_RCS_Force = 0f;
        }

        if (Input.GetKey(KeyCode.D))  // ���� D �����Ҳ�ʸ����ڼ���
        {
            Right_RCS_Force = 1f;
        }
        else if (!Input.GetKey(KeyCode.D))  // ����ֹͣ��������
        {
            Right_RCS_Force = 0f;
        }

        if (Input.GetKey(KeyCode.W))   // ���� W ����ǰ����ڲ�������
        {
            Up_RCS_Force = 1f;
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            Up_RCS_Force = 0;
        }

        if (Input.GetKey(KeyCode.S))   // ���� S ����������ڲ�������
        {
            Down_RCS_Force = 1f;
        }
        else if (!Input.GetKey(KeyCode.S))
        {
            Down_RCS_Force = 0f;
        }
    }
}
