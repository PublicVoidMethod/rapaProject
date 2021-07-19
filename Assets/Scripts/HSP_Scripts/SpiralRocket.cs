using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralRocket : MonoBehaviour
{
    public BotHPBarScript botHP;
    //public GameObject rightShootEffect;
    public GameObject rightShootEffect;

    private void Start()
    {

    }

    private void OnDestroy()
    {
        // ��Ŭ�� �ı� ȿ���� �����Ѵ�.
        GameObject rightShootObj = Instantiate(rightShootEffect);
        // ������ ȿ���� �浹�� ��ġ�� �������´�.
        rightShootObj.transform.position = transform.position;
        //Debug.Log($"rightShootObj : {rightShootObj.transform.position}");
        //Debug.Log($"collision : {collision.transform.position}");
        ParticleSystem ps = rightShootObj.GetComponent<ParticleSystem>();
        ps.Stop();
        ps.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("!!!!!!!!!!!!!!");
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
