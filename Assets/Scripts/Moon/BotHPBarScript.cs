using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        Bfsm = GetComponent<BotFSM>();
        MouseAttack.instance.botHPScript = this;
        //Bfsm = GameObject.Find("Bot").GetComponent<BotFSM>();
        //Bfsm = GameObject.Find("Bot(Clone)").GetComponent<BotFSM>();
        //BotHPBar = GameObject.Find("BotHPBar");
    }

    // Update is called once per frame
    void Update()
    {
        if (Bfsm == null) Bfsm = GetComponent<BotFSM>(); //GameObject.Find("Bot(Clone)").GetComponent<BotFSM>();
        if (BotHPBar == null) gameObject.transform.Find("Canvas").Find("BotHPBarBackground").Find("BotHPBar");
        /*if (BotHPBar == null) {
            BotHPBar = GameObject.Find("BotHPBar");
            HP = MaxHP;
        }*/
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
        Debug.Log(">> BotHPBar");
        Bfsm.state = BotFSM.State.Damaged;
        HP -= damage;
        /*if(BotHPBar == null) BotHPBar = GameObject.Find("BotHPBar");
        Debug.Log(BotHPBar == null);*/
        BotHPBar.GetComponent<Image>().fillAmount = HP / MaxHP;


        if (HP == 0)
        {
            Bfsm.state = BotFSM.State.Die;
            HP = -1;
        }
     
    }
 

}
