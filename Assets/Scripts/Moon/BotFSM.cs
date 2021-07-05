using System;
using UnityEngine;

public class BotFSM : MonoBehaviour
{

    //FSM���� ���� �����ϱ�
    //Respawn, Move, Die, Damaged, ����

    GameObject target;
    public float attackDistance = 5;



    //Animator > Animator Controller > Animation Clips
    public Animator anim;

    //����
    public GameObject BotbulletFactory;
    public GameObject BotFirePosition;
    public Vector3 dir;

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

        transform.position = BP.transform.position;

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

        else if (state == State.Die)
        {
            UpdateDie();
        }
    }





    private void UpdateRespawn()
    {
        //�¾�� ������ �ִϸ��̼� ���
        //�ִϸ��̼��� ������ Move���·� ����
        state = State.Move;
        anim.SetTrigger("Move");
    }

    public GameObject BP;
    public int BotSpeed = 1;
    public int Botdist = 20;
    private void UpdateMove()
    {
        //������ �������� �̵��ϱ�
        transform.position += transform.forward * Time.deltaTime * BotSpeed;
        if ((Vector3.Distance(BP.transform.position, transform.position)) > Botdist)
        {
            BP.transform.position = transform.position;
            transform.Rotate(0, -90, 0);
        }

        //���� target�� �Ÿ��� ���ؼ�
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //���� �� �Ÿ��� ���� �Ÿ����� ������
        if (distance < attackDistance)
        {
            //Attack ���·� ����
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }
    private void UpdateDamaged()
    {
        //�������� ������

        //������ �ִϸ��̼� ���

        //������ �ִϸ��̼��� ������ Move���·� ����
        state = State.Move;
        anim.SetTrigger("Move");
        //hp�� �� ������ die ���·� ����
        // state = State.Die;
    }


    internal void OnEventAttack()
    {
        //�Ѿ� ����
        GameObject BotBullet = Instantiate(BotbulletFactory);
        //Bullet�� �ѱ���
        BotBullet.transform.position = BotFirePosition.transform.position;
        //bullet ����
        dir = target.transform.position - transform.position;
        dir.Normalize();

        //����  target�� �Ÿ��� ���ؼ�
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //���� �� �Ÿ��� ���ݰŸ����� ũ��
        if (distance > attackDistance)
        {
            //Move���·� ����
            state = State.Move;
            anim.SetTrigger("Move");
        } else
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    private void UpdateDie()
    {

        anim.SetTrigger("Die");
        Destroy(gameObject, 4f);
    }

}
