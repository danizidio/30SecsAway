using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackClass;
using PlayerBase;
using UIClass;
public class Ninja : Player
{
    float _counter = 5;
    bool canAddShuriken;

    [SerializeField] GameObject kunaiCounter, _shadowNinja;
    [SerializeField] GameObject _dagger;

    [SerializeField] Transform _daggerSpot;

    public GameObject dagger
    {
        get
        {
            return _dagger;
        }

        set
        {
            _dagger = value;
        }
    }

    public Transform daggerSpot
    {
        get
        {
            return _daggerSpot;
        }

        set
        {
            _daggerSpot = value;
        }
    }

    [SerializeField] Transform shadowSpawn;

    [SerializeField] int _shurikenLimit;

    int _currentShuriken;
    public int shurikenLimit
    {
        get
        {
            return _shurikenLimit;
        }

        set
        {
            _shurikenLimit = value;
        }
    }

    public int currentShuriken
    {
        get
        {
            return _currentShuriken;
        }

        set
        {
            _currentShuriken = value;
        }
    }

    public GameObject shadowNinja { get { return _shadowNinja; } set { _shadowNinja = value; } }

    public float counter { get { return _counter; } set { _counter = value; } }

    void Start ()
    {
        canMove = true;

        currentMana = maxMana;
        currentHealth = maxHealth;

        currentShuriken = shurikenLimit;

        Instantiate(kunaiCounter, GameObject.Find("LifeBar(Clone)").transform);

        GetComponent<PlayerUIManagerNinja>().StartUIPoints();
    }

    void Update()
    {

        if (canMove)
        {
            DefaultMoving();

            Jumping();

            if (Input.GetKey(KeyCode.C))
            {
                animBehaviour.SetBool("attack", true);
            }
            else
            {
                animBehaviour.SetBool("attack", false);
            }

            if (Input.GetKeyDown(KeyCode.B) && currentMana >= _class.costShadow)
            {
                currentMana -= _class.costShadow;

                PlayerUIManager.instance.ManaUpdate(maxMana, currentMana);

                animBehaviour.Play("SuperAttackFNinjaAnim");
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (isOnGround)
                {
                    animBehaviour.Play("FemaleNinjaThrow");
                }
                else
                {
                    animBehaviour.Play("FemaleNinjaJumpThrow");
                }

                ThrowingDagger();
            }
        }

        if (currentMana < maxMana)
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


        GetComponent<PlayerUIManagerNinja>().CountingUpdate();

        if (isOnGround)
        {
            shadowSpawn.localPosition = new Vector3(2, 0, 0);
        }
        else
        {
            shadowSpawn.localPosition = new Vector3(2, 4, 0);
        }

        if (currentShuriken < shurikenLimit)
        {
            StartCoroutine(CountingShuriken());

            if (canAddShuriken)
            {
                counter -= Time.deltaTime;
                if (counter <= 0)
                {
                    AddShuriken();
                    counter = 5;
                }
            }
        }
        else
        {
            canAddShuriken = false;
        }
    }
    public void ShadowAttack()
    {
        if (isRight)
        {
            Instantiate(shadowNinja, new Vector3(shadowSpawn.position.x,
                shadowSpawn.position.y, shadowSpawn.position.z), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            Instantiate(shadowNinja, new Vector3(shadowSpawn.localPosition.x,
                shadowSpawn.localPosition.y, shadowSpawn.localPosition.z), Quaternion.Euler(0, -180, 0));
        }
    }

    public void ThrowingDagger()
    {
        if (currentShuriken > 0)
        {
            GameObject temp = Instantiate(dagger);
            Vector3 pos = daggerSpot.transform.position;

            temp.transform.position = new Vector3(pos.x, pos.y, pos.z);

            currentShuriken--;
            StartCoroutine(CountingShuriken());
        }
    }

    void AddShuriken()
    {
        currentShuriken++;
    }

    public IEnumerator CountingShuriken()
    {
        yield return new WaitForSeconds(5);

        canAddShuriken = true;

        yield return 0;
    }
}
