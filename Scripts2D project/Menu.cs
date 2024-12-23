using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button[] lvls;
    public Text coinText;
    public Text bulletText;
    public Text hpText;
    public Slider musicSlider, soundSlider;
    public Text musicText, soundText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Lvl"))
        {
            for (int i = 0; i < lvls.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("Lvl"))
                {
                    lvls[i].interactable = true;
                }
                else
                {
                    lvls[i].interactable = false;
                }
            }
        }
        if (!PlayerPrefs.HasKey("hp"))
        {
            PlayerPrefs.SetInt("hp", 0);
        }

        if(!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetInt("MusicVolume", 3);
        }

        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetInt("SoundVolume", 4);
        }
        musicSlider.value = PlayerPrefs.GetInt("MusicVolume");
        soundSlider.value = PlayerPrefs.GetInt("SoundVolume");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("MusicVolume", (int)musicSlider.value);
        PlayerPrefs.SetInt("SoundVolume", (int)soundSlider.value);
        musicText.text = musicSlider.value.ToString();
        soundText.text = soundSlider.value.ToString();

        if (PlayerPrefs.HasKey("coins"))
        {
            coinText.text = PlayerPrefs.GetInt("coins").ToString();
        }
        else
        {
            coinText.text = "0";
        }

        if (PlayerPrefs.HasKey("bullets"))
        {
            bulletText.text = PlayerPrefs.GetInt("bullets").ToString();
        }
        else
        {
            bulletText.text = "0";
        }

        if(PlayerPrefs.HasKey("hp"))
        {
            hpText.text = PlayerPrefs.GetInt("hp").ToString();
        }
        else
        {
            hpText.text = "0";
        }
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Buy_hp(int cost) 
    {
        if(PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("hp", PlayerPrefs.GetInt("hp") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }

    public void Buy_bul5(int cost)
    {
        if (PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("bullets", PlayerPrefs.GetInt("bullets") + 5);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }

    public void Buy_bul10(int cost)
    {
        if (PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("bullets", PlayerPrefs.GetInt("bullets") + 10);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }

    public void Buy_bul15(int cost)
    {
        if (PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("bullets", PlayerPrefs.GetInt("bullets") + 15);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }
}
