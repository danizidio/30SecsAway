using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Use this for saving, loading or delete preferences

public class ManagementPrefs : MonoBehaviour
{
    public static ManagementPrefs instance;

    #region - Atributes - 

    [SerializeField] int  _coins, _coinsToAdd;

    #endregion

    #region - Properties -


    public int coins
    {
        get { return _coins; }
        set { _coins = value; }
    }

    public int coinsToAdd
    {
        get { return _coinsToAdd; }
        set { _coinsToAdd = value; }
    }

  

    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void LoadingCoins()
    {
        //PLAYERPREFS COINS
        if (PlayerPrefs.HasKey("COINS"))
        {
            coinsToAdd = PlayerPrefs.GetInt("COINS");
            coins += coinsToAdd;
        }
        else
        {
            PlayerPrefs.SetInt("COINS", 0);
            coins = 0;
        }
    }

    public void SavingCoins(int add)
    {
        if (PlayerPrefs.HasKey("COINS"))
        {

            coinsToAdd = PlayerPrefs.GetInt("COINS");
            coins = coinsToAdd + add;
            PlayerPrefs.SetInt("COINS", coins);
        }
        else
        {
            PlayerPrefs.SetInt("COINS", 0);
            coins = 0;
        }
    }

    public void LosingLevels()
    {
        PlayerPrefs.DeleteAll();
    }
}
