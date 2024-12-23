using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAppear : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float startTimer = 20f;
    public float endTimer = 30f;
    private float alphaStep = 0.05f; // ���� ���� ��������� (�� 0.1 ����� �������)
    private float currentAlpha = 0f; // ������� ���������
    private WaitForSeconds fadeWait = new WaitForSeconds(0.1f); // �������� 1 ������� �� ������ ���������
    private WaitForSeconds fullVisibilityWait = new WaitForSeconds(3f); // �������� 3 ������� �� ����� ���������

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetAlpha(0f); // ������������ ��������� ��������� �� 0
        StartCoroutine(StartRandomAppearance());
    }

    private IEnumerator StartRandomAppearance()
    {
        float randomDelay = Random.Range(startTimer, endTimer); // ��������� ��� �� 20 � 30 ���������
        yield return new WaitForSeconds(randomDelay);
        StartCoroutine(FadeInAndOut()); // ��������� ������ ����� �� ��������
    }

    private IEnumerator FadeInAndOut()
    {
        // ������ �'���������
        while (currentAlpha < 1f)
        {
            currentAlpha += alphaStep;
            SetAlpha(currentAlpha);
            yield return fadeWait;
        }

        // ���������� �������� �� 3 �������
        yield return fullVisibilityWait;

        // ������ �������
        while (currentAlpha > 0f)
        {
            currentAlpha -= alphaStep;
            SetAlpha(currentAlpha);
            yield return fadeWait;
        }
        StartCoroutine(StartRandomAppearance());
        // ����� ����� ��������� �����-�������� �� �������
        // StartCoroutine(StartRandomAppearance()); 
    }

    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = Mathf.Clamp(alpha, 0f, 1f); // �������� ��������� �� 0 �� 1
        spriteRenderer.color = color;
    }
}