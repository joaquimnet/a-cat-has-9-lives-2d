using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static GameSaveData Save(GameSaveData data)
    {
        BinaryFormatter fmt = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.neko";
        FileStream stream = new FileStream(path, FileMode.Create);

        fmt.Serialize(stream, data);
        stream.Close();
        return data;
    }

    public static GameSaveData Load()
    {
        string path = Application.persistentDataPath + "/player.neko";
        if (File.Exists(path))
        {
            BinaryFormatter fmt = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            var gameData = (GameSaveData)fmt.Deserialize(stream);
            stream.Close();

            return gameData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
        }
        return null;
    }
}
