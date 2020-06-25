using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private float iCounter;
    
    private float waitTime = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        iCounter = 0;

    }

    // Update is called once per frame
    void Update()
    {
       

        if (!GetComponent<MeshRenderer>().enabled)
        {
            //iCounter += Time.deltaTime + 1;
            iCounter += Time.deltaTime * 1;
            // coroutine = pickupRespawnDelay();
            //StartCoroutine(coroutine);

        }

        if (iCounter > waitTime)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
            GetComponentInChildren<ParticleSystem>().Play();
            iCounter = 0;
        }
    }

}

//on the right track with this though its not doing whaat i want it to do
    /*
    private IEnumerator coroutine;


    private IEnumerator pickupRespawnDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
            GetComponentInChildren<ParticleSystem>().Play();
            StopCoroutine(coroutine);
        }
    }
    */
