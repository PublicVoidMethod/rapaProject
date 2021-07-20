using UnityEngine;
using UnityEngine.AI;

public class BotFSM : MonoBehaviour
{

    //FSM으로 상태 제어하기
    //Respawn, Move, Die, Damaged, 공격

    GameObject target;
    public float attackDistance = 5;

    //길찾기
    NavMeshAgent agent;
    GameObject currentPath;
    public bool reverse;

    //Animator > Animator Controller > Animation Clips
    public Animator anim;

    //공격
    public GameObject BotbulletFactory;
    public GameObject BotFirePosition;
    public Vector3 dir;

    public BotManager botManager;
    public int count;

    //오디오
    AudioSource audioSource;

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
        //오디오
        audioSource = GetComponent<AudioSource>();

        currentPath = GameObject.Find("Path (1)");

        state = State.Respawn;
        target = GameObject.Find("Soldier76_Player");

        //transform.position = BP.transform.position;

        agent = GetComponent<NavMeshAgent>();
    }

    bool isDead = false;
    // Update is called once per frame
    void Update()
    {
        /*
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
            //rotation
            dir = target.transform.position - transform.position;
            dir.Normalize();
            transform.rotation = Quaternion.LookRotation(dir);
        }

        else if (state == State.Die)
        {
            anim.SetTrigger("Die");
            UpdateDie();
        }

        else if (state == State.Dead)
        {
            Debug.Log("> State.Dead");
            UpdateDead();
        }
        */
        if (!isDead)
        {
            if (state == State.Respawn)
            {
                Debug.Log(">> Respawn");
                UpdateRespawn();
            }
            if (state == State.Move)
            {
                Debug.Log(">> Move");
                UpdateMove();
            }
            if (state == State.Damaged)
            {
                Debug.Log(">> Damaged");
                UpdateDamaged();
            }
            if (state == State.Attack)
            {
                Debug.Log(">> Attack");
                //rotation
                dir = target.transform.position - transform.position;
                dir.Normalize();
                transform.rotation = Quaternion.LookRotation(dir);
            }
            if (state == State.Die)
            {
                Debug.Log(">> Die");
                anim.SetTrigger("Die");
                UpdateDie();
            }
        }
        if (state == State.Dead || isDead)
        {
            isDead = true;
            Debug.Log("> State.Dead");
            UpdateDead();
        }



    }



    float updateTime;
    public void UpdateRespawn()
    {
        /*        transform.position = botManager.GetRandomPosition();
                gameObject.SetActive(true);*/
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        updateTime += Time.deltaTime;
        //태어나면 리스폰 애니메이션 재생
        if (updateTime > 2)
        {
            //애니메이션이 끝나면 Move상태로 전이
            state = State.Move;
            anim.SetTrigger("Move");
            updateTime = 0;
        }
    }

    //   public GameObject BP;
    public float BotSpeed = 1;
    public int Botdist = 20;
    private void UpdateMove()
    {

        agent.SetDestination(currentPath.transform.position);
        agent.isStopped = false;
        //나와 target의 거리를 구해서
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //만약 그 거리가 공격 거리보다 작으면
        if (distance < attackDistance)
        {
            //Attack 상태로 전이
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }



    private void UpdateDamaged()
    {
        //데미지를 받으면 데미지 애니메이션 재생
        anim.SetTrigger("Damage");
        anim.SetTrigger("Move");
        state = State.Move;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    //데미지 애니메이션이 끝나면 Move상태로 전이   
    /*    internal void OnEventDamaged()
        {
            anim.SetTrigger("Move");
            state = State.Move;

        }*/

    internal void OnEventAttack()
    {
        //GameObject prevPath = currentPath;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;


        //총알 복제
        GameObject BotBullet = Instantiate(BotbulletFactory, BotFirePosition.transform.position, BotFirePosition.transform.rotation);

        //나와 target의 거리를 구해서
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //만약 그 거리가 공격거리보다 크면
        if (distance > attackDistance)
        {
            //currentPath = prevPath;
            //Move상태로 전이
            state = State.Move;
            anim.SetTrigger("Move");
        }
        else
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    public void UpdateDie()
    {
        audioSource.Play();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        state = State.Dead;
        updateTime = 0;
    }

    private void OnDestroy()
    {
        botManager.BotInstant();
        isDead = false;
    }

    private void UpdateDead()
    {

        if (updateTime > 3)
        {
            //onenable, setactive  
            Destroy(gameObject);
            /*            gameObject.SetActive(false);*/
            updateTime = 0;
        }
        else
        {
            updateTime += Time.deltaTime;
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


