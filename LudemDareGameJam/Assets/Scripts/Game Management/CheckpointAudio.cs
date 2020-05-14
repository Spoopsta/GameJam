using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointAudio : MonoBehaviour
{

    public AudioSource checkpoint;

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
}
