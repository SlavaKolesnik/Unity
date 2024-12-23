using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlace : MonoBehaviour
{
    public GameObject[] WeaponBag;
    public int WeaponBagLenght;
    public GameObject CurrentWeapon;
    void Start()
    {
        WeaponBagLenght = transform.childCount;
        WeaponBag = new GameObject[WeaponBagLenght];

        for(int i = 0; i < WeaponBagLenght; i++)
        {
            WeaponBag[i] = transform.GetChild(i).gameObject;
        }
        CurrentWeapon = WeaponBag[0];
    }

   
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) // 1 on keyboard
        {
            CurrentWeapon = WeaponBag[0];
            ChooseWeapon();
        }
        if (Input.GetKey(KeyCode.Alpha2)) // 2 on keyboard
        {
            CurrentWeapon = WeaponBag[1];
            ChooseWeapon();
        }
    }
    void ChooseWeapon()
    {
        for (int i = 0; i < WeaponBagLenght; i++)
        {
            if(WeaponBag[i] != CurrentWeapon)
            {
                WeaponBag[i].gameObject.SetActive(false);
            }
            else
            {
                WeaponBag[i].gameObject.SetActive(true);
            }
        }
    }
}
