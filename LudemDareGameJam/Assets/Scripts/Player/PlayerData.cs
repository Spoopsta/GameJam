using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[System.Serializable]
public class PlayerData
{
    public int keyCards;
    public int Sheep;
    public float[] position;
    private FirstPersonController player;

    public PlayerData(FirstPersonController player)
    {
        keyCards = player.punchCards;
        Sheep = player.sheepCollected;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
