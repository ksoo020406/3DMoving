using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage; // 딜을 얼마나 줄건지
    public float damageRate; // 얼마나 자주 줄건지

    List<IDamagalbe> things = new List<IDamagalbe>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate); // 시작시간은 0초, damageRate초 만큼 텀을 두고 DealDamage함수 호출
    }

    // 트리거를 조회해서 실제로 함수 호출
    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalMamage(damage);
        }
    }

    // 닿았는지, 인터페이스를 가지고 있는지 확인
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagalbe damagalbe)) // other가 데미지 받는 인터페이스를 가지고 있으면?
        {
            things.Add(damagalbe);
        }
    }

    // 안닿았으면 데미지 삭제
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagalbe damagalbe))
        {
            things.Remove(damagalbe);
        }


    }
}
