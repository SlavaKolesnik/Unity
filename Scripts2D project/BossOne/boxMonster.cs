using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxMonster : MonoBehaviour
{
    public GameObject spawnedBoxEnemy;
    public float destroyDelay = 20f; // ���, ����� ���� ���� ������� ��'���

    void Start()
    {
        
    }
    void Update()
    {
        // ��������, ��� �� ��������� �������, ���� spawnedBoxEnemy �� �� ������
        if (spawnedBoxEnemy != null)
        {
            // ������� ����������� ��'��� ����� �������� ���
            Destroy(spawnedBoxEnemy.transform.parent.gameObject, destroyDelay);
        }
    }
}
