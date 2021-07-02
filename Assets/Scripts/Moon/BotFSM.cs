/*using System;
using UnityEngine;

public class BotFSM : MonoBehaviour
{

    //FSM으로 상태 제어하기
    //Respawn, Move, Die, Damaged, 공격

    enum State
    {
        Respawn,
        Move,
        Damaged,
        Attack,
        Die,
    }

    public State state;

  
    // Start is called before the first frame update
    void Start()
    {
        state = State.Respawn;
        target = GameObject.Find("Soldier76_PlayerT");

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
        //다시 태어나기
    }
    private void UpdateMove()
    {
        //정해진 방향으로 이동하기


        //나와 target의 거리를 구해서
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //만약 그 거리가 공격 거리보다 작으면
        //Attack 상태로 전이
    }
    private void UpdateDamaged()
    {

    }
    private void UpdateAttack()
    {

    }
    private void UpdateDie()
    {

    }

}
*/