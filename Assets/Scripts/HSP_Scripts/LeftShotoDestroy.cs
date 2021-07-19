using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftShotoDestroy : MonoBehaviour
{
    public float destroyTime = 4f;

    float currentTime = 0f;

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= destroyTime) Destroy(gameObject);
    }
}
