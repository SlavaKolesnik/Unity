using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCollider : MonoBehaviour
{
    public GameObject ohNo;
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
            StartCoroutine(Destroy());
        }
    }
    IEnumerator Destroy()
    {
        ohNo.SetActive(true);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
