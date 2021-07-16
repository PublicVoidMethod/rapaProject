using System.Collections;
using UnityEngine;

public class BotManager : MonoBehaviour
{

    public GameObject BotFactory;
    public BotFSM botfsm;
    GameObject Bot;


    private void Start()
    {
        //���忡�� �� ����
        Bot = Instantiate(BotFactory, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (botfsm.state == BotFSM.State.Die)
        {
            //���忡�� �� ����
            Bot = Instantiate(BotFactory, transform.position, transform.rotation);
        }
    }
}
