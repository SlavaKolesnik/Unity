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
        // �������� ��������� ������� ������� boxEnemy
        boxEnemyInitialPosition = boxEnemy.transform.position;
    
    }

    void Update()
    {
        // ��������� ��'��� ������
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // ����������, �� ���� ������� �� �� �� ����� �� Y �� �����
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
            // ���������� �� ��'���� waveEnemy ����� ���������� ������
            foreach (GameObject enemy in waveEnemy)
            {
                enemy.SetActive(false);
            }

            // �������� �������� ��'��� waveEnemy
            waveEnemy[index % waveEnemy.Length].SetActive(true);

            // ��������: ���� boxEnemy ��� ��������, �������� ����� ��'���
            if (spawnedBoxEnemy == null)
            {
                spawnedBoxEnemy = Instantiate(boxEnemy, boxEnemyInitialPosition, Quaternion.identity);
            }

            // ������ 10 ������ ����� ���������� timePlatform
            yield return new WaitForSeconds(10f);

            // �������� timePlatform �� 10 ������
            timePlatform.SetActive(true);
            yield return new WaitForSeconds(12f);
            timePlatform.SetActive(false);
           
            index++;
        }

        // ϳ��� ���������� 10 ����� ������������� �����
        if (boss != null)
        {
            main.Lose();
        }
    }
}
