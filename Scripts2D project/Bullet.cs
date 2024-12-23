using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float bulletLifetime = 0.1f; // Час життя кулі (в секундах)

    private void Start()
    {
        // Знищуємо кулю через заданий час
        Destroy(gameObject, bulletLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StrongEnemy") || collision.gameObject.CompareTag("Enemy"))
        {
            // Завдаємо шкоду ворогу
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            // Знищуємо кулю після влучання
            Destroy(gameObject);
        }
    }
}
