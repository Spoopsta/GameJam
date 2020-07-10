using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Analytics;
using UnityEngine.UI;

[Serializable]
public class GameManager : MonoBehaviour
{

    //when the player collides with a checkpoint, the old checkpoint gets replaced with the most recent. This allows the player to backtrack, but not lose any progress
    public GameObject currentCheckpoint;

    public GameObject blackOutSquare;

    //this is so we can move the player around by making them an object that the code can read and adjust their transform.position
    //public GameObject player;

    //attempted replacement of gameobject
    public FirstPersonController player;

    //creating a delay for respawning
    public float respawnDelay;

    public GameObject checkpoint1, checkpoint2, checkpoint3, checkpoint4, checkpoint5, checkpoint6, checkpoint7, checkpoint8, checkpoint9, checkpoint10, checkpointO, checkpointP,
        checkpointL, checkpointI;

    public bool playerDeath;

   


    //death fades
   // public Animator animator;

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
        Time.timeScale = 1f;
        playerDeath = false;



    }

    // Update is called once per frame
    void Update()
    {
        //DestroyWall();
        ResetLevel();
        DebugTeleporting();
        RespawnPlayer();
        
        //Debug.Log(blackOutSquare.GetComponent<Image>().color.a);



        if (playerDeath == true)
        {
            StartCoroutine(RespawnCoroutine());
        }
        if (playerDeath == false)
        {
            StartCoroutine(RespawnCoroutine(false));
        }



    }

    /// <summary>
    /// When the player collides with a bad projectile or falls to their death, this code is called and respawns the player on the current checkpoint
    /// </summary>
    public void RespawnPlayer()
    {
        //keep just in case
        //player.transform.position = currentCheckpoint.transform.position;

        //animator.SetTrigger("Death-FadeOut");
        if (playerDeath == true)
        {
            player.gameObject.GetComponent<FirstPersonController>().m_WalkSpeed = 8f;
        }



    }

    public void QUitGame()
    {
        Application.Quit();
        Debug.Log("Game QUit");
    }

  

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        GetComponent<FirstPersonController>().punchCards = data.keyCards;
        GetComponent<FirstPersonController>().sheepCollected = data.Sheep;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }


    /// <summary>
    /// handles player death fade. When player dies, the blackoutsquare gameobject alpha will begin to decrease. When it reaches 0, it will increase.
    /// </summary>
    /// <param name="fadeToBlack"></param>
    /// <param name="fadeSpeed"></param>
    /// <returns></returns>
    public IEnumerator RespawnCoroutine(bool fadeToBlack = true, float fadeSpeed = 1.5f)
    {
       yield return new WaitForSeconds(respawnDelay);
        
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime * 1);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                
                blackOutSquare.GetComponent<Image>().color = objectColor;
                playerDeath = false;
                player.transform.position = currentCheckpoint.transform.position;
                yield return null;
            }
        }

        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime * 1);
                Debug.Log("hi");
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
           
        
        }

        /*
        if (blackOutSquare.GetComponent<Image>().color.a == 1)
        {
            playerDeath = false;
        }
        */

     


/*        
        animator.SetTrigger("Death-FadeOut");
       
*/
    }

    //sends the player back to the most recent checkpoint.
    private void ResetLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.gameObject.transform.position = currentCheckpoint.transform.position;
        }
    }

    //scene transition stuff.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("WIN");
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
    }





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
