using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    public Waves _waves;

    public GameObject YakuZombie;
    public float SpawnTime;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 15f;
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
                SpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(SpawnTime);
                print("1 second has passed");
                if (YakuZombie != null)
                {
                    Instantiate(YakuZombie, gameObject.transform.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("YakuZombie has been destroyed and cannot be instantiated.");
                }

            }
            else
            {
                yield return new WaitForSeconds(SpawnTime);
            }

        }

    }
}