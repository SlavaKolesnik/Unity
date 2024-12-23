using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    int hp = 0;
    public Sprite is_hp, no_hp;
    public Image hp_image;
    public Player player;
    public Text countBagHp;
    int bagHp = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey("hp"))
        {
            hp = PlayerPrefs.GetInt("hp");
            if (hp > 0)
            {
                hp_image.sprite = is_hp;
                bagHp = hp;
                countBagHp.text = bagHp.ToString();
            }
        }
    }

    public void Add_hp()
    {
        hp++;
        hp_image.sprite = is_hp;
        bagHp++;
        countBagHp.text = bagHp.ToString();
        RecountItems();
    }

    public void UseHP()
    {
        if (hp > 0)
        {
            hp--;
            player.RecountHp(1);
            bagHp--;
            countBagHp.text = bagHp.ToString();
            if (hp <= 0)
            {
                hp_image.sprite = no_hp;
            }
            RecountItems();
        }
    }

    public void RecountItems()
    {
        PlayerPrefs.SetInt("hp", hp);
    }

    void OnApplicationQuit()
    {
        RecountItems();
    }
}
