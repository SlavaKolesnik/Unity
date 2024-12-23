using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float speed = 400f;
    public Transform target;
    public float offsetY = -10f; // Зсув по осі Y

    void Start()
    {
        // Ініціалізація позиції камери на старті
        transform.position = new Vector3(target.position.x, target.position.y + offsetY, transform.position.z);
    }

    // LateUpdate викликається після Update
    void LateUpdate()
    {
        Vector3 targetPosition = target.position;
        targetPosition.y += offsetY; // Додаємо зміщення до осі Y
        targetPosition.z = transform.position.z;

        // Інтерполяція камери до позиції персонажа зі зміщенням по осі Y
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
