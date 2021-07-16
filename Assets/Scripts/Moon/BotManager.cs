using System.Collections;
using UnityEngine;

public class BotManager : MonoBehaviour
{

    public GameObject BotFactory;
    public BotFSM botfsm;
    GameObject Bot;


    private void Start()
    {
        //공장에서 봇 생성
        Bot = Instantiate(BotFactory, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (botfsm.state == BotFSM.State.Die)
        {
            //공장에서 봇 생성
            Bot = Instantiate(BotFactory, transform.position, transform.rotation);
        }
    }
}
