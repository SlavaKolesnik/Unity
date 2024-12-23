using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public float lookEnemyRange = 5f; // Радіус виявлення гравця
    public GameObject bulletPrefab; // Префаб кулі
    public Transform bulletSpawnPoint; // Точка, звідки вилітає куля
    public float bulletSpeed = 15f; // Швидкість кулі
    public float shootCooldown = 1f; // Час між пострілами
    private float nextShootTime = 0f; // Час для наступного пострілу

    public Transform point1;
    public Transform point2;
    public float speed = 2f; // Швидкість руху ворога
    public float waitTime = 3f; // Час очікування перед зміною напрямку
    private bool CanGo = true;
    private bool playerInRange = false;
    Animator anim;


    void Start()
    {
        transform.position = new Vector3(point1.position.x, point1.position.y, transform.position.z); // Початкова позиція ворога
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!CanGo || playerInRange)
        {
            anim.SetInteger("MushBot", 1);
        }
        else
        {
            anim.SetInteger("MushBot", 2);
        }

        // Перевірка на наявність гравця в зоні видимості
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, lookEnemyRange);
        playerInRange = false;

        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {
                playerInRange = true;
                LookAtPlayer(hit.transform); // Ворога повертаємо до гравця

                if (Time.time >= nextShootTime)
                {
                    Shoot(); // Стрільба
                    nextShootTime = Time.time + shootCooldown; // Затримка до наступного пострілу
                }

                break; // Якщо гравець знайдений, зупиняємо подальші перевірки
            }
        }

        // Якщо гравець поза зоною видимості, ворог патрулює
        if (!playerInRange)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (CanGo)
        {
            LookAtPatrolPoint(); // Перевірка напрямку перед рухом

            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);

            if (transform.position == point1.position)
            {
                Transform t = point1;
                point1 = point2;
                point2 = t;
                CanGo = false;
                StartCoroutine(Waiting());
            }
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);

        // Перевертання ворога під час очікування
        if (transform.rotation.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        CanGo = true;
    }
    // Функція для стрільби
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation); // Створюємо кулю
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*-1 * bulletSpeed; // Надаємо кулі швидкість у напрямку ворога
    }

    void LookAtPatrolPoint()
    {
        // Перевіряємо, в якому напрямку ворог повинен рухатися, щоб повернутися до наступної точки
        if (point1.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // Дивитися праворуч
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // Дивитися ліворуч
        }
    }

    // Ворога повертаємо до гравця
    void LookAtPlayer(Transform player)
    {
        Vector3 direction = player.position - transform.position; // Вектор напрямку до гравця
        if (direction.x > 0)
        {
            // Якщо гравець праворуч, ворог дивиться праворуч
            transform.eulerAngles = new Vector3(0, 180, 0); // Дивитися праворуч
        }
        else
        {
            // Якщо гравець ліворуч, ворог дивиться ліворуч
            transform.eulerAngles = new Vector3(0, 0, 0); // Дивитися ліворуч
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookEnemyRange); // Візуалізація радіуса виявлення гравця
    }
}

