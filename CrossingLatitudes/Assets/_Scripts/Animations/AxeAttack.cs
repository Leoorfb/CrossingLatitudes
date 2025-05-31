using System.Collections;
using UnityEngine;

public class AxeAttack : MonoBehaviour
{
    public float fadeDuration = 0.2f;
    public float moveDuration = 0.3f;
    public Vector3 moveOffset = new Vector3(1f, 0, 0); // Direção e distância do golpe
    public float rotationAngle = 90f; // Quanto vai rotacionar no total durante o golpe

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        // Começa invisível
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        // Fade-in
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        // Movimento + rotação de ataque
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + moveOffset;
        float timer = 0;

        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation - rotationAngle;

        while (timer < moveDuration)
        {
            float t = timer / moveDuration;

            // Movimento
            transform.position = Vector3.Lerp(startPos, endPos, t);

            // Rotação no eixo Z
            float angle = Mathf.Lerp(startRotation, endRotation, t);
            transform.rotation = Quaternion.Euler(0, 0, angle);

            timer += Time.deltaTime;
            yield return null;
        }

        // Garante posição e rotação final
        transform.position = endPos;
        transform.rotation = Quaternion.Euler(0, 0, endRotation);

        // Fade-out
        yield return StartCoroutine(Fade(1, 0, fadeDuration));

        // Destroi o machado após o ataque
        Destroy(gameObject);
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float timer = 0;
        Color color = spriteRenderer.color;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
            color.a = alpha;
            spriteRenderer.color = color;
            timer += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        spriteRenderer.color = color;
    }
}
