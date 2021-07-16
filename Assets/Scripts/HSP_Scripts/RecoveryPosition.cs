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
        // eŰ�� ������ ��Ŀ���� �ڽ��� �����ȴ�.
        // ��Ŀ���� �ڽ��� �����Ǹ� 10���� ��Ÿ���� ������ �ȴ�.
    }

    void Update()
    {
        // ȸ����ų(RecoveryField) eŰ�� ������
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (!isRecoveryDelay)
            {
                isRecoveryDelay = true;
                RecoveryInstantiate();
                StartCoroutine(RecoveryDelayCoroutine());
            }
            // ������ ȸ�� ��ų �ڽ��� ���� ��ġ�� ��ġ��Ų��.
            //go.transform.position = transform.position;
        }
        //else if(isRecoveryDelay )
        //{
        //    StartCoroutine(RecoveryDelayCoroutine());
        //}
            //isRecoveryDelay = true;

        if(go != null)
        {
            // �Ʒ��� ������ ������ �̵��Ѵ�.
            Vector3 dir = Vector3.down;
            cc.Move(dir * dropSpeed * Time.deltaTime);

            if(cc.collisionFlags == CollisionFlags.Below)
            {
                //go.transform.GetChild(0).gameObject.SetActive(true);
                //go.gameObject.GetComponent<BoxCollider>().gameObject.SetActive(true); <- �ȵ�
                go.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    void RecoveryInstantiate()
    {
        // ȸ�� ��ų �ڽ��� �����Ѵ�.
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
