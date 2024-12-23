using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public int damage = -1;
    public float bulletLifetime = 0.1f; // ��� ����� ��� (� ��������)

    private void Start()
    {
        // ������� ���� ����� ������� ���
        Destroy(gameObject, bulletLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ������� ����� ������
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.RecountHp(damage);
            }
            // ������� ���� ���� ��������
            Destroy(gameObject);
        }
    }
}
