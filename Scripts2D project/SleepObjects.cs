using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepTrigger : MonoBehaviour
{
    // Зберігаємо посилання на об'єкт Enemy
    public GameObject enemy;

    public GameObject sleep;
    void Start()
    {
        print("Enemy");
        print("Sleep");
    }
    // Коли гравець входить в колайдер Sleep
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger entered by: " + other.gameObject.name); // Додаєте ім'я об'єкта для перевірки
        if (other.CompareTag("Player"))
        {
            print("Player tag confirmed");
            sleep.SetActive(false);
            enemy.SetActive(true);
        }
        else
        {
            print("Not Player, tag: " + other.tag); // Додаєте друк тегу об'єкта, що увійшов у тригер
        }
    }
}

