using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    private float destoryTimer = 0f;
    void Update()
    {
        destoryTimer += Time.deltaTime;
        if (destoryTimer >= 0.6f) Destroy(gameObject);
    }
}
