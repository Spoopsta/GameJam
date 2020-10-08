using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{

    public FirstPersonController player;
    public bool bLoadGame = false;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI hintText;

    public string[] sHints = new string[] { };
   // public GameObject sceneLoader;
    // Start is called before the first frame update
    void Start()
    {

        //player = GameObject.FindObjectOfType<FirstPersonController>();
      //  bLoadGame = false;

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
        player.GetComponent<FirstPersonController>().iYellowKey = data.YellowKeyCard;
        player.GetComponent<FirstPersonController>().iBlueKey = data.BlueKeyCard;
        player.GetComponent<FirstPersonController>().iRedKey = data.RedKeyCard;
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

    public void NewGameDeleteData(int sceneIndex)
    {
       File.Delete(Application.persistentDataPath + "/player.data");
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }



    public void LoadLevel(int sceneIndex)
    {

        StartCoroutine(LoadAsynchronously(sceneIndex));
        string name = sHints[Random.Range(0, sHints.Length - 1)];

        hintText.text = name;

    }



    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        string name = sHints[Random.Range(0, sHints.Length - 1)];

        hintText.text = name;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
           // progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

}
