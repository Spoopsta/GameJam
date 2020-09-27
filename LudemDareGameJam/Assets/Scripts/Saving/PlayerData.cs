using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

[System.Serializable]
public class PlayerData
{
    public int keyCards;
    public int Sheep;
    public float[] position;
    public int activeScene;

    public PlayerData(FirstPersonController player)
    {
        keyCards = player.punchCards;
        Sheep = player.sheepCollected;
        //activeScene = SceneManager.GetActiveScene().buildIndex;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
