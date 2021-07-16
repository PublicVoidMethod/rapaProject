using UnityEngine;

public class BotManager : MonoBehaviour
{

    public GameObject BotFactory;

    public BotFSM botfsm;
    GameObject Bot;

    void Start()
    {
        //공장에서 봇 생성
        Bot = Instantiate(BotFactory, transform.position, transform.rotation);
       // Bot = Instantiate(BotFactory, transform.position, transform.rotation);
      //  Bot = Instantiate(BotFactory, transform.position, transform.rotation);
    }

    void Update()
    {
        if (botfsm.state == BotFSM.State.Die)
        {
            //공장에서 봇 생성
            Bot = Instantiate(BotFactory, transform.position, transform.rotation);

        }
    }
}