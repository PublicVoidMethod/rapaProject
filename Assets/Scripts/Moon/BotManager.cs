using UnityEngine;

public class BotManager : MonoBehaviour
{

    public GameObject BotFactory;

    public BotFSM botfsm;
    GameObject Bot;

    void Start()
    {
        //���忡�� �� ����
        Bot = Instantiate(BotFactory, transform.position, transform.rotation);
       // Bot = Instantiate(BotFactory, transform.position, transform.rotation);
      //  Bot = Instantiate(BotFactory, transform.position, transform.rotation);
    }

    void Update()
    {
        if (botfsm.state == BotFSM.State.Die)
        {
            //���忡�� �� ����
            Bot = Instantiate(BotFactory, transform.position, transform.rotation);

        }
    }
}