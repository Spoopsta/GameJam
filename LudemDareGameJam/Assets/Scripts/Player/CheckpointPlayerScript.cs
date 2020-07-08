﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointPlayerScript : MonoBehaviour
{
    public GameManager checkpointManager;


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
        if (other.gameObject.tag.Equals ("Checkpoints"))
        {
            Debug.Log("checkpoint");
            checkpointManager.GetComponent<GameManager>().currentCheckpoint = other.gameObject;
            
        }

        if (other.gameObject.tag.Equals("Void") || other.gameObject.tag.Equals("Projectile"))
       {
            Debug.Log("void / Projectile");
            checkpointManager.GetComponent<GameManager>().RespawnPlayer();
            checkpointManager.GetComponent<GameManager>().playerDeath = true;
            //checkpointManager.GetComponent<GameManager>().blackOutSquare.SetActive(true);
            //checkpointManager.GetComponent<GameManager>().animator.SetTrigger("Death-FadeIn");
        }
    }
}
