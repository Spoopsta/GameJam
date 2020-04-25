using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Wallrun : MonoBehaviour
{

    //wall running in directions
    private bool isWallR = false;
    private bool isWallL = false;

    private RaycastHit hitR;
    private RaycastHit hitL;

    private int jumpCount = 0;
    private RigidbodyFirstPersonController cc;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<RigidbodyFirstPersonController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.Grounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && cc.Grounded && jumpCount <= 1)
        {
            if (Physics.Raycast(transform.position, transform.right, out hitR, 1))
            {

                if (hitR.transform.tag == "Wall")
                {
                    isWallR = true;
                    isWallL = false;
                    jumpCount += 1;
                    rb.useGravity = false;
                    StartCoroutine(afterRun());
                }
            }

            if (Physics.Raycast(transform.position, -transform.right, out hitL, 1))
            {

                if (hitL.transform.tag == "Wall")
                {
                    isWallL = true;
                    isWallR = false;
                    jumpCount += 1;
                    rb.useGravity = false;
                    StartCoroutine(afterRun());
                }
            }
        }
    }

    IEnumerator afterRun()
    {
        yield return new WaitForSeconds(0.5f);
        isWallL = false;
        isWallR = false;
        rb.useGravity = true;
    }
}
