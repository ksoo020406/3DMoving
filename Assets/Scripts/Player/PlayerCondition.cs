using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    // 아래 변수를 통해 이 스크립트 안에서 데이터를 사용
    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    private void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amout)
    {
        health.Add(amout);
    }

    private void Die()
    {
        Debug.Log("죽었다");
    }
}
