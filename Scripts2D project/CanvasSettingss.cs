using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    private static PersistentCanvas instance;

    void Awake()
    {
        // ��������, �� ��� ���� ��������� Canvas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �������� Canvas �� �������
        }
        else
        {
            Destroy(gameObject); // ������� �������� Canvas
        }
    }
}