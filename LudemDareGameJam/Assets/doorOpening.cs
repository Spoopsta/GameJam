using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class doorOpening : MonoBehaviour
{

    private Animator anim, anim2, anim3;

    private FirstPersonController fpc;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim2 = GetComponent<Animator>();
        anim3 = GetComponent<Animator>();
        //fpc = GetComponent<FirstPersonController>();
        fpc = GameObject.FindObjectOfType<FirstPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && fpc.gameObject.GetComponent<FirstPersonController>().punchCards == 1 )
        {
            anim.SetBool("Open1", true);
        }

        if (other.gameObject.tag.Equals("Player") && fpc.gameObject.GetComponent<FirstPersonController>().punchCards == 2)
        {
            anim2.SetBool("Open2", true);
        }

        if (other.gameObject.tag.Equals("Player") && fpc.gameObject.GetComponent<FirstPersonController>().punchCards == 3)
        {
            anim3.SetBool("Open3", true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
