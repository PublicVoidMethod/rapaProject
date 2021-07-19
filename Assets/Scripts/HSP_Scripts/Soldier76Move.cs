using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier76Move : MonoBehaviour
{
    public static Soldier76Move instance;

    public float crouchWalkSpeed = 3.0f;
    public float walkSpeed = 7.0f;
    public float runSpeed = 9.0f;
    public float gravity = -15.0f;
    public float crouchSpeed = 3.0f;
    public Transform standTransform;
    public Transform crouchTransform;
    public float maxHP = 200f;
    public float dotHeal = 0.1f; // 도트힐의 양

    CharacterController cc;
    Animator soldieranim;
    //CapsuleCollider capsule;

    float moveSpeed = 0f;
    float yVelocity = 0f;
    float jumpPower = 3.0f;
    float jumpCount = 1.0f;
    bool isRun = false;
    [SerializeField]
    public float hP = 0;
    //bool isRecoveryChecked = true; // 힐장판에 들어갔는지를 확인한하는 변수(true : 힐 장판을 들어갔을 때, false : 힐 장판을 벗어났을 때)
    bool isRecoverySwitch = true; // 힐장판의 회복 코루틴을 열고 닫는 변수(true : 코루틴으로 열기, false : 코루틴 닫기)
    //bool isCrouch = false;
    //float standPosY = 0f;
    //float crouchPosY = 0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    void Start()
    {
        cc = transform.GetComponent<CharacterController>();

        hP = maxHP;

        soldieranim = GetComponentInChildren<Animator>();

        //standPosY = Camera.main.transform.position.y;
        //crouchPosY = standPosY - 0.44f;
        //standVector = Camera.main.transform.position;
        //crouchVector = new Vector3(0, standVector.y - 0.44f, 0);

        // 플레이어의 캡슐 콜라이더를 가져온다
        //capsule = gameObject.GetComponent<CapsuleCollider>();
    }
    public float GetHP()
    {
        return hP;
    }

    void LateUpdate()
    {
        
        // 전후좌우의 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 입력받은 전후좌우의 방향을 만든다.
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        moveSpeed = walkSpeed;
        float animSpeed = 0;
        
        if(dir.magnitude > 0)
        {
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
                //walkSpeed = runSpeed;
                animSpeed = moveSpeed = dir.magnitude * runSpeed;
            }
            else // 그렇지 않다면 워크스피드를 초기화한다.
            {
                //walkSpeed = 7.0f;
                animSpeed = moveSpeed = dir.magnitude * walkSpeed;
            }

            // 왼쪽 Ctrl 키를 입력하면 앉는다.
            if (Input.GetKey(KeyCode.LeftControl))
            {
                //isCrouch = true;
                OnUpDown(true);
                //walkSpeed = crouchWalkSpeed;
                animSpeed = moveSpeed = dir.magnitude * crouchWalkSpeed;

                // 캡슐 콜라이더의 y 축 사이즈를 줄인다.
            }
            else
            {
                //isCrouch = false;
                OnUpDown(false);
                //moveSpeed = dir.magnitude * walkSpeed;
            }
        }

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
        //moveSpeed = dir.magnitude * walkSpeed;
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // 솔저의 현재 속도를 Animator의 "SoldierSpeed" 파라미터에 전달한다.
        soldieranim.SetFloat("SoldierSpeed", animSpeed / runSpeed);

        //// 솔저의 전후방 방향값과 좌우 방향값을 animator 파라미터에 전달한다.
        //soldieranim.SetFloat("Speed_V", v);
        //soldieranim.SetFloat("Speed_H", h);
    }

    /// <summary>
    /// 앉기와 서는 상태의 함수
    /// </summary>
    /// <param name="isCrouch"></param>
    private void OnUpDown(bool isCrouch)
    {
        Transform targetTransform;
        // 앉기의 상태가 true라면
        if (isCrouch)
        {
            // true라면 앉기 상태
            targetTransform = crouchTransform;
            //walkSpeed = crouchWalkSpeed;

        }
        else
        {
            // false라면 서있는 상태
            targetTransform = standTransform;
            //walkSpeed = 7.0f;
        }
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetTransform.position, crouchSpeed * Time.deltaTime);
        //Debug.Log(Camera.main.transform.position);
    }

    /// <summary>
    /// 캐릭터 컨트롤러의 충돌을 감지를 처리하는 함수
    /// </summary>
    /// <param name="hit"></param>
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // 리커버리 필드의 충돌을 감지하면
        if (hit.gameObject.tag == "Recovery Field" && isRecoverySwitch)
        {
            isRecoverySwitch = false;
            print(hit.gameObject.name);

            //도트힐을 주는 코르틴을 실행한다.
            StartCoroutine(AddDotHealCoroutine());
        }
    }
    IEnumerator AddDotHealCoroutine()
    {
        // 5초의 시간동안 도트힐을 0.5초에 한번씩 회복한다.
        for(int i = 0; i < 10; i++)
        {
            // 내 현재체력이 최대체력보다 높거나 같으면
            if (hP >= maxHP)
            {
                // 현재체력에 최대체력을 넣는다
                hP = maxHP;
            }
            // 내 현재체력이 최대체력보다 낮으면
            else if (hP < maxHP)
            {
                // 최대체력의 도트힐을 곱한다?
                hP += maxHP * dotHeal;
                Debug.Log("현재 체력 : " + hP);
            }
            // 코루틴을 접근하고, 힐하는 것에 대해서 0.5를 기다린다.
            yield return new WaitForSeconds(0.5f);
        }
        isRecoverySwitch = true;
    }
}
