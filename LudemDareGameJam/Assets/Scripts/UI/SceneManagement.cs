using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class SceneManagement : MonoBehaviour
{

   // public GameObject StartCanvas;
    public GameObject UICanvas;
    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text progressText;
    public AudioSource uiSound;
    public FirstPersonController player;
    public bool bLoadGame;
    //public GameObject player;


    private void Start()
    {
      //  StartCanvas.gameObject.SetActive(true);
        UICanvas.gameObject.SetActive(true);
        loadingScreen.gameObject.SetActive(false);
        //UIAnimation3.Play();
 
    

    }


    private void Update()
    {
        /*
        if (Input.GetKeyDown("space"))
        {
            Cursor.visible = true;
            StartCanvas.gameObject.SetActive (false);

            UICanvas.gameObject.SetActive(true);

            
           // SceneManager.LoadScene(sceneBuildIndex: 1);
        }
        */

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("game quit");
            Application.Quit();
        }

        if (Input.GetKeyDown("y"))
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        if (Input.GetKeyDown("e"))
        {
            Application.Quit();
        }
    }

    public void uiSoundPlay()
    {
        uiSound.Play();
    }

    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
    }

    public void QuitGame()
    {
        Debug.Log("game has been quit");
        Application.Quit();

      
    }

    public void LoadLevel(int sceneIndex)
    {

       StartCoroutine(LoadAsynchronously(sceneIndex));

    }

   

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

   
}
