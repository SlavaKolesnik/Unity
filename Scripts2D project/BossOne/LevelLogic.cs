using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    public GameObject[] waveEnemy;
    public GameObject boxEnemy;
    public GameObject timePlatform;
    public GameObject boss;
    public GameObject winPlatform;

    private GameObject spawnedBoxEnemy;
    private Vector3 boxEnemyInitialPosition;
    private bool levelCycleStarted = false;
    public Main main;

    void Start()
    {
        // Зберігаємо початкову позицію префаба boxEnemy
        boxEnemyInitialPosition = boxEnemy.transform.position;
    
    }

    void Update()
    {
        // Знаходимо об'єкт гравця
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Перевіряємо, чи існує гравець та чи він нижче по Y за босса
        if (player != null && boss != null && player.transform.position.y < boss.transform.position.y && !levelCycleStarted)
        {
            levelCycleStarted = true;
            StartCoroutine(LevelCycle());
        }

    }

    IEnumerator LevelCycle()
    {
        int index = 0;
        while (index < 9)
        {
            if (boss == null) 
            {
                winPlatform.SetActive(true);
                break;
            }
            // Деактивуємо всі об'єкти waveEnemy перед активацією нового
            foreach (GameObject enemy in waveEnemy)
            {
                enemy.SetActive(false);
            }

            // Активуємо поточний об'єкт waveEnemy
            waveEnemy[index % waveEnemy.Length].SetActive(true);

            // Перевірка: якщо boxEnemy був знищений, спавнимо новий об'єкт
            if (spawnedBoxEnemy == null)
            {
                spawnedBoxEnemy = Instantiate(boxEnemy, boxEnemyInitialPosition, Quaternion.identity);
            }

            // Чекаємо 10 секунд перед активацією timePlatform
            yield return new WaitForSeconds(10f);

            // Активуємо timePlatform на 10 секунд
            timePlatform.SetActive(true);
            yield return new WaitForSeconds(12f);
            timePlatform.SetActive(false);
           
            index++;
        }

        // Після завершення 10 циклів перезапускаємо рівень
        if (boss != null)
        {
            main.Lose();
        }
    }
}
