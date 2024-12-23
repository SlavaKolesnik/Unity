using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetles : MonoBehaviour
{
    public float speed = 4f;
    bool isWait = false;
    bool isHidden = true;
    public float waitTime = 4f;
    public Transform point;
    public float height = 1.9f;

    // Start is called before the first frame update
    void Start()
    {
        point.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWait)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        }

        if (transform.position == point.position)
        {
            // Якщо isHidden == false, то рухаємось вгору, якщо true — вниз
            if (!isHidden)
            {
                point.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
                isHidden = true;  // Після руху вгору перемикаємося на рух вниз
            }
            else
            {
                point.transform.position = new Vector3(transform.position.x, transform.position.y - height, transform.position.z);
                isHidden = false;  // Після руху вниз перемикаємося на рух вгору
            }

            isWait = true;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        isWait = false;
    }
}

