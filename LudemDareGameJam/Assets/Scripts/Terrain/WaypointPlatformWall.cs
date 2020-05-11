using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPlatformWall : MonoBehaviour
{
    //had to make this script without the child things because it created quite a fun bug in which the player would teleport wildly all over the map

    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 2f;

    int waypointIndex = 0;

    //public GameObject Player;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);


        //when a waypoint is reached, +1 to go to next waypoint
        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }


        //when the limit of waypoints is reached, return to the first waypoint.
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;

        }
    }
}
