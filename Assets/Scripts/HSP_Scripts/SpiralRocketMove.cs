using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralRocketMove : MonoBehaviour
{
    public float locketSpeed = 10.0f;

    Vector3 dir;

    void Start()
    {
        // ���� ������ �����
        dir = new Vector3(0, 0, 1);

        // ���� ī�޶��� ���� �������� ���ư��� �ʹ�.
        dir = Camera.main.transform.forward;
    }

    void Update()
    {
        // �̵��ϰ� �ʹ�.
        transform.position += dir * locketSpeed * Time.deltaTime;
    }
}
