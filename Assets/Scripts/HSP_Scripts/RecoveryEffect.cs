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
        // 아래의 방향을 가지고 이동한다.
        Vector3 dir = Vector3.down;
        //cc.Move(dir * dropSpeed * Time.deltaTime);
        transform.position += dir * dropSpeed * Time.deltaTime;

        // 회복 스킬 박스가 밑의 부분에 닿으면
        //if (cc.collisionFlags == CollisionFlags.Below)
        //{
        //    // 회복 스킬 박스가 멈춘다.
        //    dir = Vector3.zero;
            // 정해진 크기의 원형 레이가 생성된다
            // 만약 생성된 범위 레이에 안에 들어오면 회복 함수를 실행한다.
        //}
    }

    public void Recovery()
    {

    }
}
