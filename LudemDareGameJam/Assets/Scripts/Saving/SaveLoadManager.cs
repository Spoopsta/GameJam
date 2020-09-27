using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{

    public FirstPersonController player;
    private bool bLoadGame;
    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player);
    }

    public void LoadPlayer()
    {
        //SceneManager.LoadScene(sceneBuildIndex: 1);
        PlayerData data = SaveSystem.LoadPlayer();

        player.GetComponent<FirstPersonController>().punchCards = data.keyCards;
        player.GetComponent<FirstPersonController>().sheepCollected = data.Sheep;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        player.transform.position = position;
       // DontDestroyOnLoad(player.transform);


    }

    public void LoadPlayerConfirm()
    {
        bLoadGame = true;
    }

}
