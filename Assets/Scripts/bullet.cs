using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float bulletspeed = 100f;
    public float timeRemaining = 3;
    public GameObject bulletImpact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Rigidbody>().velocity = transform.forward * bulletspeed;
        if(timeRemaining <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timeRemaining -= Time.deltaTime;
        }
    

        //transform.Translate() = transform.forward * 100f;
    }
    private void OnCollisionEnter(Collision collision)
    {



        //if the objet we have collided wiht has tag enemy then destroy object or pass

        //Transform point = collision.gameObject.transform.position;
        //Instantiate(bulletImpact, point , Quaternion.Euler());

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("U missed");
        }


        
        //Destroy(collision.gameObject);
        //Destroy(gameObject);
    }
}
