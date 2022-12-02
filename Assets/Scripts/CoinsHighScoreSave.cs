using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


public class CoinsHighScoreSave : MonoBehaviour
{
    public static CoinsHighScoreSave instance;

    [SerializeField] int _coins = 0;
    GameObject[] Datas;
    [SerializeField] GameObject[] characters;

    [SerializeField] GameObject _playerSelect;

    public int coins
    {
        get
        {
            return _coins;
        }

        set
        {
            _coins = value;
        }
    }

    public GameObject playerSelect
    {
        get
        {
            return _playerSelect;
        }

        set
        {
            _playerSelect = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }       
    }

    private void Start()
    {
        LoadingGame();
    }

    public void SavingGame(int coinsToAdd)
    {
        if (!File.Exists(Application.persistentDataPath + "CoinsAndHighScore.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "CoinsAndHighScore.data");
            GameNumbersSaving gns = new GameNumbersSaving();

            //salvar coisas

            gns.coinstoSave = coinsToAdd;

            bf.Serialize(fs, gns);
            fs.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenWrite(Application.persistentDataPath + "CoinsAndHighScore.data");
            GameNumbersSaving gns = new GameNumbersSaving();

            gns.coinstoSave = coins + coinsToAdd;

            bf.Serialize(fs, gns);
            fs.Close();
        }
    }

    public void LoadingGame()
    {
        if (File.Exists(Application.persistentDataPath + "CoinsAndHighScore.data"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "CoinsAndHighScore.data", FileMode.OpenOrCreate);
            GameNumbersSaving gns = (GameNumbersSaving)bf.Deserialize(fs);
            fs.Close();

            //entregar os dados para as variaveis

            coins = gns.coinstoSave;
            playerSelect = characters[gns.characterToplay];
        }
        else
        {
            playerSelect = characters[0];
        }
    }

    public void SelectCharacter(int characterNumber)
    {
        playerSelect = characters[characterNumber - 1];

        if (!File.Exists(Application.persistentDataPath + "CoinsAndHighScore.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "CoinsAndHighScore.data");
            GameNumbersSaving gns = new GameNumbersSaving();

            //salvar coisas

            gns.characterToplay = characterNumber -1;
            print(gns.characterToplay);

            bf.Serialize(fs, gns);
            fs.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenWrite(Application.persistentDataPath + "CoinsAndHighScore.data");
            GameNumbersSaving gns = new GameNumbersSaving();

            gns.characterToplay = characterNumber -1;
            print("Save  " + gns.characterToplay);

            bf.Serialize(fs, gns);
            fs.Close();
        }
    }
}
[Serializable]
class GameNumbersSaving
{
    public int coinstoSave;
    public int characterToplay;
}
