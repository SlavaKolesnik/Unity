using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpHeight;
    public Transform groundCheck;
    bool isGrounded;
    bool isClimbing = false;
    Animator anim;
    bool isHit = false;
    private Coroutine hitCoroutine;
    public Main main;


    int curHp;
    int maxHp = 3;
    int curCoint = 0;

    public GameObject flashlight;
    public bool isFlashlight = false;
    public bool isButs = false;
    public bool isShoot = false;
    public bool isCanShoot = false;
    public float flashlightRange = 4f; // ĳ������ 䳿 ��������
    private Coroutine damageCoroutine;

    public GameObject bulletPrefab; // ������ ���
    public Transform bulletSpawnPoint; // ����� ����� ���
    public float bulletSpeed = 15f;
    public float shootCooldown = 0.3f; // ��� ���������� �� ���������
    private float nextShootTime = 0f; // ���, ���� ������� ���� ����� �������
    private int curBullet = 0;

    private bool isButtonPressed = false;
    private bool isShootPressed = false;

    public Inventory inventory;
    public SoundEffector soundEffector;

    public Joystick joystick;

    public GameObject buttonJump;
    public GameObject fireButton;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHp = maxHp;
        if (!PlayerPrefs.HasKey("bullets"))
        {
            PlayerPrefs.SetInt("bullets", 0);
        }
        curBullet = PlayerPrefs.GetInt("bullets");
        anim.SetInteger("State", 1);
        //buttonJump.SetActive(false);
        //fireButton.SetActive(false);

    }


    void Update()
    {



        CheckGround();
        FlashlightUse();

        // ������ �������
        Shoot();

        if (isGrounded && !isClimbing)
        {
            if (joystick.Horizontal < 0.3f && joystick.Horizontal > -0.3f)
            {
                if (isFlashlight && !isShoot)
                {
                    anim.SetInteger("State", 4);

                }
                else if (!isFlashlight && !isShoot)
                {
                    anim.SetInteger("State", 1); // ������� ������ ��� ��������

                }
                else if (isShoot)
                {
                    anim.SetInteger("State", 8); // ������� �������

                }
            }
            else
            {
                Flip();
                if (isFlashlight && !isClimbing && !isShoot)
                {
                    anim.SetInteger("State", 5);

                }
                else if (!isFlashlight && !isClimbing && isShoot)
                {
                    anim.SetInteger("State", 9);

                }
                else
                {
                    if (!isClimbing)
                    {
                        anim.SetInteger("State", 2);

                    }
                }
            }
        }
        else
        {
            if (!isClimbing && isButs)
            {
                anim.SetInteger("State", 3);

            }
        }
    }

    void FixedUpdate() // ��� � ������
    {
        if (joystick.Horizontal >= 0.2f)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (joystick.Horizontal <= -0.2f)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        //{
        //    rb.velocity = new Vector2(joystick.Horizontal * speed, rb.velocity.y);
        //}

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded && isButs)
        //{
        //    rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        //    soundEffector.PlayJumpSound();
        //}

    }
    public void Jump()
    {
        if (isGrounded && isButs)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            soundEffector.PlayJumpSound();
        }
    }

    void Flip() //������� ��������� �� 180 ������� 
    {
        if (joystick.Horizontal >= 0.3f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (joystick.Horizontal <= -0.3f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void CheckGround() //���������� �� ��������� ����
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded) // ������� ������ ���� �� �� ����
        {
            anim.SetInteger("State", 3);
        }
    }
    public void RecountHp(int deltaHp)
    {
        if (deltaHp > 0 && curHp < maxHp)
        {
            curHp += deltaHp;

            // ��������, ��� curHp �� ����������� maxHp
            if (curHp > maxHp)
            {
                curHp = maxHp;
            }
        }
        else if (deltaHp < 0) // ���� ������� ������ �����������
        {
            soundEffector.PlayDamageSound();
            if (hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine);
            }
            isHit = true;
            hitCoroutine = StartCoroutine(OnHit());

            curHp += deltaHp; // �������� HP
        }

        // �������� �� ������
        if (curHp <= 0)
        {
            curHp = 0; // ������ �� ��'����� ������� HP
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1f);
        }
    }
    IEnumerator OnHit()
    {
        // �������� ��������� SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // ��������� ���������
        while (spriteRenderer.color.g > 0)
        {
            spriteRenderer.color = new Color(1f, spriteRenderer.color.g - 0.04f, spriteRenderer.color.b - 0.04f);
            yield return new WaitForSeconds(0.02f);
        }

        // ���������� �� ����������� �����
        while (spriteRenderer.color.g < 1)
        {
            spriteRenderer.color = new Color(1f, spriteRenderer.color.g + 0.04f, spriteRenderer.color.b + 0.04f);
            yield return new WaitForSeconds(0.02f);
        }

        // �������� ����� ���������
        isHit = false;
    }

    void Lose() // ���������� ����� 
    {
        main.GetComponent<Main>().Lose();
    }

    public void OnShootButtonPressed()
    {
        isShootPressed = true;
    }

    public void OnShootButtonReleased()
    {
        isShootPressed = false;
    }

    public void Shoot()
    {
        // ������ ��� ��������� ��� ������ ����, ���� ������� ��� ����������
        if (isShootPressed && Time.time >= nextShootTime && isGrounded && !isClimbing && curBullet > 0 && isCanShoot)
        {
            isShoot = true;
            soundEffector.PlayShootSound();
            // ��������� ��� ���������� �������
            nextShootTime = Time.time + shootCooldown;

            // ��������� ����
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // ������������ �������� ��� �� �� X
            float direction = transform.localRotation.y == 0 ? 1 : -1;
            bulletRb.velocity = new Vector2(bulletSpeed * direction, 0);
            RecountBullet(-1);

        }
        else
        {
            isShoot = false;
        }
    }

    public void OnFlashlightButtonPressed()
    {
        isButtonPressed = true;
    }

    public void OnFlashlightButtonReleased()
    {
        isButtonPressed = false;
    }

    public void FlashlightUse()
    {
        if (isButtonPressed && isGrounded && !isClimbing)
        {
            flashlight.GetComponent<SpriteRenderer>().enabled = true;
            isFlashlight = true;

            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamageEnemiesInLight());
            }
        }
        else
        {
            flashlight.GetComponent<SpriteRenderer>().enabled = false;
            isFlashlight = false;

            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }


    IEnumerator DamageEnemiesInLight()
    {
        while (isFlashlight)
        {
            // ��������� ��� ������ � ����� 䳿 ��������
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, flashlightRange);
            foreach (Collider2D enemyCollider in hitEnemies)
            {
                if (enemyCollider.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(1); // �������� ����� ������
                    }
                }
            }
            yield return new WaitForSeconds(1f); // ��������� ����� ����� �������
        }
    }
    void OnDrawGizmosSelected() // ��� ���������� �������� �������� � ��������
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, flashlightRange);
    }

    private void OnTriggerStay2D(Collider2D collision) // ������� �� ��������
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isClimbing = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            if (joystick.Vertical == 0)
            {
                anim.SetInteger("State", 6);
            }
            else
            {
                anim.SetInteger("State", 7);
                transform.Translate(Vector3.up * joystick.Vertical * speed * 0.5f * Time.deltaTime);
            }

        }
    }

    public void RecountCoin(int money)
    {
        curCoint += money;
    }

    public void RecountBullet(int bullet)
    {
        curBullet += bullet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "heart")
        {
            inventory.Add_hp();
            Destroy(collision.gameObject);
            soundEffector.PlayCoinSound();
        }
        if (collision.gameObject.tag == "coin")
        {
            RecountCoin(1);
            Destroy(collision.gameObject);
            soundEffector.PlayCoinSound();
        }
        if (collision.gameObject.tag == "bullet5")
        {
            RecountBullet(5);
            Destroy(collision.gameObject);
            soundEffector.PlayCoinSound();
        }
        if (collision.gameObject.tag == "bullet10")
        {
            RecountBullet(10);
            Destroy(collision.gameObject);
            soundEffector.PlayCoinSound();
        }
        if (collision.gameObject.tag == "bullet15")
        {
            RecountBullet(15);
            Destroy(collision.gameObject);
            soundEffector.PlayCoinSound();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isClimbing = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "boots")
        {
            isButs = true;
            buttonJump.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "CanShoot")
        {
            isCanShoot = true;
            fireButton.SetActive(true);
            Destroy(collision.gameObject);
        }

    }
    public int GetCoins()
    {
        return curCoint;
    }
    public int GetHP()
    {
        return curHp;
    }
    public int GetBullets()
    {
        return curBullet;
    }
}