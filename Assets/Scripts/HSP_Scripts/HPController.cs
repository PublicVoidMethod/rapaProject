using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    public Soldier76Move soldier76;
    public Slider hpSlider;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerGetDamaged(int damage)
    {
        hpSlider.value -= damage;
        //hpSlider.value = soldier76.GetHP() / soldier76.maxHP;
    }

}
