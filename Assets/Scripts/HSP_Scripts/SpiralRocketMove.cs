using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralRocketMove : MonoBehaviour
{
    public float locketSpeed = 10.0f;

    Vector3 dir;

    void Start()
    {
        // 정면 방향을 만들고
        dir = new Vector3(0, 0, 1);

        // 메인 카메라의 정면 방향으로 나아가고 싶다.
        dir = Camera.main.transform.forward;
    }

    void Update()
    {
        // 이동하고 싶다.
        transform.position += dir * locketSpeed * Time.deltaTime;
    }
}
