using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().RecountHp(damage); // Віднімемо життя гравцеві
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 5f, ForceMode2D.Impulse);
        }
    }
}
