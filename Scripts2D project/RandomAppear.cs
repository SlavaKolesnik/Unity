using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAppear : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float startTimer = 20f;
    public float endTimer = 30f;
    private float alphaStep = 0.05f; // Крок зміни прозорості (по 0.1 кожну секунду)
    private float currentAlpha = 0f; // Поточна прозорість
    private WaitForSeconds fadeWait = new WaitForSeconds(0.1f); // Затримка 1 секунда між змінами прозорості
    private WaitForSeconds fullVisibilityWait = new WaitForSeconds(3f); // Затримка 3 секунди на повній прозорості

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetAlpha(0f); // Встановлюємо початкову прозорість на 0
        StartCoroutine(StartRandomAppearance());
    }

    private IEnumerator StartRandomAppearance()
    {
        float randomDelay = Random.Range(startTimer, endTimer); // Рандомний час між 20 і 30 секундами
        yield return new WaitForSeconds(randomDelay);
        StartCoroutine(FadeInAndOut()); // Запускаємо процес появи та зникання
    }

    private IEnumerator FadeInAndOut()
    {
        // Плавно з'являємося
        while (currentAlpha < 1f)
        {
            currentAlpha += alphaStep;
            SetAlpha(currentAlpha);
            yield return fadeWait;
        }

        // Залишаємось видимими на 3 секунди
        yield return fullVisibilityWait;

        // Плавно зникаємо
        while (currentAlpha > 0f)
        {
            currentAlpha -= alphaStep;
            SetAlpha(currentAlpha);
            yield return fadeWait;
        }
        StartCoroutine(StartRandomAppearance());
        // Можна знову запустити появу-зникання за потреби
        // StartCoroutine(StartRandomAppearance()); 
    }

    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = Mathf.Clamp(alpha, 0f, 1f); // Обмежуємо прозорість від 0 до 1
        spriteRenderer.color = color;
    }
}