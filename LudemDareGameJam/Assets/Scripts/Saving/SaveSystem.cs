using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public static class SaveSystem 
{
    public static void SavePlayer(FirstPersonController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        Debug.Log(data.Sheep);
        formatter.Serialize(stream, data);
        Debug.Log(path);
        stream.Close();      
    }


    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            

            FileStream stream = new FileStream(path, FileMode.Open);
            stream.Position = 0;

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found in" + path);
            return null;
        }
    }
}
