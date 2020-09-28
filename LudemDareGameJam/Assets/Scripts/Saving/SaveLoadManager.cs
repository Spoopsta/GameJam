using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{

    public FirstPersonController player;
    public bool bLoadGame = false;
    // Start is called before the first frame update
    void Start()
    {

        //player = GameObject.FindObjectOfType<FirstPersonController>();

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
     
        PlayerData data = SaveSystem.LoadPlayer();

        
        player.GetComponent<FirstPersonController>().punchCards = data.keyCards;
        player.GetComponent<FirstPersonController>().sheepCollected = data.Sheep;
        //SceneManager.LoadScene(sceneBuildIndex: GetComponent<PlayerData>().activeScene);
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
    public void LoadPlayerFalse()
    {
        bLoadGame = false;
    }

    public void LoadPlayerInGame()
    {
        if (bLoadGame == true)
        {
            LoadPlayer();
        }
    }

    public void NewGameDeleteData()
    {
       File.Delete(Application.persistentDataPath + "/player.data");
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

}
