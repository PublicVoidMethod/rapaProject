using UnityEngine;

public class BotFSM : MonoBehaviour
{

    //FSM���� ���� �����ϱ�
    //Respawn, Move, Die, Damaged, ����

    GameObject target;
    public float attackDistance = 5;
    float currentTime;
    float attackTime = 1;

    public enum State
    {
        Respawn,
        Move,
        Damaged,
        Attack,
        Die,
    }

    public State state;
    BotHP bhp;


    // Start is called before the first frame update
    void Start()
    {
        state = State.Respawn;
        target = GameObject.Find("Soldier76_Player");


    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Respawn)
        {
            UpdateRespawn();
        }
        else if (state == State.Move)
        {
            UpdateMove();
        }
        else if (state == State.Damaged)
        {
            UpdateDamaged();
        }

        else if (state == State.Attack)
        {
            UpdateAttack();
        }
        else if (state == State.Die)
        {
            UpdateDie();
        }
    }





    private void UpdateRespawn()
    {
        //�¾�� ������ �ִϸ��̼� ���
    }
    private void UpdateMove()
    {
        //������ �ִϸ��̼� �� ������ �������� �̵��ϱ�

 
        //���� target�� �Ÿ��� ���ؼ�
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //���� �� �Ÿ��� ���� �Ÿ����� ������
        if (distance < attackDistance)
        {
            //Attack ���·� ����
            state = State.Attack;
        }
    }
    private void UpdateDamaged()
    {
        //�������� ������
        //������ �ִϸ��̼� ���
        //

        //hp�� �� ������ die ���·� ����
        state = State.Die;
    }
    private void UpdateAttack()
    {
        //�ð��� �帣�ٰ�
        currentTime += Time.deltaTime;
        //����ð��� ���ݽð��� �Ǹ�
        if (currentTime > attackTime)
        {
            //���� �ð� �ʱ�ȭ
            currentTime = 0;
            //�÷��̾ ����
            //target.AddDamage();
            //����  target�� �Ÿ��� ���ؼ�
            float distance = Vector3.Distance(transform.position, target.transform.position);
            //���� �� �Ÿ��� ���ݰŸ����� ũ��
            if (distance > attackDistance)
            {
                //Move���·� ����
                state = State.Move;
            }

        }

    }
    private void UpdateDie()
    {
       // Destroy(gameObject.Bot);
    }

}
