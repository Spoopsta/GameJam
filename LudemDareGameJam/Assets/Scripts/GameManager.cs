using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

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

    public GameObject checkpoint1, checkpoint2, checkpoint3, checkpoint4, checkpoint5, checkpoint6, checkpoint7, checkpoint8, checkpoint9, checkpoint10, checkpointO, checkpointP;


    //death fades
    public Animator animator;
    //public Animation DeathFadeOut;

    public GameObject punchCard1, Door1;

    public GameObject punchCard2, Door2, punchCard3;

    public GameObject winWall;

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
        DebugTeleporting();

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
            //Debug.Log("Wall is kill");
           Door1.gameObject.SetActive(false);
            //Wall1.gameObject.GetComponent<MeshRenderer>().enabled = false;
           // Wall1.gameObject.GetComponent<MeshCollider>().enabled = false;
        }

        if (player.gameObject.GetComponent<FirstPersonController>().punchCards == 2)
        {
            //Debug.Log("Wall is kill");
            Door2.gameObject.SetActive(false);
            //Wall1.gameObject.GetComponent<MeshRenderer>().enabled = false;
            // Wall1.gameObject.GetComponent<MeshCollider>().enabled = false;
        }

        if (player.gameObject.GetComponent<FirstPersonController>().punchCards == 3)
        {
            //Debug.Log("Wall is kill");
            SceneManager.LoadScene(sceneBuildIndex: 2);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("WIN");
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
    }



    /* public void DeathFade(int levelIndex)
     {
         animator.SetTrigger("Death-FadeOut");
     }
     */

    /// <summary>
    /// Used for teleporting between checkpoints for the purposes of testing with the level manager on. delete or comment out when unnecessary. or just make the proper debug tool 4head
    /// </summary>
    private void DebugTeleporting()
    {
        if (Input.GetKeyDown("1"))
        {
            player.transform.position = checkpoint1.transform.position;
        }

        if (Input.GetKeyDown("2"))
        {
            player.transform.position = checkpoint2.transform.position;
        }

        if (Input.GetKeyDown("3"))
        {
            player.transform.position = checkpoint3.transform.position;
        }

        if (Input.GetKeyDown("4"))
        {
            player.transform.position = checkpoint4.transform.position;
        }

        if (Input.GetKeyDown("5"))
        {
            player.transform.position = checkpoint5.transform.position;
        }

        if (Input.GetKeyDown("6"))
        {
            player.transform.position = checkpoint6.transform.position;
        }

        if (Input.GetKeyDown("7"))
        {
            player.transform.position = checkpoint7.transform.position;
        }

        if (Input.GetKeyDown("8"))
        {
            player.transform.position = checkpoint8.transform.position;
        }

        if (Input.GetKeyDown("9"))
        {
            player.transform.position = checkpoint9.transform.position;
        }

        if (Input.GetKeyDown("0"))
        {
            player.transform.position = checkpoint10.transform.position;
        }

        if (Input.GetKeyDown("o"))
        {
            player.transform.position = checkpointO.transform.position;
        }

        if (Input.GetKeyDown("p"))
        {
            player.transform.position = checkpointP.transform.position;
        }
    }
}
