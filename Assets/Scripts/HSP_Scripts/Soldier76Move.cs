using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier76Move : MonoBehaviour
{
    public float walkSpeed = 7.0f;
    public float runSpeed = 9.0f;
    public float gravity = -15.0f;
    public float crouchSpeed = 3.0f;
    public Transform standTransform;
    public Transform crouchTransform;

    CharacterController cc;
    CapsuleCollider capsule;

    float yVelocity = 0f;
    float jumpPower = 3.0f;
    float jumpCount = 1.0f;
    bool isRun = false;
    //bool isCrouch = false;
    //float standPosY = 0f;
    //float crouchPosY = 0f;

    void Start()
    {
        cc = transform.GetComponent<CharacterController>();

        //standPosY = Camera.main.transform.position.y;
        //crouchPosY = standPosY - 0.44f;
        //standVector = Camera.main.transform.position;
        //crouchVector = new Vector3(0, standVector.y - 0.44f, 0);

        // 플레이어의 캡슐 콜라이더를 가져온다
        capsule = gameObject.GetComponent<CapsuleCollider>();
    }

    void LateUpdate()
    {
        
        // 전후좌우의 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 입력받은 전후좌우의 방향을 만든다.
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        // 메인 카메라가 바라보는 방향을 정면 방향으로 바꾼다.
        dir = Camera.main.transform.TransformDirection(dir);

        // 캐릭터컨트롤러의 밑에 부분이 닿아있다면
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // yVelocity를 0으로 초기화하고, 점프카운트를 1로 초기화한다.
            yVelocity = 0;
            jumpCount = 1;
        }

        // 점프키(스페이스바)를 누르고, 점프카운트가 0보다 클때
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            // yVelocity에 점프파워를 넣고.
            yVelocity = jumpPower;
            // 점프카운트를 하나씩 줄인다.
            jumpCount--;
        }

        // yVelocity에 중력값을 누적시킨다.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // 캐릭터컨트롤러로 이동시킨다.
        cc.Move(dir * walkSpeed * Time.deltaTime);

        // shift키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            #region 장황하게 달리기
            // 나는 isRun(true) 달리기 상태로 한다. 쉬프트를 눌렀을 때 달리고 있다면 걷게 바꾼다. 만약 걷고 있다면 달리게 바꾼다.
            //if (isRun) 
            //{ 
            //    isRun = false;
            //} 
            //else
            //{
            //    // 다시한번 shift를 눌렀을 때 isRun(false) 걷기 상태로 만든다.
            //    isRun = true;
            //}
            #endregion
            isRun = !isRun;
        }

        // isRun(true)라면 워크스피드에 런스피드를 넣어주고
        if (isRun == true)
        {
            walkSpeed = runSpeed;
        }
        else // 그렇지 않다면 워크스피드를 초기화한다.
        {
            walkSpeed = 7.0f;
        }

        // 왼쪽 Ctrl 키를 입력하면 앉는다.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //isCrouch = true;
            OnUpDown(true);
            
            // 캡슐 콜라이더의 y 축 사이즈를 줄인다.
        }
        else 
        {
            //isCrouch = false;
            OnUpDown(false);
        }
    }

    private void OnUpDown(bool isCrouch)
    {
        Transform targetTransform;
        // 앉기의 상태가 false가 아니라면
        if (isCrouch)
        {
            targetTransform = crouchTransform;
        }
        else
        {
            targetTransform = standTransform;
        }
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetTransform.position, crouchSpeed * Time.deltaTime);
    }
}
