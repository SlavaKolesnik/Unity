using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float speed = 400f;
    public Transform target;
    public float offsetY = -10f; // ���� �� �� Y

    void Start()
    {
        // ����������� ������� ������ �� �����
        transform.position = new Vector3(target.position.x, target.position.y + offsetY, transform.position.z);
    }

    // LateUpdate ����������� ���� Update
    void LateUpdate()
    {
        Vector3 targetPosition = target.position;
        targetPosition.y += offsetY; // ������ ������� �� �� Y
        targetPosition.z = transform.position.z;

        // ������������ ������ �� ������� ��������� � �������� �� �� Y
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
