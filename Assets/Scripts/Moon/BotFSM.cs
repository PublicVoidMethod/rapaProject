using UnityEngine;
using UnityEngine.AI;

public class BotFSM : MonoBehaviour
{

    //FSM���� ���� �����ϱ�
    //Respawn, Move, Die, Damaged, ����

    GameObject target;
    public float attackDistance = 5;

    //��ã��
    NavMeshAgent agent;
    GameObject currentPath;
    public bool reverse;

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
        Dead,
    }

    public State state;


    // Start is called before the first frame update
    void Start()
    {

        currentPath = GameObject.Find("Path (1)");

        state = State.Respawn;
        target = GameObject.Find("Soldier76_Player");

        //transform.position = BP.transform.position;

        agent = GetComponent<NavMeshAgent>();
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
            anim.SetTrigger("Die");
            UpdateDie();
        }

        else if (state == State.Dead)
        {

        }
    }

    private void UpdateDie()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        state = State.Dead;
        
        //onenable, setactive  
        Destroy(gameObject, 4f);

    }
    float updateTime;
    private void UpdateRespawn()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        updateTime += Time.deltaTime;
        //�¾�� ������ �ִϸ��̼� ���
        if (updateTime > 2)
        {
            //�ִϸ��̼��� ������ Move���·� ����
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }

    //   public GameObject BP;
    public float BotSpeed = 1;
    public int Botdist = 20;
    private void UpdateMove()
    {

        agent.SetDestination(currentPath.transform.position);
        agent.isStopped = false;
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
        //�������� ������ ������ �ִϸ��̼� ���
        anim.SetTrigger("Damage");
        anim.SetTrigger("Move");
        state = State.Move;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    //������ �ִϸ��̼��� ������ Move���·� ����   
    /*    internal void OnEventDamaged()
        {
            anim.SetTrigger("Move");
            state = State.Move;

        }*/

    internal void OnEventAttack()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
 
        //�Ѿ� ����
        GameObject BotBullet = Instantiate(BotbulletFactory);
        //Bullet�� �ѱ���
        BotBullet.transform.position = BotFirePosition.transform.position;
        //bullet ����
        dir = target.transform.position - transform.position;
        dir.Normalize();


        //���� target�� �Ÿ��� ���ؼ�
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //���� �� �Ÿ��� ���ݰŸ����� ũ��
        if (distance > attackDistance)
        {
            //Move���·� ����
            state = State.Move;
            anim.SetTrigger("Move");
        }
        else
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }


    //path trigger
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == currentPath.name)
        {
            currentPath = currentPath.GetComponent<PathScript>().GetTarget(reverse);
        }
    }

}


