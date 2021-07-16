using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryZone : MonoBehaviour
{
    public float destroyTime = 5;

    float currentTime = 0;

    void Update()
    {
        currentTime += Time.deltaTime;

        // �����ǰ� 5�� �Ŀ� ȸ�� ��ų �ڽ��� �ı��ȴ�.
        if(currentTime >= destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
