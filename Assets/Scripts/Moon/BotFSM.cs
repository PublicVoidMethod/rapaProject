using System;
using UnityEngine;

public class BotFSM : MonoBehaviour
{

    //FSM으로 상태 제어하기
    //Respawn, Move, Die, Damaged, 공격

    GameObject target;
    public float attackDistance = 5;



    //Animator > Animator Controller > Animation Clips
    public Animator anim;

    //공격
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
        //태어나면 리스폰 애니메이션 재생
        //애니메이션이 끝나면 Move상태로 전이
        state = State.Move;
        anim.SetTrigger("Move");
    }

    public GameObject BP;
    public int BotSpeed = 1;
    public int Botdist = 20;
    private void UpdateMove()
    {
        //정해진 방향으로 이동하기
        transform.position += transform.forward * Time.deltaTime * BotSpeed;
        if ((Vector3.Distance(BP.transform.position, transform.position)) > Botdist)
        {
            BP.transform.position = transform.position;
            transform.Rotate(0, -90, 0);
        }

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
        //데미지를 받으면

        //데미지 애니메이션 재생

        //데미지 애니메이션이 끝나면 Move상태로 전이
        state = State.Move;
        anim.SetTrigger("Move");
        //hp가 다 닳으면 die 상태로 전이
        // state = State.Die;
    }


    internal void OnEventAttack()
    {
        //총알 복제
        GameObject BotBullet = Instantiate(BotbulletFactory);
        //Bullet을 총구에
        BotBullet.transform.position = BotFirePosition.transform.position;
        //bullet 방향
        dir = target.transform.position - transform.position;
        dir.Normalize();

        //나와  target의 거리를 구해서
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //만약 그 거리가 공격거리보다 크면
        if (distance > attackDistance)
        {
            //Move상태로 전이
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
