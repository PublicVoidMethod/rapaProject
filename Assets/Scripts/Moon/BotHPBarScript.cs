using UnityEngine;
using UnityEngine.UI;

public class BotHPBarScript : MonoBehaviour
{

    public float MaxHP = 10;
    public float HP = 10;
    public GameObject BotHPBar;
    BotFSM Bfsm;




    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        //Bfsm = GameObject.Find("Bot").GetComponent<BotFSM>();
        Bfsm = GameObject.Find("Bot(Clone)").GetComponent<BotFSM>();
        BotHPBar = GameObject.Find("BotHPBar");
    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetButtonDown("Fire2"))
        {
            HP -= 1;
            BotHPBar.GetComponent<Image>().fillAmount = HP/MaxHP;
            Bfsm.state = BotFSM.State.Damaged;
        }
        if (HP == 0)
        {
            Bfsm.state = BotFSM.State.Die;
            HP = -1;
        }*/

    }


    public void BotGetDamaged(int damage)
    {
        Bfsm.state = BotFSM.State.Damaged;
        HP -= damage;
        BotHPBar.GetComponent<Image>().fillAmount = HP / MaxHP;


        if (HP == 0)
        {
            Bfsm.state = BotFSM.State.Die;
            HP = -1;
        }
    }
}
