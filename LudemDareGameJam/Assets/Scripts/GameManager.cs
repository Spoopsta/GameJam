using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;


[System.Serializable]
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

    public GameObject checkpoint1, checkpoint2, checkpoint3, checkpoint4, checkpoint5, checkpoint6, checkpoint7, checkpoint8, checkpoint9, checkpoint10, checkpointO, checkpointP,
        checkpointL, checkpointI;


    //death fades
    public Animator animator;

    //public Animation DeathFadeOut;

    public GameObject punchCard1, Door1;

    public GameObject punchCard2, Door2;

    public GameObject punchCard3, Door3;

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
        //DestroyWall();
        ResetLevel();
        DebugTeleporting();
        


        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
            Debug.Log("quit");
        }


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
        player.gameObject.GetComponent<FirstPersonController>().m_WalkSpeed = 8f;



    }

   

    public IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        animator.SetTrigger("Death-FadeIn");
        player.transform.position = currentCheckpoint.transform.position;
    }

/*
    private void DestroyWall()
    {
        if (player.gameObject.GetComponent<FirstPersonController>().punchCards == 1)
        {
            //Debug.Log("Wall is kill");
           Door1.gameObject.SetActive(false);
        }

        if (player.gameObject.GetComponent<FirstPersonController>().punchCards == 2)
        {
            //Debug.Log("Wall is kill");
            Door2.gameObject.SetActive(false);
        }

        if (player.gameObject.GetComponent<FirstPersonController>().punchCards == 3)
        {
            //Debug.Log("Wall is kill");
            Door3.gameObject.SetActive(false);
        }

    }
    */
    private void ResetLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.gameObject.transform.position = currentCheckpoint.transform.position;
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
        //look up switch cases
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

        if (Input.GetKeyDown("l"))
        {
            player.transform.position = checkpointL.transform.position;
        }
        if (Input.GetKeyDown("i"))
        {
            player.transform.position = checkpointI.transform.position;

        }
    }
}
