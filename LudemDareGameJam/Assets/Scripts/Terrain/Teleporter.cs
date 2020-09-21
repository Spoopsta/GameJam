using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportTarget;
    public FirstPersonController fpc;

    void OnTriggerEnter(Collider other)
    {
        fpc.transform.position = teleportTarget.transform.position;    
    }


}
