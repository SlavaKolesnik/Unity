using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    private static PersistentCanvas instance;

    void Awake()
    {
        // Перевірка, чи вже існує екземпляр Canvas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Зберегти Canvas між сценами
        }
        else
        {
            Destroy(gameObject); // Знищити дублікати Canvas
        }
    }
}