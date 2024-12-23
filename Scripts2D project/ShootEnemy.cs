using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public float lookEnemyRange = 5f; // ����� ��������� ������
    public GameObject bulletPrefab; // ������ ���
    public Transform bulletSpawnPoint; // �����, ����� ����� ����
    public float bulletSpeed = 15f; // �������� ���
    public float shootCooldown = 1f; // ��� �� ���������
    private float nextShootTime = 0f; // ��� ��� ���������� �������

    public Transform point1;
    public Transform point2;
    public float speed = 2f; // �������� ���� ������
    public float waitTime = 3f; // ��� ���������� ����� ����� ��������
    private bool CanGo = true;
    private bool playerInRange = false;
    Animator anim;


    void Start()
    {
        transform.position = new Vector3(point1.position.x, point1.position.y, transform.position.z); // ��������� ������� ������
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

        // �������� �� �������� ������ � ��� ��������
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, lookEnemyRange);
        playerInRange = false;

        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {
                playerInRange = true;
                LookAtPlayer(hit.transform); // ������ ��������� �� ������

                if (Time.time >= nextShootTime)
                {
                    Shoot(); // �������
                    nextShootTime = Time.time + shootCooldown; // �������� �� ���������� �������
                }

                break; // ���� ������� ���������, ��������� �������� ��������
            }
        }

        // ���� ������� ���� ����� ��������, ����� ��������
        if (!playerInRange)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (CanGo)
        {
            LookAtPatrolPoint(); // �������� �������� ����� �����

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

        // ������������ ������ �� ��� ����������
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
    // ������� ��� �������
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation); // ��������� ����
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*-1 * bulletSpeed; // ������ ��� �������� � �������� ������
    }

    void LookAtPatrolPoint()
    {
        // ����������, � ����� �������� ����� ������� ��������, ��� ����������� �� �������� �����
        if (point1.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // �������� ��������
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // �������� ������
        }
    }

    // ������ ��������� �� ������
    void LookAtPlayer(Transform player)
    {
        Vector3 direction = player.position - transform.position; // ������ �������� �� ������
        if (direction.x > 0)
        {
            // ���� ������� ��������, ����� �������� ��������
            transform.eulerAngles = new Vector3(0, 180, 0); // �������� ��������
        }
        else
        {
            // ���� ������� ������, ����� �������� ������
            transform.eulerAngles = new Vector3(0, 0, 0); // �������� ������
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookEnemyRange); // ³��������� ������ ��������� ������
    }
}

