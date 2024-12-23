using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float Healt = 50f;
    public void TakeDamage(float Amount)
    {
        Healt -= Amount;
        if (Healt < 0 )
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
