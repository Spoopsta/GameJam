using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    public float respawnDelay;

    private void OnCollisionEnter(Collision other)
    {
        player.transform.position = respawnPoint.transform.position;     
    }
   
}
