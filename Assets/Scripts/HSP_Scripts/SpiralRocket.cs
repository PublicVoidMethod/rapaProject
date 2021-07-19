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
        // 우클릭 파괴 효과를 생성한다.
        GameObject rightShootObj = Instantiate(rightShootEffect);
        // 생성한 효과를 충돌된 위치에 가져놓는다.
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
