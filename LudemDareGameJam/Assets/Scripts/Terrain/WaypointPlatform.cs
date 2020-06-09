using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPlatform : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 2f;

    float downSpeed = 0;

    int waypointIndex = 0;

    public GameObject Player;

    //testing player touching the thing to make it start moving
    public bool isStationary = false;

    //setting up a falling platform when the player touches
    public bool isFallingPlatform = false;

   

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        // if the platform is not stationary, start moving towards the next waypoint 
        if (isStationary == false)
        {
            //when a waypoint is reached, +1 to go to next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }

            if (isFallingPlatform)
            {
                downSpeed += Time.deltaTime/10;
                transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
            }

        }


        //when the limit of waypoints is reached, return to the first waypoint.
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
            
        }

     /*   if (waypointIndex == waypoints.Length) //&& this.gameObject.tag.Equals("Moving")) 
        {
            Debug.Log("made it to the start chief");
            waypointIndex = 0;
            
           // isStationary = true;
        }
        */

        if (waypointIndex == 0 && this.gameObject.tag.Equals("Moving"))
        {
            //Debug.Log("nani");
            isStationary = true;
        }
    }


   /* private void OnTriggerEnter(Collider other)
    {
        //if the player touches the object, the platform will begin to move and the player becomes a child object of the platform
        if (other.gameObject == Player)
        {
            isStationary = false;
            Player.transform.parent = transform;
        }

        else if (isFallingPlatform == false)
        {
            isFallingPlatform = true;
        }
    }
    */

    private void OnTriggerExit(Collider other)
    {
        //player is no longer a child object of the platform / object.
        Player.transform.parent = null;
       // isStationary = true;
        
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player)
        {
            isStationary = false;
            Player.transform.parent = transform;
        }
    }

}
