using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP = 5;
    public int damage = -1;
    public float impulse = 8f;
    private bool isHit = false;
    private Coroutine hitCoroutine;
    private SpriteRenderer spriteRenderer;
    public GameObject drop;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Отримуємо компонент SpriteRenderer
    }

    void Update()
    {
        if (enemyHP <= 0)
        {
            if (drop != null)
            {
            
                Instantiate(drop, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Знищуємо ворога якщо життя <= 0
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHP -= damage;
        Debug.Log("Enemy HP: " + enemyHP);
        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine); // Зупиняємо попередню корутину, якщо вона ще виконується
        }

        isHit = true;
        hitCoroutine = StartCoroutine(OnHit()); // Запускаємо корутину для червоніння
    }

    // Викликається при вході в зіткнення
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().RecountHp(damage); // Віднімемо життя гравцеві
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * impulse, ForceMode2D.Impulse);
        }
    }

    IEnumerator OnHit()
    {
        // Червоніння ворога
        while (spriteRenderer.color.g > 0)
        {
            spriteRenderer.color = new Color(1f, spriteRenderer.color.g - 0.04f, spriteRenderer.color.b - 0.04f);
            yield return new WaitForSeconds(0.02f);
        }

        // Повернення до нормального стану
        while (spriteRenderer.color.g < 1)
        {
            spriteRenderer.color = new Color(1f, spriteRenderer.color.g + 0.04f, spriteRenderer.color.b + 0.04f);
            yield return new WaitForSeconds(0.02f);
        }

        isHit = false; // Завершуємо стан отримання удару
    }
}

