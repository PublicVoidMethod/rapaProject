using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralRocket : MonoBehaviour
{
    public BotHPBarScript botHP;

    private void Start()
    {
        botHP = GameObject.Find("Bot").GetComponent<BotHPBarScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bot"))
        {
            botHP.BotGetDamaged(5);
        }
        Destroy(gameObject);
    }
}
