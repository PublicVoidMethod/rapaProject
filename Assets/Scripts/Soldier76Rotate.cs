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
        // 마우스의 입력을 받고
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        // x, y값를 누적시킨다.
        if(rotateY == true)
        {
            rotX += x * rotSpeed * Time.deltaTime;
        }
        if(rotateX == true)
        {
            rotY += y * rotSpeed * Time.deltaTime;
        }

        // x값을 고정시킨다.
        rotY = Mathf.Clamp(rotY, -60, 60);

        // 방향을 만들고
        Vector3 dir = new Vector3(-rotY, rotX, 0);
        transform.eulerAngles = dir;
    }
}
