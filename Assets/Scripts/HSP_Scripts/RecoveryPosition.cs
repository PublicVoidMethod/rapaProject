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
        // 회복스킬(RecoveryField) e키를 누르면
        if(Input.GetKeyDown(KeyCode.E))
        {
            // 회복 스킬 박스를 생성한다.
            GameObject go = Instantiate(recoveryField);
            // 생성한 회복 스킬 박스를 나의 위치에 위치시킨다.
            go.transform.position = transform.position;
        }
    }
}
