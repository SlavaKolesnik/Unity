using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnZombies : MonoBehaviour
{
    public Waves _waves;

    public GameObject YakuZombie;
    public float SpawnTime;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 15f;
    public Vector3 SpawnPlace;
    // Start is called before the first frame update
    void Start()
    {
        _waves = FindObjectOfType<Waves>();
        // Шукає модельку в папці
        YakuZombie = GameObject.FindGameObjectWithTag("Zombie");
        StartCoroutine("WaitTimeForSpawn");
    }

    // Update is called once per frame
    IEnumerator WaitTimeForSpawn()
    {
        while (true)
        {
            if (_waves.ZombieCount.Length < _waves.maxZombiesOnWave)
            {
                SpawnPlace = new Vector3(Random.Range(-7, 9), 10.6f, Random.Range(-20, 24));
                SpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                Instantiate(YakuZombie.gameObject, SpawnPlace, Quaternion.identity);
                yield return new WaitForSeconds(SpawnTime);
                print("1 second has passed");

               
              

            }
            else
            {
                yield return new WaitForSeconds(SpawnTime);
            }

        }

    }
}