using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    public Soldier76Move soldier76;
    public Slider hpSlider;

    private void Update()
    {
        hpSlider.value = Soldier76Move.instance.hP / soldier76.maxHP;


        if (Soldier76Move.instance.hP <= 0)
        {
            Application.Quit();
        }
    }

    public void PlayerGetDamaged(int damage)
    {

        Debug.Log("> PlayerGetDamaged");
        Debug.Log($"damage : {damage}");

        Soldier76Move.instance.hP = Soldier76Move.instance.hP - damage > 0 ? Soldier76Move.instance.hP - damage : 0;
        hpSlider.value = Soldier76Move.instance.hP;
        //hpSlider.value = soldier76.GetHP() / soldier76.maxHP;
    }
}
