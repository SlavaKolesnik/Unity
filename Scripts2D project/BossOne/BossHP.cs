using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public Enemy enemy; // ��������� �� ������
    public Slider healthSlider; // ��������� �� �������

    void Start()
    {
        if (healthSlider != null && enemy != null)
        {
            // ���������� ������� ������������ ��������� HP ������
            healthSlider.maxValue = enemy.enemyHP;
            healthSlider.value = enemy.enemyHP;
        }
    }

    void Update()
    {
        if (healthSlider != null && enemy != null)
        {
            // ��������� �������� �������� �������� �� HP ������
            healthSlider.value = enemy.enemyHP;
        }
    }
}
