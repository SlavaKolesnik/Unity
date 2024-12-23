using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInteractive : MonoBehaviour
{
    [SerializeField] private float RayLength = 50f;
    [SerializeField] private Camera PlayerCamera;
    private Ray ray;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Створюэмо луч
        ray = PlayerCamera.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
   
        Debug.DrawRay(ray.origin, ray.direction * RayLength, Color.blue);
        if (Physics.Raycast(ray, out hit, RayLength))
        {
            Debug.Log("Hit smth");
            Debug.DrawRay(ray.origin, ray.direction * RayLength, Color.red);
            if (hit.collider.gameObject.tag == "Mark")
            {
                Shoot();
            }
        }
        
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Shoot is done");
        }
        
    }
}
