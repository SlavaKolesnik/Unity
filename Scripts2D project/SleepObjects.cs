using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepTrigger : MonoBehaviour
{
    // �������� ��������� �� ��'��� Enemy
    public GameObject enemy;

    public GameObject sleep;
    void Start()
    {
        print("Enemy");
        print("Sleep");
    }
    // ���� ������� ������� � �������� Sleep
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger entered by: " + other.gameObject.name); // ������ ��'� ��'���� ��� ��������
        if (other.CompareTag("Player"))
        {
            print("Player tag confirmed");
            sleep.SetActive(false);
            enemy.SetActive(true);
        }
        else
        {
            print("Not Player, tag: " + other.tag); // ������ ���� ���� ��'����, �� ������ � ������
        }
    }
}

