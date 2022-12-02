using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AttackClass;



public class HealthPointsEnemy : MonoBehaviour, IShowDamage
{
    public static HealthPointsEnemy instance;

    [SerializeField] private float _currentHealth;

    [SerializeField] private float _fullHealth;

    [SerializeField] private GameObject _lifeBar;

    [SerializeField] private Text _enemyName;

    [SerializeField] private GameObject _dmgTxt, _pivot;

    bool _isHit;

    bool _isBurnning;

    float a = 0, b = 0, c= 0;

    #region - properties -
    public float currentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public float fullHealth
    {
        get { return _fullHealth; }
        set { _fullHealth = value; }
    }

    public GameObject lifeBar
    {
        get { return _lifeBar; }
        set { _lifeBar = value; }
    }

    public Text enemyName
    {
        get
        {
            return _enemyName;
        }

        set
        {
            _enemyName = value;
        }
    }

    public GameObject dmgTxt
    {
        get
        {
            return _dmgTxt;
        }

        set
        {
            _dmgTxt = value;
        }
    }

    public GameObject pivot
    {
        get
        {
            return _pivot;
        }

        set
        {
            _pivot = value;
        }
    }

    public bool isHit
    {
        get
        {
            return _isHit;
        }

        set
        {
            _isHit = value;
        }
    }

    public bool isBurnning
    {
        get
        {
            return _isBurnning;
        }

        set
        {
            _isBurnning = value;
        }
    }

    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void HealthStart()
    {
        currentHealth = fullHealth;

        lifeBar.GetComponentInChildren<Image>();
    }

    public void HealthUpdate()
    {
        lifeBar.GetComponentInChildren<Image>().fillAmount = currentHealth / fullHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GetComponent<EnemyBase>().dead = true;
        }
        else
        {
            GetComponent<EnemyBase>().dead = false;
        }

        if (currentHealth >= fullHealth)
        {
            currentHealth = fullHealth;
        }

        if (isHit && currentHealth > 0)
        {
            //GameObject temp = GetComponent<EnemyBase>().lifeUI;

            lifeBar.GetComponent<Animator>().SetBool("show", true);

            Timer();
        }
    }

    void Timer()
    {
        GameObject temp = GetComponent<EnemyBase>().lifeUI;
        a += Time.deltaTime;
        if (a >= 3)
        {
            temp.GetComponent<Animator>().SetBool("show", false);
            AttackBase.instance.isHit = false;
        }
        if(temp.GetComponent<Animator>().GetBool("show") == false)
        {
            a = 0;
        }
    }

    public void Burning()
    {
        if (isBurnning)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            //InvokeRepeating("OnFire", 1, 1);
            //OnFire();

            b += Time.deltaTime;
            c += Time.deltaTime;

            if (c >= 1)
            {
                OnFire();
                c = 0;
            }

            if (b >= 5)
            {
                isBurnning = false;
                b = 0;
            }
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    public void GetKilled()
    {
        GameBehaviour.instance.enemiesKilled +=1;
    }

    void OnFire()
    {
        currentHealth -= 1;
        ShowText("BURN!!\n", Color.red, 1);
    }

    public void ShowText(string txt, Color txtColor, float damage)
    {
        dmgTxt.GetComponentInChildren<Text>().color = txtColor;
        dmgTxt.GetComponentInChildren<Text>().text = txt + damage;

        Instantiate(dmgTxt, new Vector3(pivot.transform.position.x,
                                        pivot.transform.position.y,
                                        pivot.transform.position.z),
                                        Quaternion.identity,
                                        pivot.transform);
    }
}
