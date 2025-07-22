using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed; //Player's speed
    public Vector2 inputDirection, lookDirection; //player's input along x and y axis and corresponding look direction
    Animator anim; // player animator

    private Touch theTouch;
    private Vector3 touchStart, touchEnd;
    [SerializeField] GameObject dpad; //dpad gameobject icon

    [SerializeField] GameObject dpadBoundary; //dpad boundary gameobject icon
    [SerializeField] float dPadRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();


        //makes the character look down by default
        lookDirection = new Vector2(0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        //getting input from touch/mouse controls
        //calculateMobileInput();

        //getting input from keyboard controls
        //calculateDesktopInputs();

        //getting input from Touch controls alone
        calculateTouchInput();

        //sets up the animator
        animationSetup();

        //moves the player
        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);
    }


    void calculateDesktopInputs()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(x, y).normalized;

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

    void calculateTouchInput()
    {
        if (Input.touchCount > 0 ) //gets first finger touch
        {
            theTouch = Input.GetTouch(0); // get screen touch
            dpad.gameObject.SetActive(true);
            dpadBoundary.gameObject.SetActive(true);

            if (theTouch.phase == TouchPhase.Began)  //first screen touch poisition is recorded where the click started
            {
                touchStart = theTouch.position;
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEnd = theTouch.position;  // the screen touch position while the button is held down is recorded

                float x = touchEnd.x - touchStart.x; // Difference between start and current mouse position is recorded
                float y = touchEnd.y - touchStart.y;

                Vector2 inputDirection = new Vector2(x, y).normalized; // input direction is set

                dpadBoundary.transform.position = touchStart; // boundary position is touchStart

                if ((touchEnd - touchStart).magnitude > dPadRadius) // moving the dpad while current mouse position is outside the dpad radius
                {
                    dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dPadRadius;
                }
                else
                {
                    dpad.transform.position = touchEnd; //moving the dpad when the mouse is inside the radius
                }
            }     
        }
        else
        {
            inputDirection = Vector2.zero;
            dpad.gameObject.SetActive(false);
            dpadBoundary.gameObject.SetActive(false);
        }

    }
    
     void calculateMobileInput()
    {

        if (Input.GetMouseButton(0)) //gets left mouse button
        {
            dpad.gameObject.SetActive(true);
            dpadBoundary.gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))  //the mouse poisition is recorded wher the click started
            {
                touchStart = Input.mousePosition;
            }

            touchEnd = Input.mousePosition;  // the mouse position while the button is held down is recorded

            float x = touchEnd.x - touchStart.x; // Difference between start and current mouse position is recorded
            float y = touchEnd.y - touchStart.y;

            inputDirection = new Vector2(x, y).normalized; // input direction is set

            dpadBoundary.transform.position = touchStart; // boundary position is touchStart

            if ((touchEnd - touchStart).magnitude > dPadRadius) // moving the dpad while current mouse position is outside the dpad radius
            {
                dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dPadRadius;

            }
            else
            {
                dpad.transform.position = touchEnd; //moving the dpad when the mouse is inside the radius
            }
        }
        else
        {
            inputDirection = Vector2.zero;
            dpad.gameObject.SetActive(false);
            dpadBoundary.gameObject.SetActive(false);
        }

    }
}
