using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    //when the player collides with a checkpoint, the old checkpoint gets replaced with the most recent. This allows the player to backtrack, but not lose any progress
    public GameObject currentCheckpoint;

    //this is so we can move the player around by making them an object that the code can read and adjust their transform.position
    //public GameObject player;

    //attempted replacement of gameobject
    public FirstPersonController player;

    //creating a delay for respawning
    public float respawnDelay;


    //death fades
    public Animator animator;
    //public Animation DeathFadeOut;

    public GameObject punchCard1, Door1;

    private int punchCardCount;



    // Start is called before the first frame update
    void Start()
    {

        player.transform.position = currentCheckpoint.transform.position;
        player = GameObject.FindObjectOfType<FirstPersonController>();


    }

    // Update is called once per frame
    void Update()
    {
        DestroyWall();
        ResetLevel();

    }

    /// <summary>
    /// When the player collides with a bad projectile or falls to their death, this code is called and respawns the player on the current checkpoint
    /// </summary>
    public void RespawnPlayer()
    {
        //keep just in case
        //player.transform.position = currentCheckpoint.transform.position;
        StartCoroutine(RespawnCoroutine());
        
        animator.SetTrigger("Death-FadeOut");



    }

    public IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        animator.SetTrigger("Death-FadeIn");
        player.transform.position = currentCheckpoint.transform.position;
    }


    private void DestroyWall()
    {
        if (player.gameObject.GetComponent<FirstPersonController>().punchCards == 1)
        {
            Debug.Log("Wall is kill");
           Door1.gameObject.SetActive(false);
            //Wall1.gameObject.GetComponent<MeshRenderer>().enabled = false;
           // Wall1.gameObject.GetComponent<MeshCollider>().enabled = false;
        }
    }
    private void ResetLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }



    /* public void DeathFade(int levelIndex)
     {
         animator.SetTrigger("Death-FadeOut");
     }
     */
}
