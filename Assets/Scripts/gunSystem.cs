using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunSystem : MonoBehaviour


{
    public Transform firingPosition;
    public GameObject Bullet;
    public GameObject muzzleflash;
    public GameObject bulletimpact;
    public float closerange;
    public float gunRange;
    public Transform myCameraHead;

    public bool autoFire = false;
    public int magSize = 20;
    public int bulletsInGun = 0;
    public int totalBullets = 90;
    public bool shootingInput;




    // Start is called before the first frame update
    void Start()
    {
        bulletsInGun = magSize;
        totalBullets -= magSize;
    }

    // Update is called once per frame
    void Update()
    {
        bulletShooting();
        Reload();
        

    }

    private void bulletShooting()
    {


        if (autoFire)
        {
            shootingInput = Input.GetMouseButton(0);
        }
        else
        {
            shootingInput = Input.GetMouseButtonDown(0);

        }







        if (shootingInput && bulletsInGun > 0)
        {
            RaycastHit hit;
            // only debug logs when hits object
            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, gunRange))
            {
                if (Vector3.Distance(firingPosition.position, hit.point) > closerange)
                {
                    firingPosition.LookAt(hit.point);

                }
                else
                {
                   
                }

                if (!hit.collider.CompareTag("Enemy"))
                {
                    Instantiate(bulletimpact, hit.point, Quaternion.Euler(hit.normal));
                    
                }

            }

            Instantiate(Bullet, firingPosition.position, firingPosition.rotation);
            Instantiate(muzzleflash, firingPosition.position, firingPosition.rotation);
            bulletsInGun--;


           
        }





    }

    private void Reload()
    {
        if (Input.GetKey(KeyCode.R) && bulletsInGun < magSize)
        {
            int bulletsToAdd = magSize - bulletsInGun;
            if (totalBullets > bulletsToAdd)
            {
                totalBullets -= bulletsToAdd;
                bulletsInGun += bulletsToAdd;
            }
            else
            {
                bulletsInGun += totalBullets;
                totalBullets = 0;
            }
        }
    }
}
