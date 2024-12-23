using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider slider;
    public int playerHealth = 100;


    // Start is called before the first frame update
    void Start()
    {
        slider = FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerHealth;
        Death();
    }
    public void TakePlayerDamage(int PlayerDamage) 
    {
        playerHealth = playerHealth - PlayerDamage;
    }

    void Death()
    {
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
