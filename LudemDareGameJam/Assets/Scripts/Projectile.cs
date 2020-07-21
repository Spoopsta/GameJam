using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float startPosZ;
    public float speed;
    private int wait;
   
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Boundary") || collision.gameObject.tag.Equals("Void")|| collision.gameObject.tag.Equals("Wall"))
        {
            // transform.position = new Vector3(startPosX, startPosY, startPosZ);

            Destroy(this.gameObject);
        }
    }

}

