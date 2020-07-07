using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepCollectables : MonoBehaviour
{
    public Animator fadeText;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        fadeText.GetComponent<Animator>();
        //fadeText.SetBool("FadeIN", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            fadeText.SetBool("FadeIN", true);
            Debug.Log("AHAHAHh");
           
        }

        
    }
}
