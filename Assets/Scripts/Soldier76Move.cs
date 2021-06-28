using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier76Move : MonoBehaviour
{
    public float walkSpeed = 7.0f;
    public float runSpeed = 9.0f;
    public float gravity = -15.0f;

    CharacterController cc;
    float yVelocity = 0;
    float jumpPower = 3.0f;
    float jumpCount = 1.0f;

    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        // 전후좌우의 입력을 받아서
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 전후좌우의 방향을 만들고
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        // 월드의 정면을 메인 카메라의 정면으로 바꾼다.
        dir = Camera.main.transform.TransformDirection(dir);

        // 캐릭터컨트롤러의 아래쪽이 닿았다면
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // yVelocity의 값을 0으로 초기화하고, 점프카운트를 1로 초기화한다.
            yVelocity = 0;
            jumpCount = 1;
        }

        // 점프키를 누르면
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            // 위로 점프파워의 힘만큼 주고싶다.
            yVelocity = jumpPower;
            // 점프의 횟수를 1번으로 제한한다.
            jumpCount--;
        }

        // 방향의 y값을 누적하고 싶다.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // 캐릭터컨트롤러로 움직이고 싶다.
        cc.Move(dir * walkSpeed * Time.deltaTime);

        // shift키를 누르고 있을 때
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 워크스피드를 런스피드로 한다.
            walkSpeed = runSpeed;
        }
        else
        {
            walkSpeed = 7.0f;
        }
    }
}
