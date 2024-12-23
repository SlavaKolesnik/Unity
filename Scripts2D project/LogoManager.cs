using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoManager : MonoBehaviour
{
    public GameObject logoCanvas; // Canvas з логотипом
    public float displayTime = 3f; // Час показу логотипу в секундах

    void Start()
    {
        if (!PlayerPrefs.HasKey("LogoShown")) // Якщо логотип ще не показувався
        {
            logoCanvas.SetActive(true); // Показуємо логотип
            PlayerPrefs.SetInt("LogoShown", 1); // Запам'ятовуємо, що логотип показаний
            Invoke("HideLogo", displayTime);
        }
        else
        {
            logoCanvas.SetActive(false); // Логотип більше не показується
        }
    }

    void HideLogo()
    {
        logoCanvas.SetActive(false); // Приховуємо логотип
    }
}

