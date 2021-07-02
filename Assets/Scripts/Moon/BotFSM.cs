using UnityEngine;

public class BotFSM : MonoBehaviour
{

    //FSM으로 상태 제어하기
    //Respawn, Move, Die, Damaged, 공격

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
        //태어나면 리스폰 애니메이션 재생
    }
    private void UpdateMove()
    {
        //리스폰 애니메이션 후 정해진 방향으로 이동하기

 
        //나와 target의 거리를 구해서
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //만약 그 거리가 공격 거리보다 작으면
        if (distance < attackDistance)
        {
            //Attack 상태로 전이
            state = State.Attack;
        }
    }
    private void UpdateDamaged()
    {
        //데미지를 받으면
        //데미지 애니메이션 재생
        //

        //hp가 다 닳으면 die 상태로 전이
        state = State.Die;
    }
    private void UpdateAttack()
    {
        //시간이 흐르다가
        currentTime += Time.deltaTime;
        //현재시간이 공격시간이 되면
        if (currentTime > attackTime)
        {
            //현재 시간 초기화
            currentTime = 0;
            //플레이어를 공격
            //target.AddDamage();
            //나와  target의 거리를 구해서
            float distance = Vector3.Distance(transform.position, target.transform.position);
            //만약 그 거리가 공격거리보다 크면
            if (distance > attackDistance)
            {
                //Move상태로 전이
                state = State.Move;
            }

        }

    }
    private void UpdateDie()
    {
       // Destroy(gameObject.Bot);
    }

}
