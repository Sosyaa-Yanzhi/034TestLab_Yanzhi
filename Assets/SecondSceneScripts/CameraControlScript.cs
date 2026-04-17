using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CameraControlScript : MonoBehaviour
{
    [Header("ת���ڸ�UI")]
    public Image coverImage;
    [Header("�ƶ��ٶ�")]
    public float moveSpeed = 2f;
    [Header("�Ų���Ч")]
    public AudioSource audioSource;
    [Header("�������")]
    public Animator animator;
    [Header("场景过渡UI")]
    public Image CoverImage;
    public float checkPoint = 10f;

    void Awake()
    {
        // �����ڸ�UI͸����
        coverImage.GetComponent<CanvasGroup>().DOFade(0f , 0f);

        // ��ȡ��Դ���
        audioSource = GetComponent<AudioSource>();

        // ��ȡ�������
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    void CameraMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += this.transform.forward * Time.deltaTime * moveSpeed;

            // �����Чû���ڲ��ţ��ſ�ʼ����
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // �ɿ�W��ʱֹͣ����
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    // 如果行走到一定位置，进行场景切换
    void ChangeScene()
    {
        if (this.transform.position.z >= checkPoint)
        {
            CoverImage.GetComponent<CanvasGroup>().DOFade(1f , 1f);
            DOVirtual.DelayedCall(1f , () =>
            {
                SceneManager.LoadScene("RunScene");
            });
        }
    }
}
