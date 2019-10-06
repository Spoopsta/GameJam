using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public Animation UIAnimation1;
    public Animation UIAnimation2;
    public Animation UIAnimation3;

    public GameObject StartCanvas;
    public GameObject UICanvas;


    private void Start()
    {
        StartCanvas.gameObject.SetActive(true);
        UICanvas.gameObject.SetActive(false);
        UIAnimation3.Play();
    }


    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartCanvas.gameObject.SetActive (false);

            UICanvas.gameObject.SetActive(true);

            UIAnimation1.Play();
            UIAnimation2.Play();
        }
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
}
