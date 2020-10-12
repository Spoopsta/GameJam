using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class SavePlayer : MonoBehaviour
{
    public GameObject saveManager;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //saveManager.GetComponent<SaveLoadManager>().LoadPlayer();
        
        //sets the load game bool to true
        saveManager.GetComponent<SaveLoadManager>().LoadPlayerConfirm();
    }
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if the load bool is true, load player when they are in game
        saveManager.GetComponent<SaveLoadManager>().LoadPlayerInGame();

        //set the load bool to false so that the player isnt in an infinite loop.
        saveManager.GetComponent<SaveLoadManager>().LoadPlayerFalse();
    }
}
