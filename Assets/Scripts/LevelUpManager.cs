using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerBase;

public class LevelUpManager : MonoBehaviour
{

    public static LevelUpManager instance;

    [SerializeField] string playerName;
    [SerializeField] int _currentLevel, maxLevel, _pointsToExpend;
    [SerializeField] long _exp;
    [SerializeField] float _nxtLvlExp;

    [SerializeField] bool _lvlcap, _canSave;

    GameObject playerOne;
    Text a1, a2, a3, a4, a5, _a6, a7, a8, a9, a10, a11;
    
    #region - Properties -
    public int currentLevel
    {
        get
        {
            return _currentLevel;
        }

        set
        {
            _currentLevel = value;
        }
    }

    public long exp
    {
        get
        {
            return _exp;
        }

        set
        {
            _exp = value;
        }
    }

    public bool lvlcap
    {
        get
        {
            return _lvlcap;
        }
        set
        {
            _lvlcap = value;
        }
    }

    public Text a6
    {
        get
        {
            return _a6;
        }

        set
        {
            _a6 = value;
        }
    }

    public int pointsToExpend
    {
        get
        {
            return _pointsToExpend;
        }

        set
        {
            _pointsToExpend = value;
        }
    }

    public float nxtLvlExp
    {
        get
        {
            return _nxtLvlExp;
        }

        set
        {
            _nxtLvlExp = value;
        }
    }

    public bool canSave
    {
        get
        {
            return _canSave;
        }

        set
        {
            _canSave = value;
        }
    }
    #endregion

    private void Awake()
    {
        instance = this;
    }
    void Start ()
    {
        playerOne = GameObject.FindGameObjectWithTag("Player");
        playerName = GameObject.FindGameObjectWithTag("Player").name;


        //GetComponent<LevelManagerSave>().DeleteSaveManager(playerOne);
        GetComponent<LevelManagerSave>().LoadingManager(playerOne);

        //playerOne.GetComponent<Player>().playerLevel = currentLevel;
        
        a1 = GameObject.Find("LVL").GetComponent<Text>();
        a2 = GameObject.Find("EXP").GetComponent<Text>();
        a3 = GameObject.Find("ATK").GetComponent<Text>();
        a4 = GameObject.Find("HP").GetComponent<Text>();
        a5 = GameObject.Find("DEF").GetComponent<Text>();
        //a6 = GameObject.Find("BattleLog").GetComponent<Text>();
        a7 = GameObject.Find("LevelAvailable").GetComponent<Text>();
        a8 = GameObject.Find("Mana").GetComponent<Text>();
        a9 = GameObject.Find("CriticalChance").GetComponent<Text>();
        //a10 = GameObject.Find("Help").GetComponent<Text>();
        a11 = GameObject.Find("ManaRegen").GetComponent<Text>();
    }

    void Update ()
    {
        LevelingUp();

        if (a1 != null)
            a1.text = "Level = " + currentLevel.ToString();
        if (a2 != null)
            a2.text = "EXP = " + exp.ToString();
        if (a3 != null)
            a3.text = "ATK = " + playerOne.GetComponent<Player>().damage.ToString();
        if (a4 != null)
            a4.text = "HPMAX = " + playerOne.GetComponent<Player>().maxHealth.ToString();
        if (a5 != null)
            a5.text = "DEF = " + playerOne.GetComponent<Player>().playerArmor.ToString();
        if (a8 != null)
            a8.text = "Mana = " + playerOne.GetComponent<Player>().maxMana.ToString();
        if (a9 != null)
            a9.text = "CriticalChance = " + playerOne.GetComponent<Player>().criticalChance.ToString();
        if (a7 != null)
            a7.text = "Points Available = " + pointsToExpend;
        if (a11 != null)
            a11.text = "ManaRegen = " + playerOne.GetComponent<Player>().manaRegen;

        //a10.text = "AJUDA\n Z = Projétil \n V = Dash* \n Espaço = Pulo \n Espaço duas vezes = Pulo Duplo* \n B = Ataque especial* \n\n * Gasta Mana";

        if(pointsToExpend > 0)
        {
            Text temp = GameObject.Find("PointsAvailable").GetComponent<Text>();
            temp.GetComponentInChildren<Button>().enabled = true;
            temp.enabled = true;
        }
        else
        {
            Text temp = GameObject.Find("PointsAvailable").GetComponent<Text>();
            temp.GetComponentInChildren<Button>().enabled = false;
            temp.enabled = false;
        }
    }

    void LevelingUp()
    {
        if(exp > nxtLvlExp)
        {
            playerOne.GetComponent<Player>().playerLevel++;
            currentLevel = playerOne.GetComponent<Player>().playerLevel;
            nxtLvlExp += nxtLvlExp * 1.5f;
            pointsToExpend++;
            exp = 0;
            playerOne.GetComponent<Player>().currentHealth = playerOne.GetComponent<Player>().maxHealth;
            playerOne.GetComponent<Player>().currentMana = playerOne.GetComponent<Player>().maxMana;

            if (currentLevel >= maxLevel)
            {
                currentLevel = maxLevel;
                exp = (long)nxtLvlExp;

                lvlcap = true;

            }
            else
            {
                lvlcap = false;
            }

            AtributesBonus();
            
        }
    }

    public void EarnExp(float xp)
    {
        if (lvlcap == false)
        {
            exp += (long)xp;
        }
        else
        {
            exp = (long) nxtLvlExp;
        }
    }
    public void AtributesBonus()
    {
        if (currentLevel > 1)
        {
            float bonus = currentLevel * .2f;
            playerOne.GetComponent<Player>().playerArmor += (int)bonus + (int).2f;
            playerOne.GetComponent<Player>().maxHealth += (int)bonus;
            playerOne.GetComponent<Player>().damage += (int)bonus + (int).2f;
            playerOne.GetComponent<Player>().maxMana += (int)bonus;
        }
    }

    public void UpgradeStrentgh()
    {
        if (pointsToExpend >= 1)
        {
            playerOne.GetComponent<Player>().damage++;
            pointsToExpend--;
        }
    }
    public void UpgradeLife()
    {
        if (pointsToExpend >= 1)
        {
            playerOne.GetComponent<Player>().maxHealth += 10;
            pointsToExpend--;
        }
    }
    public void UpgradeMana()
    {
        if (pointsToExpend >= 1)
        {
            playerOne.GetComponent<Player>().maxMana += 1;
            pointsToExpend--;
        }
    }
    public void UpgradeDef()
    {
        if (pointsToExpend >= 1)
        {
            playerOne.GetComponent<Player>().playerArmor++;
            pointsToExpend--;
        }
    }

    public void UpgradeCritical()
    {
        if (pointsToExpend >= 1)
        {
            playerOne.GetComponent<Player>().criticalChance++;
            pointsToExpend--;
        }
    }

    public void UpgradeManaRegen()
    {
        if (pointsToExpend >= 1)
        {
            playerOne.GetComponent<Player>().manaRegen++;
            pointsToExpend--;
        }
    }
}
