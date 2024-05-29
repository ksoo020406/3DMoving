using System;
using UnityEngine;

// 데미지를 받는 인터페이스, 엔피시 몬스터 등 데미지를 입는 개체에 다중 상속 가능
public interface IDamagalbe
{
    void TakePhysicalMamage(int damage);
}


public class PlayerCondition : MonoBehaviour, IDamagalbe
{
    public UICondition uiCondition;

    // 아래 변수를 통해 이 스크립트 안에서 데이터를 사용
    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage; // 델리게이트 이벤트로 받기, 데미지 받을 때 깜빡

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

    public void TakePhysicalMamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
