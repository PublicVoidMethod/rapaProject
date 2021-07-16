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

        // 생성되고 5초 후에 회복 스킬 박스가 파괴된다.
        if(currentTime >= destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
