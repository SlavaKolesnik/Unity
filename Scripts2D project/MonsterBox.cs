using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBox : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 2f;
    public float waitTime = 3f;
    Animator anim;

    bool CanGo = true;
    void Start()
    {
        gameObject.transform.position = new Vector3(point1.position.x, point1.position.y, transform.position.z);
        anim = GetComponent<Animator>();
        anim.SetInteger("MonaterBox", 2);  // Під час старту, задаємо анімацію стояння
    }

    void Update()
    {
        if (CanGo)
        {
            anim.SetInteger("MonaterBox", 1);  // Коли рухається, задаємо анімацію руху

            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);

            if (transform.position == point1.position)
            {
                anim.SetInteger("MonaterBox", 2);  // Коли досягли точки, задаємо анімацію стояння

                Transform t = point1;
                point1 = point2;
                point2 = t;
                CanGo = false;
                StartCoroutine(Waiting());
            }
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
