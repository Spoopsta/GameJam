using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportTarget;
    public FirstPersonController fpc;
    public GameObject FallingTree;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            fpc.transform.position = teleportTarget.transform.position;
        }

        if (other.gameObject == FallingTree)
        {
            FallingTree.transform.position = teleportTarget.transform.position;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
