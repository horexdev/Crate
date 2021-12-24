using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    // СОХРАНЕНИЕ ИГРОКА
    public static void SavePlayer(Player_Control player)
    {
        var path = Application.persistentDataPath + "/PlayerDATA.crate";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    // ЗАГРУЗКА ИГРОКА
    public static PlayerData LoadPlayer()
    {
        var path = Application.persistentDataPath + "/PlayerDATA.crate";

        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogWarning(string.Format("No such a savefile in {0}", Application.persistentDataPath));
            FileStream stream = new FileStream(path, FileMode.Create);
            Player_Control player = new Player_Control();
            PlayerData data = new PlayerData(player);
            data.money = 0;
            data.highscore = 0;
            formatter.Serialize(stream, data);
            stream.Close();
            return data;
        }
    }
}
