using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBillboard : MonoBehaviour
{
    public bool isUI = false;

    float dir;

    void Start()
    {
        dir = isUI == true ? 1.0f : -1.0f;
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward * dir;
    }
}
