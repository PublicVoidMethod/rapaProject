using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryPosition : MonoBehaviour
{
    public GameObject recoveryField;

    void Start()
    {
        
    }

    void Update()
    {
        // ȸ����ų(RecoveryField) eŰ�� ������
        if(Input.GetKeyDown(KeyCode.E))
        {
            // ȸ�� ��ų �ڽ��� �����Ѵ�.
            GameObject go = Instantiate(recoveryField);
            // ������ ȸ�� ��ų �ڽ��� ���� ��ġ�� ��ġ��Ų��.
            go.transform.position = transform.position;
        }
    }
}
