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

        // �÷��̾��� ĸ�� �ݶ��̴��� �����´�
        capsule = gameObject.GetComponent<CapsuleCollider>();
    }

    void LateUpdate()
    {
        
        // �����¿��� �Է��� �޴´�.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // �Է¹��� �����¿��� ������ �����.
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

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
        cc.Move(dir * walkSpeed * Time.deltaTime);

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
            walkSpeed = runSpeed;
        }
        else // �׷��� �ʴٸ� ��ũ���ǵ带 �ʱ�ȭ�Ѵ�.
        {
            walkSpeed = 7.0f;
        }

        // ���� Ctrl Ű�� �Է��ϸ� �ɴ´�.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //isCrouch = true;
            OnUpDown(true);
            
            // ĸ�� �ݶ��̴��� y �� ����� ���δ�.
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
        // �ɱ��� ���°� false�� �ƴ϶��
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
