using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLock : MonoBehaviour
{
   
    public GameObject iceBarier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����������, �� ��'���, � ���� �� ������������, �� weight ��� iceBarier
        if (collision.gameObject == iceBarier)
        {
            // �������� ������ ��'����
            gameObject.SetActive(false);
            iceBarier.SetActive(false);
        }
    }
}
