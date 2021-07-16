
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{

    public GameObject BotFactory;

    public BotFSM botfsm;
    GameObject Bot;

    void Start()
    {
        //공장에서 봇 생성
        Bot = Instantiate(BotFactory, GetRandomPosition(), transform.rotation);
    }

    void Update()
    {
        if (botfsm.state == BotFSM.State.Die)
        {
            print("e");
            //공장에서 봇 생성
            Instantiate(BotFactory, GetRandomPosition(), transform.rotation);
        }
    }

    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(20f, 22f);
        float z = Random.Range(8f,27f);

        Vector3 result = new Vector3(x, transform.position.y, z);
        return result;
    }

}