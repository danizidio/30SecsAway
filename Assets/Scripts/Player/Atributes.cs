using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atributes : MonoBehaviour
{

    public static Atributes instance;

    [SerializeField] int _initialArmor, _initialDamage, _initialLvl;
    [SerializeField] float _initialFullHealth, _initialFullMana, _initialManaRegen, _initialCritChance;

    [Tooltip("Desesa do Player")]
    [SerializeField] int _playerArmor;

    [Tooltip("Quantidade máxima de pontos de vida do Player")]
    [SerializeField] float _fullHealth;
    
    float _currentHealth;

    [Tooltip("Quantidade máxima de pontos de habilidade do Player")]
    [SerializeField] float _fullMana;
    
    float _currentMana;

    [Tooltip("Quantidade máxima de pontos de habilidade do Player")]
    [SerializeField] float _manaRegen;

    [Tooltip("A força do ataque do Player")]
    [SerializeField] int _damage;

    [Tooltip("Chance de Dano Critico")]
    [SerializeField] float _criticalChance;

    bool canRegenMana;

    [SerializeField] int _playerLevel;

    float _count = 5;

    #region - Properties -

    public float fullHealth
    {
        get { return _fullHealth; }
        set { _fullHealth = value; }
    }

    public int playerArmor
    {
        get { return _playerArmor; }
        set {_playerArmor = value; }
    }

    public int damage
    {
        get { return _damage; }
        set { _damage = value;}
    }

    public float fullMana
    {
        get { return _fullMana;}
        set { _fullMana = value;}
    }

    public float criticalChance
    {
        get { return _criticalChance;}
        set { _criticalChance = value;}
    }

    public float count
    {
        get{return _count;}
    }

    public float currentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public float currentMana
    {
        get { return _currentMana;}
        set { _currentMana = value;}
    }

    public float manaRegen
    {
        get { return _manaRegen;}
        set { _manaRegen = value;}
    }

    public int playerLevel
    {
        get
        {
            return _playerLevel;
        }

        set
        {
            _playerLevel = value;
        }
    }

    public int initialArmor
    {
        get
        {
            return _initialArmor;
        }

        set
        {
            _initialArmor = value;
        }
    }

    public int initialDamage
    {
        get
        {
            return _initialDamage;
        }

        set
        {
            _initialDamage = value;
        }
    }

    public int initialLvl
    {
        get
        {
            return _initialLvl;
        }

        set
        {
            _initialLvl = value;
        }
    }

    public float initialFullHealth
    {
        get
        {
            return _initialFullHealth;
        }

        set
        {
            _initialFullHealth = value;
        }
    }

    public float initialFullMana
    {
        get
        {
            return _initialFullMana;
        }

        set
        {
            _initialFullMana = value;
        }
    }

    public float initialManaRegen
    {
        get
        {
            return _initialManaRegen;
        }

        set
        {
            _initialManaRegen = value;
        }
    }

    public float initialCritChance
    {
        get
        {
            return _initialCritChance;
        }

        set
        {
            _initialCritChance = value;
        }
    }

    #endregion

    void Awake()
    {
        instance = this;
        GetComponent<PlayerSave>().LoadingPlayer();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (currentMana < fullMana)
        {
            StartCoroutine(CoolDownToRegenMana());
            if (canRegenMana)
            {
                ManaRegen(manaRegen);
            }
        }
        else
        {
            canRegenMana = false;
        }
    }


    void ManaRegen(float regenRate)
    {
        regenRate = (regenRate / 100);
        if (currentMana < fullMana)
        {
            currentMana += regenRate;
        }
    }


    public IEnumerator CoolDownToRegenMana()
    {
        yield return new WaitForSeconds(10);

        canRegenMana = true;

        yield return 0;
    }
}

