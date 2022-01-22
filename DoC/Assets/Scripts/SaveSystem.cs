using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void saveGameData(GameManager gm)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameData.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(gm);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData loadGameData()
    {
        string path = Application.persistentDataPath + "/gameData.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("File not found in" + path);
            return null;
        }
    }
}
