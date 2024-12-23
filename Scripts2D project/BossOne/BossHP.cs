using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public Enemy enemy; // Посилання на ворога
    public Slider healthSlider; // Посилання на слайдер

    void Start()
    {
        if (healthSlider != null && enemy != null)
        {
            // Ініціалізуємо слайдер максимальним значенням HP ворога
            healthSlider.maxValue = enemy.enemyHP;
            healthSlider.value = enemy.enemyHP;
        }
    }

    void Update()
    {
        if (healthSlider != null && enemy != null)
        {
            // Оновлюємо значення слайдера відповідно до HP ворога
            healthSlider.value = enemy.enemyHP;
        }
    }
}
