using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    [Header("玩家对象")]
    public GameObject spaceCraft;

    private LineRenderer lineRenderer;
    private float originDistance;
    private float currentDistance;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0f , 0f , 0f);
        lineRenderer = GetComponent<LineRenderer>();
        originDistance = Vector3.Distance(transform.position , spaceCraft.transform.position);
    }
    void Update()
    {
        Turning();
        Connecting();
        CheckGameState();
    }

    void Turning()
    {
        Vector3 direction = spaceCraft.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f , 0f , angle - 90f);
    }

    void Connecting()
    {
        lineRenderer.SetPosition(0 , transform.position);
        lineRenderer.SetPosition(1 , spaceCraft.transform.position);
        currentDistance = Vector3.Distance(spaceCraft.transform.position , transform.position);
        lineRenderer.startWidth = 0.25f - currentDistance / 30f;
    }

    void CheckGameState()
    {
        float lineWidth = lineRenderer.startWidth;
        if (currentDistance - originDistance >= 3f)
        {
            Lab_1_EventsManager.Instance.GameOver(true);
            Debug.Log("Rope is broken!!!");
        }
    }
}
