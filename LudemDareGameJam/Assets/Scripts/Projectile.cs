using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float startPosZ;
    private float speed;
    private int wait;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        startPosZ = transform.position.z;
        speed = -0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<Transform>().rotation.eulerAngles.z == 270)
        {
            transform.Translate(speed, 0, 0, Space.Self);
        }
        else if (GetComponentInParent<Transform>().rotation.eulerAngles.z == 0) {
            if (GetComponentInParent<Transform>().rotation.eulerAngles.y == 0)
            {
                transform.Translate(speed, 0, 0, Space.Self);
            }
            else if (GetComponentInParent<Transform>().rotation.eulerAngles.y == 180) {
                transform.Translate(speed, 0, 0, Space.Self);
            }
            else if (GetComponentInParent<Transform>().rotation.eulerAngles.y == 90)
            {
                transform.Translate(speed, 0, 0, Space.Self);
            }
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Boundary"))
        {
            transform.position = new Vector3(startPosX, startPosY, startPosZ);
        }
    }
}
