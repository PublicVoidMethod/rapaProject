using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFire : MonoBehaviour
{


    public GameObject BotbulletFactory;
    public GameObject BotFirePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject BotBullet = Instantiate(BotbulletFactory);
        BotBullet.transform.position = BotFirePosition.transform.position;
    }
}
