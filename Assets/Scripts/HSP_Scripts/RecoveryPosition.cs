using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryPosition : MonoBehaviour
{
    public GameObject recoveryField;
    public float dropSpeed = 5.0f;
    [SerializeField]
    GameObject go;
    CharacterController cc;
    UnityEngine.BoxCollider boxCol;

    bool isRecoveryDelay = true;
    float recoveryCoolTime = 6f;

    void Start()
    {
        isRecoveryDelay = false;
        // e키를 누르면 리커버리 박스가 생성된다.
        // 리커버리 박스가 생성되면 10초의 쿨타임을 가지게 된다.
    }

    void Update()
    {
        // 회복스킬(RecoveryField) e키를 누르면
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (!isRecoveryDelay)
            {
                isRecoveryDelay = true;
                RecoveryInstantiate();
                StartCoroutine(RecoveryDelayCoroutine());
            }
            // 생성한 회복 스킬 박스를 나의 위치에 위치시킨다.
            //go.transform.position = transform.position;
        }
        //else if(isRecoveryDelay )
        //{
        //    StartCoroutine(RecoveryDelayCoroutine());
        //}
            //isRecoveryDelay = true;

        if(go != null)
        {
            // 아래의 방향을 가지고 이동한다.
            Vector3 dir = Vector3.down;
            cc.Move(dir * dropSpeed * Time.deltaTime);

            if(cc.collisionFlags == CollisionFlags.Below)
            {
                //go.transform.GetChild(0).gameObject.SetActive(true);
                //go.gameObject.GetComponent<BoxCollider>().gameObject.SetActive(true); <- 안됨
                go.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    void RecoveryInstantiate()
    {
        // 회복 스킬 박스를 생성한다.
        go = Instantiate(recoveryField, transform.position, Quaternion.identity);
        cc = go.GetComponent<CharacterController>();
        //isRecoveryDelay = true;
    }
    
    IEnumerator RecoveryDelayCoroutine()
    {
        yield return new WaitForSeconds(recoveryCoolTime);
        isRecoveryDelay = false;
    }
}
