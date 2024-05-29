using UnityEngine;
using UnityEngine.UI;


public class Condition : MonoBehaviour
{
    public float curValue; // 현재 값
    public float startValue; // 시작 값
    public float maxValue; // 최대 값
    public float passiveValue; // 줄어들었다 회복되는 값, 주기적으로 변하는 값
    public Image uiBar;

    private void Start()
    {
        curValue = startValue; // 지금은 시작 값과 현재 값을 동일하게 하지만 나중엔 startValue를 저장된 데이터로 가져오도록 할 수 있다.
    }

    private void Update()
    {
        // ui업데이트
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue); // 더해지는 값이 최대값을 넘지 않도록 제한
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0); // 감소되는 값이 0 아래로 내려가지 않도록 제한
    }
}
