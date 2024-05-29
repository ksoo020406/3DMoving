using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine; // 코루틴 실행을 위한 변수

    private void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }

    public void Flash()
    {
        if (coroutine != null) // 이미 실행중인 코루틴이 있으면
        {
            StopCoroutine(coroutine); // 멈춰주기
        }

        image.enabled = true;
        image.color = new Color(1f, 100f / 255f, 100f / 255f);
        coroutine = StartCoroutine(FadeAway()); // StartCoroutine으로 실행하고 FadeAway() 코루틴을 반환
    }

    // 코루틴 사용
    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        // 빨간색이 한 번 보이고 서서히 옅어지는 효과
        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 100f / 255f, 100f / 255f, a);
            yield return null; // 반환 값을 항상 정해줘야 코루틴 사용 가능
        }

        image.enabled = false;
    }
}
