using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Player player;
    public Text cointText;
    public Text bulletText;
    public Image[] hearts;
    public Sprite isLife, nonLife;
    public GameObject PauseScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject inventoryPan;
    public SoundEffector soundEffector;
    public AudioSource musicSoucre, soundSorce;

    void Start()
    {
        musicSoucre.volume = (float)PlayerPrefs.GetInt("MusicVolume") / 9;
        soundSorce.volume = (float)PlayerPrefs.GetInt("SoundVolume") / 9;
    }

    
    public void Update()
    {
        cointText.text = player.GetCoins().ToString();
        bulletText.text = player.GetBullets().ToString();
        for (int i = 0; i < hearts.Length; i++)
        {
            if (player.GetHP() > i)
            {
                hearts[i].sprite = isLife;
            }
            else
            {
                hearts[i].sprite = nonLife;
            }
        }
    }
    public void ReloadLvl()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseOn()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        PauseScreen.SetActive(true);
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        PauseScreen.SetActive(false);
    }
    public void Win()
    {
        soundEffector.PlayWinSound();
        Time.timeScale = 0f;
        player.enabled = false;
        WinScreen.SetActive(true);

       if (!PlayerPrefs.HasKey("Lvl") || PlayerPrefs.GetInt("Lvl") < SceneManager.GetActiveScene().buildIndex) 
       {
            PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);
       }
       
       if (PlayerPrefs.HasKey("coins"))
       {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + player.GetCoins());
       } 
       else
       {
            PlayerPrefs.SetInt("coins", player.GetCoins());
       }

       if (PlayerPrefs.HasKey("bullets"))
       {
           PlayerPrefs.SetInt("bullets", PlayerPrefs.GetInt("bullets") + player.GetBullets());
       } 
       else
       {
           PlayerPrefs.SetInt("bullets", player.GetCoins());
       }
       inventoryPan.SetActive(false);
       GetComponent<Inventory>().RecountItems();
       RecountBullets();
    }
    public void Lose()
    {
        soundEffector.PlayLoseSound();
        Time.timeScale = 0f;
        player.enabled = false;
        LoseScreen.SetActive(true);
        inventoryPan.SetActive(false);
        GetComponent<Inventory>().RecountItems(); 
        RecountBullets();
    }
    public void MenuLvl()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene("Menu");
    }
    public void NextLvl()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RecountBullets()
    {
        PlayerPrefs.SetInt("bullets", player.GetBullets());
    }
}
