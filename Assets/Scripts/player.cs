using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
   public float mouseSensitivity = 1.0f;
   public Transform myCameraHead;
   float cameraVerticalMovement;
   public CharacterController mycc;
   public Transform firingPosition;
   public GameObject Bullet;
   public GameObject muzzleflash;
   public GameObject bulletimpact;
   public float closerange;
   public float gunRange;
   public float gravityModifier = 10f;
    public float jumpHeight = 10f;
    Vector3 yvelocity;

   

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        playermovement();
        mouseMovement();
        bulletShooting();
        jump();
        crouch();
        Sprint();

        //health update
        //score update
        //reloading
        




        //firingPosition.lookAt(crosshair.transform);



    }

    private void Sprint()
    {
        
    }

    private void crouch()
    {
        
    }

    private void jump()
    {

        // only allowed to jump if we are grounded and press spacebar
        if(Input.GetButtonDown("Jump") && mycc.isGrounded)
        {
            yvelocity.y = jumpHeight;//yvelocity is being subtrated in playeer movement to create effect of gravity
            mycc.Move(yvelocity);
        }
        
    }

    private void bulletShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // only debug logs when hits object
            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, gunRange))
            {
                if (Vector3.Distance(firingPosition.position, hit.point) > closerange)
                {
                    firingPosition.LookAt(hit.point);
                    Instantiate(bulletimpact, hit.point, Quaternion.Euler(hit.normal));
                }

                

            }
            else
            {
                Instantiate(Bullet, firingPosition.position, firingPosition.rotation);
                Instantiate(muzzleflash, firingPosition.position, firingPosition.rotation);
            }
           

            
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

        cameraVerticalMovement = Mathf.Clamp(cameraVerticalMovement,-80f, 80f);

        myCameraHead.localRotation = (Quaternion.Euler(cameraVerticalMovement, 0,0)); //rotating  up and down camera head

        

       // myCameraHead.transform.Rotate(Vector3.right * ymovement);

        

        
        
       
    }

    private void playermovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;

        // get the cc and then use its move function
        mycc.Move(movement);

        //Vector3 yvelocity = mycc.velocity + Physics.gravity;

        //seperating the y vector/velociity and only making the addition in that
        yvelocity.y += mycc.velocity.y + Physics.gravity.y * gravityModifier;

        if (mycc.isGrounded)
        {
            yvelocity.y = Physics.gravity.y * Time.deltaTime;
            // in our y vector = -1 * 0.016 = -0.016

        }
        mycc.Move(yvelocity);
        

    }
    
    
}
 