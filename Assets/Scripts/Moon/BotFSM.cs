/*using System;
using UnityEngine;

public class BotFSM : MonoBehaviour
{

    //FSM���� ���� �����ϱ�
    //Respawn, Move, Die, Damaged, ����

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
        //�ٽ� �¾��
    }
    private void UpdateMove()
    {
        //������ �������� �̵��ϱ�


        //���� target�� �Ÿ��� ���ؼ�
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //���� �� �Ÿ��� ���� �Ÿ����� ������
        //Attack ���·� ����
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