using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{
    public static void Salvar(Pause pause)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.bagulho";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(pause);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData Carregar()
    {
        string path = Application.persistentDataPath + "/player.bagulho";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Reiniciar();
            Debug.LogError("NAO ENCONTROU O ARQUIVO " + path);
            return null;
        }
    }

    public static void Reiniciar()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.bagulho";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData("Seção 1 Level 1");
        formatter.Serialize(stream, data);
        stream.Close();
    }
}
