using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Vector2 turn;
    public float sensivity = 0.5f;

    public float horizontalMove;
    public float verticalMove;
    public float speed = 2;
    public GameObject player;
    public Vector3 deltaMove;

    private Animator PlayerAnimator;

    public MobileController _mobileControllerMovement;
    public MobileController _mobileControllerRotate;
    // Update is called once per frame

    void Start()
    {
        //«аборон€Їмо курсору вийти за рамки екрану гри
       //Cursor.lockState = CursorLockMode.Locked;
       PlayerAnimator = player.GetComponent<Animator>();
        _mobileControllerMovement = GameObject.FindGameObjectWithTag("Joystic_BG").GetComponent<MobileController>();
        _mobileControllerRotate = GameObject.FindGameObjectWithTag("Joystic_BG_Rotate").GetComponent<MobileController>();
    }

    void Update()
    {
        //ќтримуЇмо координати мишки
        //turn.x += Input.GetAxis("Mouse X") * sensivity;
        //turn.y += Input.GetAxis("Mouse Y") * sensivity;
        if(Input.GetAxis("Mouse X") != 0)
        {
            turn.x += Input.GetAxis("Mouse X") * sensivity;
        }
        else
        {
            turn.x += _mobileControllerRotate.Horizontal() * sensivity;
        }
        if(Input.GetAxis("Mouse Y") != 0)
        {
            turn.y += Input.GetAxis("Mouse Y") * sensivity;
        }
        else
        {
            turn.y += _mobileControllerRotate.Horizontal() * sensivity;
        }
        //«робити обмеженн€  "x" ≥ "y"
        turn.y = Mathf.Clamp(turn.y, -20, 20);

        //¬иконуЇмо поворот дл€ камери
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
        //¬иконуЇмо поворот дл€ гравц€
        player.transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        //¬иконуЇмо рух дл€ гравц€
        horizontalMove = _mobileControllerMovement.Horizontal();
        verticalMove = _mobileControllerMovement.Vertical();

        deltaMove = new Vector3(horizontalMove, 0, verticalMove) * speed * Time.deltaTime;
        player.transform.Translate(deltaMove);

        PlayerController();
    }

    void PlayerController()
    {
        if(verticalMove > 0)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                PlayerAnimator.SetInteger("Move", 1);
                speed = 4;
            }
            else
            {
                PlayerAnimator.SetInteger("Move", 2);
                speed = 2;
            }
            
        } 

        else if (verticalMove < 0)
        {
            PlayerAnimator.SetInteger("Move", -1);
            speed = 2;
        }

        else
        {
            PlayerAnimator.SetInteger("Move", 0);
            speed = 0;
        }
    }
}
