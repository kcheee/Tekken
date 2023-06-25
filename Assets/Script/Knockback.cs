using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    static public Knockback instance;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;
    public AnimationCurve knockbackCurve;

    private Coroutine knockbackCoroutine;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
       
    }
    public void ApplyKnockback(Vector3 direction, float Force)
    {
       
        // �˹� �ڷ�ƾ�� ���� ���̶�� �ߴ��մϴ�.
        if (knockbackCoroutine != null)
            StopCoroutine(knockbackCoroutine);

        // �˹� �ڷ�ƾ�� �����մϴ�.
        knockbackCoroutine = StartCoroutine(DoKnockback(direction,Force));
    }

    private IEnumerator DoKnockback(Vector3 direction, float Force)
    {
        // �˹��� �����ϱ� ���� ������ �� �Ÿ��� ����մϴ�.
        float distance = Force * knockbackDuration;

        // ���� ��ġ�� �����մϴ�.
        Vector3 startPosition = transform.position;

        // �˹� �ð��� �带���� ���� �����ϵ��� �������� ����մϴ�.
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / knockbackDuration;
            float progress = knockbackCurve.Evaluate(t);

            // �������� ����Ͽ� ��ġ�� �̵��մϴ�.
            Vector3 targetPosition = startPosition + (direction * distance * progress);
            transform.position = targetPosition;

            yield return null;
        }

        // �˹��� �Ϸ�Ǹ� �˹� �ڷ�ƾ�� �����մϴ�.
        knockbackCoroutine = null;
    }
}
