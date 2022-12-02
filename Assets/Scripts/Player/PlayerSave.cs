using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using PlayerBase;

public class PlayerSave : MonoBehaviour
{
    private void Awake()
    {
        LoadingPlayer();
    }

    public void SavingPlayer()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "Player" + this.gameObject.name + ".data");
        
        SavePlayer sp = new SavePlayer();

        //salvar coisas

        sp.atk = GetComponent<Player>().damage;
        sp.def = GetComponent<Player>().playerArmor;
        sp.fHealth = GetComponent<Player>().maxHealth;
        sp.fMana = GetComponent<Player>().maxMana;
        sp.mRegen = GetComponent<Player>().manaRegen;
        sp.cChance = GetComponent<Player>().criticalChance;
        sp.lvl = GetComponent<Player>().playerLevel;

        bf.Serialize(fs, sp);
        fs.Close();

    }

    public void LoadingPlayer()
    {
        if(File.Exists(Application.persistentDataPath + "Player" + this.gameObject.name + ".data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "Player" + this.gameObject.name + ".data", FileMode.Open);
            SavePlayer sp = (SavePlayer) bf.Deserialize(fs);
            fs.Close();

            //entregar os dados para as variaveis
            GetComponent<Player>().damage = (int) sp.atk;
            GetComponent<Player>().playerArmor = sp.def;
            GetComponent<Player>().maxHealth = sp.fHealth;
            GetComponent<Player>().maxMana = sp.fMana;
            GetComponent<Player>().manaRegen = sp.mRegen;
            GetComponent<Player>().criticalChance = sp.cChance;
            GetComponent<Player>().playerLevel = sp.lvl;
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "Player" + this.gameObject.name + ".data");
            SavePlayer sp = new SavePlayer();


            sp.atk = GetComponent<Player>().classAtributes.damage;
            sp.def = GetComponent<Player>().classAtributes.playerArmor;
            sp.fHealth = GetComponent<Player>().classAtributes.maxHealth;
            sp.fMana = GetComponent<Player>().classAtributes.maxMana;
            sp.mRegen = GetComponent<Player>().classAtributes.manaRegen;
            sp.cChance = GetComponent<Player>().classAtributes.criticalChance;
            sp.lvl = 0;

            GetComponent<Player>().damage = GetComponent<Player>().classAtributes.damage;
            GetComponent<Player>().playerArmor = GetComponent<Player>().classAtributes.playerArmor;
            GetComponent<Player>().maxHealth = GetComponent<Player>().classAtributes.maxHealth;
            GetComponent<Player>().maxMana = GetComponent<Player>().classAtributes.maxMana;
            GetComponent<Player>().manaRegen = GetComponent<Player>().classAtributes.manaRegen;
            GetComponent<Player>().criticalChance = GetComponent<Player>().classAtributes.criticalChance;
            GetComponent<Player>().playerLevel = 0;
            bf.Serialize(fs, sp);
            fs.Close();
        }
    }

    public void LosingCharacter()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "Player" + this.gameObject.name + ".data");
        
        SavePlayer sp = new SavePlayer();

        Atributes a = GetComponent<Atributes>();
        //salvar coisas

        sp.atk = GetComponent<Player>().classAtributes.damage;
        sp.def = GetComponent<Player>().classAtributes.playerArmor;
        sp.fHealth = GetComponent<Player>().classAtributes.maxHealth;
        sp.fMana = GetComponent<Player>().classAtributes.maxMana;
        sp.mRegen = GetComponent<Player>().classAtributes.manaRegen;
        sp.cChance = GetComponent<Player>().classAtributes.criticalChance;
        sp.lvl = 0;

        bf.Serialize(fs, sp);
        fs.Close();
    }
}

[Serializable] class SavePlayer
{
    public float fHealth;
    public float fMana;
    public float mRegen;
    public float cChance;
    public float atk;
    public int def;
    public int lvl;
}
