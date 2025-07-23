using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public Vector2 inputDirection,lookDirection;
    PlayerInput _playerInput;
    InputAction moveAction;
    Animator anim;

    private Vector3 touchStart, touchEnd;
    [SerializeField] GameObject dpad;
    public float dPadRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //makes the character look down by default
        lookDirection = new Vector2(0, -1);

        // Referencing the new "player Input" map and associated "Move" Action made in Unity editor
        _playerInput = GetComponent<PlayerInput>();
        moveAction = _playerInput.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        //getting input from touch/mouse controls
        //calculateMobileInput();

        //getting input from keyboard controls
        calculateDesktopInputs();

        //sets up the animator
        animationSetup();

        //moves the player
        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);
    }


    void calculateDesktopInputs()
    {
        // float x = Input.GetAxisRaw("Horizontal");
        // float y = Input.GetAxisRaw("Vertical");

        // inputDirection = new Vector2(x, y).normalized;

        // inputDirections references the new player input made in Unity Editor
        inputDirection = moveAction.ReadValue<Vector2>();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            attack();
        }

    }


    void animationSetup()
    {
        //checking if the player wants to move the character or not
        if (inputDirection.magnitude > 0.1f)
        {
            //changes look direction only when the player is moving, so that we remember the last direction the player was moving in
            lookDirection = inputDirection;

            //sets "isWalking" true. this triggers the walking blend tree
            anim.SetBool("isWalking", true);
        }
        else
        {
            // sets "isWalking" false. this triggers the idle blend tree
            anim.SetBool("isWalking", false);

        }

        //sets the values for input and lookdirection. this determines what animation to play in a blend tree
        anim.SetFloat("inputX", lookDirection.x);
        anim.SetFloat("inputY", lookDirection.y);
        anim.SetFloat("lookX", lookDirection.x);
        anim.SetFloat("lookY", lookDirection.y);
    }

    public void attack()
    {
        anim.SetTrigger("Attack");
    }

    // void calculateMobileInput()
    // {

    //     if (Input.GetMouseButton(0)) //gets left mouse button
    //     {
    //         dpad.gameObject.SetActive(true);

    //         if (Input.GetMouseButtonDown(0))  //the mouse poisition is recorded wher the click started
    //         {
    //             touchStart = Input.mousePosition;
    //         }

    //         touchEnd = Input.mousePosition;  // the mouse position while the button is held down is recorded

    //         float x = touchEnd.x - touchStart.x; // Difference between start and current mouse position is recorded
    //         float y = touchEnd.y - touchStart.y;

    //         inputDirection = new Vector2(x, y).normalized; // input direction is set

    //         if ((touchEnd - touchStart).magnitude > dPadRadius) // moving the dpad while current mouse position is outside the dpad radius
    //         {
    //             dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dPadRadius;
    //         }
    //         else
    //         {
    //             dpad.transform.position = touchEnd; //moving the dpad when the mouse is inside the radius
    //         }
    //     }
    //     else
    //     {
    //         inputDirection = Vector2.zero;
    //         dpad.gameObject.SetActive(false);
    //     }

    // }
}
