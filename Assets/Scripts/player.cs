using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    //movement
    public float mouseSensitivity = 1.0f;
    public Transform myCameraHead;
    float cameraVerticalMovement;
    public CharacterController mycc;
    public float runningSpeed = 20f;
    public float walkingSpeed = 10f;

    //gun/bullet
    

    //jumping section
    public float gravityModifier = 10f;
    public float jumpHeight = 10f;
    Vector3 velocity;


    //crouching section
    Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    Vector3 normalScale;
    public float crouchSpeed = 10f;
    bool isCrouching = false;
    public Transform myBody;
    float initialControllerHeight;

    //animation

    public Animator myAnimator;

    //sliding section
    public float slidingSpeed = 25f;
    bool isRunning = false;
    public bool isSliding = false;
    public float slideTime = 0;
    public float maxslidetime = 3;


    private void Start()
    {
        normalScale = myBody.localScale;
        initialControllerHeight = mycc.height;
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {

        playermovement();
        mouseMovement();
        
        jump();
        crouch();
        Sprint();
        Sliding();


        //health update
        //score update
        //reloading





        //firingPosition.lookAt(crosshair.transform);



    }

    private void Sliding()
    {
        if(isSliding)
        {
            slideTime += Time.deltaTime;
        }
        if(slideTime >= maxslidetime)
        {
            isSliding = false;
            velocity = Vector3.zero;
        
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCrouching();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            EndCrouching();
        }
    }

    private void StartCrouching()
    {
        isCrouching = true;
        mycc.height /= 2;
        myBody.localScale = crouchScale;

        if (isRunning)
        {
            slideTime = 0;
            isSliding = true;
            // this velocity is being passed on to mycc by
            velocity = Vector3.ProjectOnPlane(myCameraHead.transform.forward, Vector3.up);
        }
    }
    private void EndCrouching()
    {
        myBody.localScale = normalScale;
        mycc.height = initialControllerHeight;
        isCrouching = false;

    }



    private void jump()
    {

        // only allowed to jump if we are grounded and press spacebar
        if (Input.GetButtonDown("Jump") && mycc.isGrounded)
        {
            velocity.y = jumpHeight;//yvelocity is being subtrated in playeer movement to create effect of gravity
            mycc.Move(velocity);
        }

    }

   

    private void mouseMovement()
    {
        float xmovement = Input.GetAxisRaw("Mouse X");
        xmovement = xmovement * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xmovement);//left right body


        float ymovement = Input.GetAxisRaw("Mouse Y");

        ymovement = ymovement * mouseSensitivity * Time.deltaTime;

        ymovement = ymovement * -1;

        cameraVerticalMovement += ymovement;

        cameraVerticalMovement = Mathf.Clamp(cameraVerticalMovement, -80f, 80f);

        myCameraHead.localRotation = (Quaternion.Euler(cameraVerticalMovement, 0, 0)); //rotating  up and down camera head



        // myCameraHead.transform.Rotate(Vector3.right * ymovement);


    }

    private void playermovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        if (!isSliding)
        {
            if (isRunning && !isCrouching)
            {
                movement *= (runningSpeed * Time.deltaTime);
            }
            else
            {
                movement *= (walkingSpeed * Time.deltaTime);
                if (isCrouching == true)
                {
                    movement *= (crouchSpeed * Time.deltaTime);

                }
                else
                {
                    movement *= (walkingSpeed * Time.deltaTime);
                }
            }

        }
        myAnimator.SetFloat("PlayerSpeed", movement.magnitude);
        mycc.Move(movement);




        // get the cc and then use its move function


        //Vector3 yvelocity = mycc.velocity + Physics.gravity;

        //acceleration
        velocity.y += mycc.velocity.y + Physics.gravity.y * gravityModifier;

        if (mycc.isGrounded)
        {
            velocity.y = Physics.gravity.y * Time.deltaTime;
            // in our y vector = -1 * 0.016 = -0.016

        }

        mycc.Move(velocity);

    }


}
