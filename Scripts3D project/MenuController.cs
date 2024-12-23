using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public GameObject panelSettings;
    public Slider volumeSlider;
    public AudioSource BG_MenuAudio;
    // Start is called before the first frame update
    void Start()
    {
        panelSettings = GameObject.FindGameObjectWithTag("Sound");
        volumeSlider = GetComponentInChildren<Slider>();
        BG_MenuAudio = GetComponent<AudioSource>();
        panelSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        BG_MenuAudio.volume = volumeSlider.value;

    }
    public void StartGame()
    {
        SceneManager.LoadScene("City");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Settings()
    {
        if(panelSettings.activeInHierarchy==false) //галочка над імененем
        {
            panelSettings.SetActive(true);
        }
        else
        {
            panelSettings.SetActive(false);
        }
    }
}
