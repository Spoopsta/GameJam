using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private int iCounter;
    // Start is called before the first frame update
    void Start()
    {
        iCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<MeshRenderer>().enabled) {
            iCounter++;
            if (iCounter > 550) {
                GetComponent<MeshRenderer>().enabled = true;
                GetComponent<BoxCollider>().enabled = true;
                GetComponentInChildren<ParticleSystem>().Play();
                iCounter = 0;
            }
        }        
    }
}
