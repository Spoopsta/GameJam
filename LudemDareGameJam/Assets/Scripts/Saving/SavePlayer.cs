using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class SavePlayer : MonoBehaviour
{
    public GameObject saveManager;
    private FirstPersonController player;
    // Start is called before the first frame update
    void Start()
    {
        //saveManager.GetComponent<SaveLoadManager>().LoadPlayer();
        saveManager.GetComponent<SaveLoadManager>().LoadPlayerConfirm();
        
    }
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        saveManager.GetComponent<SaveLoadManager>().LoadPlayerInGame();
        saveManager.GetComponent<SaveLoadManager>().LoadPlayerFalse();
    }
}
