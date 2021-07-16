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
            // ������ ���̸� �����ϰ�
            Collider[] cols = Physics.OverlapSphere(transform.position, 5.0f);
            for(int i = 0; i < cols.Length; i++)
            {
                if(cols[i].gameObject.tag == "Bot")
                {
                    botHP = cols[i].gameObject.GetComponent<BotHPBarScript>();
                    // ���� ������ ���� ���� Bot�� �ִٸ� �������� �ش�.
                    botHP.BotGetDamaged(5);
                }
            }
        }
        Destroy(gameObject);
    }
}
