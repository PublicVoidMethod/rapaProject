using UnityEngine;


//�¾ �� �ִ�ü��
//�÷��̾ ���� ������ �� ü�� ����
//ü���� ����Ǹ� UI�� �ݿ�

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
