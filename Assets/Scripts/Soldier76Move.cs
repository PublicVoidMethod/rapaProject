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
        // �����¿��� �Է��� �޾Ƽ�
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // �����¿��� ������ �����
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        // ������ ������ ���� ī�޶��� �������� �ٲ۴�.
        dir = Camera.main.transform.TransformDirection(dir);

        // ĳ������Ʈ�ѷ��� �Ʒ����� ��Ҵٸ�
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // yVelocity�� ���� 0���� �ʱ�ȭ�ϰ�, ����ī��Ʈ�� 1�� �ʱ�ȭ�Ѵ�.
            yVelocity = 0;
            jumpCount = 1;
        }

        // ����Ű�� ������
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            // ���� �����Ŀ��� ����ŭ �ְ�ʹ�.
            yVelocity = jumpPower;
            // ������ Ƚ���� 1������ �����Ѵ�.
            jumpCount--;
        }

        // ������ y���� �����ϰ� �ʹ�.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // ĳ������Ʈ�ѷ��� �����̰� �ʹ�.
        cc.Move(dir * walkSpeed * Time.deltaTime);

        // shiftŰ�� ������ ���� ��
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // ��ũ���ǵ带 �����ǵ�� �Ѵ�.
            walkSpeed = runSpeed;
        }
        else
        {
            walkSpeed = 7.0f;
        }
    }
}
