using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier76Rotate : MonoBehaviour
{
    public float rotSpeed = 300.0f;
    public bool rotateX;
    public bool rotateY;

    float rotX = 0;
    float rotY = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺�� �Է��� �ް�
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        // x, y���� ������Ų��.
        if(rotateY == true)
        {
            rotX += x * rotSpeed * Time.deltaTime;
        }
        if(rotateX == true)
        {
            rotY += y * rotSpeed * Time.deltaTime;
        }

        // x���� ������Ų��.
        rotY = Mathf.Clamp(rotY, -60, 60);

        // ������ �����
        Vector3 dir = new Vector3(-rotY, rotX, 0);
        transform.eulerAngles = dir;
    }
}
