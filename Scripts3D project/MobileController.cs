using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image Joystick_BG;
    private Image Joystick;
    private Vector2 inputVector; // отиримані коорднаити джостика

    void Start()
    {
        Joystick_BG = GetComponent<Image>();
        Joystick = transform.GetChild(0).GetComponent<Image>(); 
    }

    void Update()
    {
        
    }

    public virtual void OnPointerDown(PointerEventData ped) // натискаємо пальцем
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped) // відпускаємо 
    {
        // reset joystick
        inputVector = Vector2.zero;
        Joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData ped) // перетаскуємо по сцені
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(Joystick_BG.rectTransform,
            ped.position, ped.pressEventCamera, out pos))
        {
            // виведемо результати джостика відносно бекграунда точки утримання
            pos.x = (pos.x / Joystick_BG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / Joystick_BG.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);// Устанволює точні координати по формулі
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : Vector2.zero;

            Joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (Joystick_BG.rectTransform.sizeDelta.x / 2),
                inputVector.y * (Joystick_BG.rectTransform.sizeDelta.y / 2));
        }
        
    }
    public float Horizontal()
    {
        if(inputVector.x !=0)
        {
            return inputVector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float Vertical()
    {
        if (inputVector.y != 0)
        {
            return inputVector.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
