using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float bulletLifetime = 0.1f; // ��� ����� ��� (� ��������)

    private void Start()
    {
        // ������� ���� ����� ������� ���
        Destroy(gameObject, bulletLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StrongEnemy") || collision.gameObject.CompareTag("Enemy"))
        {
            // ������� ����� ������
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            // ������� ���� ���� ��������
            Destroy(gameObject);
        }
    }
}
