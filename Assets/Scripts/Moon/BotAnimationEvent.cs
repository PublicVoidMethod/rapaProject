using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimationEvent : MonoBehaviour
{
    public BotFSM bot;

    //�ִϸ��̼� ���� ���ۿ� ȣ���� �̺�Ʈ �Լ��� ����
    public void OnEventAttack()
    {
        print("ATTACK");
        //�̺�Ʈ�� �߻��ϸ� Enemy ������Ʈ���� �˷��ְ�ʹ� 
        bot.OnEventAttack();
    }
}
