using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimationEvent : MonoBehaviour
{
    public BotFSM bot;

    //애니메이션 공격 동작에 호출할 이벤트 함수를 제작
    public void OnEventAttack()
    {
        print("ATTACK");
        //이벤트가 발생하면 Enemy 컴포넌트에게 알려주고싶다 
        bot.OnEventAttack();
    }
}
