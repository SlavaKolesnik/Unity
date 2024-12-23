using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoManager : MonoBehaviour
{
    public GameObject logoCanvas; // Canvas � ���������
    public float displayTime = 3f; // ��� ������ �������� � ��������

    void Start()
    {
        if (!PlayerPrefs.HasKey("LogoShown")) // ���� ������� �� �� �����������
        {
            logoCanvas.SetActive(true); // �������� �������
            PlayerPrefs.SetInt("LogoShown", 1); // �����'�������, �� ������� ���������
            Invoke("HideLogo", displayTime);
        }
        else
        {
            logoCanvas.SetActive(false); // ������� ����� �� ����������
        }
    }

    void HideLogo()
    {
        logoCanvas.SetActive(false); // ��������� �������
    }
}

