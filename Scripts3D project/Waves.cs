using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Waves : MonoBehaviour
{
    // Start is called before the first frame update
    //рахуватимем зомбі
    public GameObject[] ZombieCount;
    public int maxZombiesOnWave = 10;
    public int ZombieKillsOnWave;
    public int ZombieDamageAdding;
    public int ZombieHealthAdding;
    public int WavesCount = 1;

    private void Start()
    {


    }

    void Update()
    {

        if (ZombieKillsOnWave >= maxZombiesOnWave)
        {
            ChangeWave();
            CountZombiesOfWave();
        }
      
    }

    void CountZombiesOfWave()
    {
        ZombieCount = GameObject.FindGameObjectsWithTag("Zombie");
    }
    void ChangeWave()
    {
        //кожну нову хвилю збільшуємо зомбі на одного.
        maxZombiesOnWave++;
        ZombieKillsOnWave = 0;

        WavesCount++;
        ZombieHealthAdding = ZombieHealthAdding + 50;
        ZombieDamageAdding = ZombieDamageAdding + 50; 

        for (int countZombies = 0; countZombies < ZombieCount.Length; countZombies++)
        {
            Destroy(ZombieCount[countZombies].gameObject);
        }
    }
}
