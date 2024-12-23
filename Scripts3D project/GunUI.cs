using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    private Gun gun;

    ////public Text textBagAmmo;
    ////public Text textMaxAmmo;
    ////public Text textCurrentAmmo;
    public Text[] textAmmo;
    // Start is called before the first frame update
    void Start()
    {
        gun = FindObjectOfType<Gun>();
        textAmmo = gameObject.GetComponentsInChildren<Text>();
       
    }


    // Update is called once per frame
    void Update()
    {
        PistolUI();
    }
    void PistolUI()
    {
        textAmmo[0].text = gun.currentAmmo + "|" + gun.BagAmmo;

    }
}

