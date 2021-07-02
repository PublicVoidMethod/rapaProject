using UnityEngine;


//태어날 때 최대체력
//플레이어가 봇을 공격할 때 체력 감소
//체력이 변경되면 UI에 반영

public class BotHP : MonoBehaviour
{
    public int hp;
 //   public int currentHP;
    public int maxHP = 5;
    public GameObject BotHPBarBack;
    public GameObject BotHPBarFill;

    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            
        }
    }

    public void AddDamage()
    {
        HP = HP - 1;

    }
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
