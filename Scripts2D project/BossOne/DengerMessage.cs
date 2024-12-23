using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DengerMessage : MonoBehaviour
{
    public GameObject message;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateEnemyAndDeactivateMessage());
    }

    IEnumerator ActivateEnemyAndDeactivateMessage()
    {
        // Затримка перед активацією об'єкта enemy
        yield return new WaitForSeconds(3f);
        enemy.SetActive(true);

        // Затримка перед деактивацією об'єкта message
        yield return new WaitForSeconds(3f);
        message.SetActive(false);
    }
}
