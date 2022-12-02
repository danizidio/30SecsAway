using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelManagerSave : MonoBehaviour
{
    public static LevelManagerSave instance;

    private void Awake()
    {
        instance = this;
    }

    public void SavingManager(GameObject player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "LevelManager" + player.name + ".data");
        SaveManager sm = new SaveManager();

        //salvar coisas
        sm.xp = GetComponent<LevelUpManager>().exp;
        sm.lvlPoints = GetComponent<LevelUpManager>().pointsToExpend;
        sm.PtsToNxtLvl = GetComponent<LevelUpManager>().nxtLvlExp;
        sm.currentLevel = GetComponent<LevelUpManager>().currentLevel;

        bf.Serialize(fs, sm);
        fs.Close();
    }

    public void LoadingManager(GameObject player)
    {
        if (File.Exists(Application.persistentDataPath + "LevelManager" + player.name + ".data"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "LevelManager" + player.name + ".data", FileMode.Open);
            SaveManager sm = (SaveManager)bf.Deserialize(fs);
            fs.Close();

            LevelUpManager lm = GetComponent<LevelUpManager>();

            //entregar os dados para as variaveis

            lm.exp = sm.xp;
            lm.pointsToExpend = sm.lvlPoints;
            lm.nxtLvlExp = sm.PtsToNxtLvl;
            lm.currentLevel = sm.currentLevel;

            print("exp " + lm.exp + "\n" + "points " + "\n" + "nextlvl = " + lm.nxtLvlExp);
        }

    }

    public void DeleteSaveManager(GameObject player)
    {
        if (File.Exists(Application.persistentDataPath + "LevelManager" + player.name + ".data"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "LevelManager" + player.name + ".data");
            SaveManager sm = (SaveManager)bf.Deserialize(fs);


            LevelUpManager lm = GetComponent<LevelUpManager>();

            //entregar os dados para as variaveis

            lm.exp = 0;
            lm.pointsToExpend = 0;
            lm.nxtLvlExp = 1000;

            sm.xp = lm.exp;
            sm.lvlPoints = lm.pointsToExpend;
            sm.PtsToNxtLvl = lm.nxtLvlExp;

            bf.Serialize(fs, sm);
            fs.Close();
        }
    }
}

[Serializable]
class SaveManager
{
    public long xp;
    public int lvlPoints;
    public float PtsToNxtLvl;
    public int currentLevel;
}
