using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SceneSaving : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI hintText;

    public string[] sHints = new string[] { "hi", "hy", "he", "hii" };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneSave()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        Debug.Log("Scene Saved");
    }

    public void LoadScene()
    {
       // SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));

        StartCoroutine(LoadAsynchronously(PlayerPrefs.GetInt("Level")));
        string name = sHints[Random.Range(0, sHints.Length - 1)];

        hintText.text = name;

    }


    public void LoadLevel(int sceneIndex)
    {

        StartCoroutine(LoadAsynchronously(sceneIndex));

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
