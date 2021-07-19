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
    public float dotHeal = 0.1f; // ��Ʈ���� ��

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
    //bool isRecoveryChecked = true; // �����ǿ� �������� Ȯ�����ϴ� ����(true : �� ������ ���� ��, false : �� ������ ����� ��)
    bool isRecoverySwitch = true; // �������� ȸ�� �ڷ�ƾ�� ���� �ݴ� ����(true : �ڷ�ƾ���� ����, false : �ڷ�ƾ �ݱ�)
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

        // �÷��̾��� ĸ�� �ݶ��̴��� �����´�
        //capsule = gameObject.GetComponent<CapsuleCollider>();
    }
    public float GetHP()
    {
        return hP;
    }

    void LateUpdate()
    {
        
        // �����¿��� �Է��� �޴´�.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // �Է¹��� �����¿��� ������ �����.
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        moveSpeed = walkSpeed;
        float animSpeed = 0;
        
        if(dir.magnitude > 0)
        {
            // shiftŰ�� ������ ��
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                #region ��Ȳ�ϰ� �޸���
                // ���� isRun(true) �޸��� ���·� �Ѵ�. ����Ʈ�� ������ �� �޸��� �ִٸ� �Ȱ� �ٲ۴�. ���� �Ȱ� �ִٸ� �޸��� �ٲ۴�.
                //if (isRun) 
                //{ 
                //    isRun = false;
                //} 
                //else
                //{
                //    // �ٽ��ѹ� shift�� ������ �� isRun(false) �ȱ� ���·� �����.
                //    isRun = true;
                //}
                #endregion
                isRun = !isRun;
            }

            // isRun(true)��� ��ũ���ǵ忡 �����ǵ带 �־��ְ�
            if (isRun == true)
            {
                //walkSpeed = runSpeed;
                animSpeed = moveSpeed = dir.magnitude * runSpeed;
            }
            else // �׷��� �ʴٸ� ��ũ���ǵ带 �ʱ�ȭ�Ѵ�.
            {
                //walkSpeed = 7.0f;
                animSpeed = moveSpeed = dir.magnitude * walkSpeed;
            }

            // ���� Ctrl Ű�� �Է��ϸ� �ɴ´�.
            if (Input.GetKey(KeyCode.LeftControl))
            {
                //isCrouch = true;
                OnUpDown(true);
                //walkSpeed = crouchWalkSpeed;
                animSpeed = moveSpeed = dir.magnitude * crouchWalkSpeed;

                // ĸ�� �ݶ��̴��� y �� ����� ���δ�.
            }
            else
            {
                //isCrouch = false;
                OnUpDown(false);
                //moveSpeed = dir.magnitude * walkSpeed;
            }
        }

        // ���� ī�޶� �ٶ󺸴� ������ ���� �������� �ٲ۴�.
        dir = Camera.main.transform.TransformDirection(dir);

        // ĳ������Ʈ�ѷ��� �ؿ� �κ��� ����ִٸ�
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // yVelocity�� 0���� �ʱ�ȭ�ϰ�, ����ī��Ʈ�� 1�� �ʱ�ȭ�Ѵ�.
            yVelocity = 0;
            jumpCount = 1;
        }

        // ����Ű(�����̽���)�� ������, ����ī��Ʈ�� 0���� Ŭ��
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            // yVelocity�� �����Ŀ��� �ְ�.
            yVelocity = jumpPower;
            // ����ī��Ʈ�� �ϳ��� ���δ�.
            jumpCount--;
        }
        
        // yVelocity�� �߷°��� ������Ų��.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        
        // ĳ������Ʈ�ѷ��� �̵���Ų��.
        //moveSpeed = dir.magnitude * walkSpeed;
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // ������ ���� �ӵ��� Animator�� "SoldierSpeed" �Ķ���Ϳ� �����Ѵ�.
        soldieranim.SetFloat("SoldierSpeed", animSpeed / runSpeed);

        //// ������ ���Ĺ� ���Ⱚ�� �¿� ���Ⱚ�� animator �Ķ���Ϳ� �����Ѵ�.
        //soldieranim.SetFloat("Speed_V", v);
        //soldieranim.SetFloat("Speed_H", h);
    }

    /// <summary>
    /// �ɱ�� ���� ������ �Լ�
    /// </summary>
    /// <param name="isCrouch"></param>
    private void OnUpDown(bool isCrouch)
    {
        Transform targetTransform;
        // �ɱ��� ���°� true���
        if (isCrouch)
        {
            // true��� �ɱ� ����
            targetTransform = crouchTransform;
            //walkSpeed = crouchWalkSpeed;

        }
        else
        {
            // false��� ���ִ� ����
            targetTransform = standTransform;
            //walkSpeed = 7.0f;
        }
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetTransform.position, crouchSpeed * Time.deltaTime);
        //Debug.Log(Camera.main.transform.position);
    }

    /// <summary>
    /// ĳ���� ��Ʈ�ѷ��� �浹�� ������ ó���ϴ� �Լ�
    /// </summary>
    /// <param name="hit"></param>
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // ��Ŀ���� �ʵ��� �浹�� �����ϸ�
        if (hit.gameObject.tag == "Recovery Field" && isRecoverySwitch)
        {
            isRecoverySwitch = false;
            print(hit.gameObject.name);

            //��Ʈ���� �ִ� �ڸ�ƾ�� �����Ѵ�.
            StartCoroutine(AddDotHealCoroutine());
        }
    }
    IEnumerator AddDotHealCoroutine()
    {
        // 5���� �ð����� ��Ʈ���� 0.5�ʿ� �ѹ��� ȸ���Ѵ�.
        for(int i = 0; i < 10; i++)
        {
            // �� ����ü���� �ִ�ü�º��� ���ų� ������
            if (hP >= maxHP)
            {
                // ����ü�¿� �ִ�ü���� �ִ´�
                hP = maxHP;
            }
            // �� ����ü���� �ִ�ü�º��� ������
            else if (hP < maxHP)
            {
                // �ִ�ü���� ��Ʈ���� ���Ѵ�?
                hP += maxHP * dotHeal;
                Debug.Log("���� ü�� : " + hP);
            }
            // �ڷ�ƾ�� �����ϰ�, ���ϴ� �Ϳ� ���ؼ� 0.5�� ��ٸ���.
            yield return new WaitForSeconds(0.5f);
        }
        isRecoverySwitch = true;
    }
}
