using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenuScript : MonoBehaviour
{
    public GameObject playerCam;
    public float checkZ = 17f;
    void Start()
    {
       playerCam = GameObject.Find("Main Camera");
    }

    void Update()
    {
        if (playerCam.transform.position.z > checkZ)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
        }
    }
}
