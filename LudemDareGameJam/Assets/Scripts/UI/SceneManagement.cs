using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private Animation UIAnimation1;
    private Animation UIAnimation2;
    private Animation UIAnimation3;

    public GameObject StartCanvas;
    public GameObject UICanvas;


    private void Start()
    {
        StartCanvas.gameObject.SetActive(true);
        UICanvas.gameObject.SetActive(false);
        //UIAnimation3.Play();
       
    }


    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            /*Cursor.visible = true;
            StartCanvas.gameObject.SetActive (false);

            UICanvas.gameObject.SetActive(true);

            UIAnimation1.Play();
            UIAnimation2.Play();
            */
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

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
