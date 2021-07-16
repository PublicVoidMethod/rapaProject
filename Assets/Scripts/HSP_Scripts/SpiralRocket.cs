using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralRocket : MonoBehaviour
{
    public BotHPBarScript botHP;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bot")
        {
            // 원형의 레이를 생성하고
            Collider[] cols = Physics.OverlapSphere(transform.position, 5.0f);
            for(int i = 0; i < cols.Length; i++)
            {
                if(cols[i].gameObject.tag == "Bot")
                {
                    botHP = cols[i].gameObject.GetComponent<BotHPBarScript>();
                    // 원형 레이의 범위 내에 Bot이 있다면 데미지를 준다.
                    botHP.BotGetDamaged(5);
                }
            }
        }
        Destroy(gameObject);
    }
}
