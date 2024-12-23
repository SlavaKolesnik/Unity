using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxMonster : MonoBehaviour
{
    public GameObject spawnedBoxEnemy;
    public float destroyDelay = 20f; // Час, через який буде знищено об'єкт

    void Start()
    {
        
    }
    void Update()
    {
        // Перевірка, щоб не допустити помилок, якщо spawnedBoxEnemy ще не задано
        if (spawnedBoxEnemy != null)
        {
            // Знищуємо батьківський об'єкт через вказаний час
            Destroy(spawnedBoxEnemy.transform.parent.gameObject, destroyDelay);
        }
    }
}
