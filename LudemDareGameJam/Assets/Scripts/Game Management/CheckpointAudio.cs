using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointAudio : MonoBehaviour
{

    public AudioSource checkpoint;


    public GameObject checkpointObject;

    private int waitTime = 2;

    private IEnumerator coroutine;

    //public AudioClip checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
           
            Debug.Log("AUDIO PLAY");
            checkpoint.Play();
        }
   
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            coroutine = AudioDeleteDelay();
            StartCoroutine(coroutine);
            
        }
    }

    private IEnumerator AudioDeleteDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            checkpointObject.gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }


}
