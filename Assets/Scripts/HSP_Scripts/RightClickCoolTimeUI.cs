using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightClickCoolTimeUI : MonoBehaviour
{
    public Image skillImage;
    public float coolTimeUI = 5.0f;
    public KeyCode skillButton;
    bool isCoolTime = false;

    void Start()
    {
        skillImage.fillAmount = 0;
    }

    void Update()
    {
        RocketSkillCoolTime();
    }

    private void RocketSkillCoolTime()
    {
        if(Input.GetKey(skillButton) && isCoolTime == false)
        {
            isCoolTime = true;
            skillImage.fillAmount = 1;
        }
        if (isCoolTime)
        {
            skillImage.fillAmount -= 1 / coolTimeUI * Time.deltaTime;

            if(skillImage.fillAmount <= 0)
            {
                skillImage.fillAmount = 0;
                isCoolTime = false;
            }
        }

    }
}
