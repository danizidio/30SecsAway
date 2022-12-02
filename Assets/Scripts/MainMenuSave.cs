using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenuSave : MonoBehaviour
{

    public void LoadingMenu()
    {
        if (File.Exists(Application.persistentDataPath + "Game.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "Game.data", FileMode.Open);

            MenuSave ms = (MenuSave)bf.Deserialize(fs);

            fs.Close();

            //entregar os dados para as variaveis

            GetComponent<MainMenu>().qntCoins = ms.coins;
            print(ms.coins);
        }
    }
}

[Serializable]
class MenuSave
{
    public int lvl;
    public int coins;
}