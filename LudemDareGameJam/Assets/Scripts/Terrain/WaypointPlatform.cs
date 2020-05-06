using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPlatform : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 2f;

    int waypointIndex = 0;

    public GameObject Player;

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.transform.parent = null;
    }

}
