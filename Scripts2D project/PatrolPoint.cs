using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 2f;
    public float waitTime = 3f;
    bool CanGo = true;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(point1.position.x, point1.position.y, transform.position.z);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanGo)
        {
            anim.SetInteger("EnemyNotBullet", 1);
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
        else
        {
            anim.SetInteger("EnemyNotBullet", 2);
        }
        IEnumerator Waiting()
        {
            yield return new WaitForSeconds(waitTime);
            if (transform.rotation.y == 0)
            {
                transform.eulerAngles = new Vector3(0, transform.rotation.y + 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            CanGo = true;
        }
    }
}
