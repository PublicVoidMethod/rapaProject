using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryEffect : MonoBehaviour
{
    public float coolTime = 10.0f;
    public float elapsedTime = 5.0f;

    //CharacterController cc;

    //float currentTime = 0f;
    public float dropSpeed = 1.0f;

    void Start()
    {
        //cc = transform.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // �Ʒ��� ������ ������ �̵��Ѵ�.
        Vector3 dir = Vector3.down;
        //cc.Move(dir * dropSpeed * Time.deltaTime);
        transform.position += dir * dropSpeed * Time.deltaTime;

        // ȸ�� ��ų �ڽ��� ���� �κп� ������
        //if (cc.collisionFlags == CollisionFlags.Below)
        //{
        //    // ȸ�� ��ų �ڽ��� �����.
        //    dir = Vector3.zero;
            // ������ ũ���� ���� ���̰� �����ȴ�
            // ���� ������ ���� ���̿� �ȿ� ������ ȸ�� �Լ��� �����Ѵ�.
        //}
    }

    public void Recovery()
    {

    }
}
