using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public int damage = -1;
    public float bulletLifetime = 0.1f; // Час життя кулі (в секундах)

    private void Start()
    {
        // Знищуємо кулю через заданий час
        Destroy(gameObject, bulletLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Завдаємо шкоду ворогу
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.RecountHp(damage);
            }
            // Знищуємо кулю після влучання
            Destroy(gameObject);
        }
    }
}
