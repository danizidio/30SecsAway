using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AttackClass;

public class EnemyBase : MonoBehaviour, IEnemyCombat
{

    public static EnemyBase instance;

    #region - Atributes - 

    public string thatName;

    [SerializeField] private string[] _enemyNames;

    [SerializeField] private GameObject[] _healthPotion;

    private Animator _animBehaviour;

    [SerializeField] private GameObject _animUI, _lifeUI, _coin;

    private Transform _lifebar;

    [SerializeField] private GameObject thisEnemy, _player;

    [SerializeField] private Transform _groundDetector, _wallDetector, _enemyView, _potSpawn;

    //[SerializeField] private TextMesh _damageText;

    [SerializeField] private float _speed, _expGiven;

    [SerializeField] private int _armor, _enemyDamage;

    private float _chanceToGivePotion;
    private float _chanceToGiveCoin;
    private float _contadorMorte;
    private float _contador = 3;
    private float _contador2 = 3;
    private int _potcap = 0;

    [SerializeField] private bool _dead;
    private bool _touchGround;
    private bool _touchWall;
    private bool _obstacle;
    [SerializeField] private bool _lookingHero;

    #endregion

    #region - Properties -

    public Animator animBehaviour
    {
        get
        {
            return _animBehaviour;
        }

        set
        {
            _animBehaviour = value;
        }
    }

    public GameObject[] healthPotion
    {
        get
        {
            return _healthPotion;
        }

        set
        {
            _healthPotion = value;
        }
    }

    public GameObject player
    {
        get
        {
            return _player;
        }

        set
        {
            _player = value;
        }
    }

    public Transform lifebar
    {
        get
        {
            return _lifebar;
        }

        set
        {
            _lifebar = value;
        }
    }

    public Transform potSpawn
    {
        get
        {
            return _potSpawn;
        }

        set
        {
            _potSpawn = value;
        }
    }

    public Transform enemyView
    {
        get
        {
            return _enemyView;
        }

        set
        {
            _enemyView = value;
        }
    }

    public Transform wallDetector
    {
        get
        {
            return _wallDetector;
        }

        set
        {
            _wallDetector = value;
        }
    }

    public Transform groundDetector
    {
        get
        {
            return _groundDetector;
        }

        set
        {
            _groundDetector = value;
        }
    }

    //public TextMesh damageText
    //{
    //    get { return _damageText; }
    //    set { _damageText = value; }
    //}

    public float speed
    {
        get
        {
            return _speed;
        }

        set
        {
            _speed = value;
        }
    }

    public float chanceToGivePotion
    {
        get
        {
            return _chanceToGivePotion;
        }

        set
        {
            _chanceToGivePotion = value;
        }
    }

    public float chanceToGiveCoin
    {
        get
        {
            return _chanceToGiveCoin;
        }

        set
        {
            _chanceToGiveCoin = value;
        }
    }

    public float contadorMorte
    {
        get
        {
            return _contadorMorte;
        }

        set
        {
            _contadorMorte = value;
        }
    }

    public float contador2
    {
        get
        {
            return _contador2;
        }

        set
        {
            _contador2 = value;
        }
    }

    public float contador
    {
        get
        {
            return _contador;
        }

        set
        {
            _contador = value;
        }
    }

    public bool dead
    {
        get
        {
            return _dead;
        }

        set
        {
            _dead = value;
        }
    }

    public bool touchGround
    {
        get
        {
            return _touchGround;
        }

        set
        {
            _touchGround = value;
        }
    }

    public bool touchWall
    {
        get
        {
            return _touchWall;
        }

        set
        {
            _touchWall = value;
        }
    }

    public bool lookingHero
    {
        get
        {
            return _lookingHero;
        }

        set
        {
            _lookingHero = value;
        }
    }

    public int armor
    {
        get
        {
            return _armor;
        }

        set
        {
            _armor = value;
        }
    }

    public int enemyDamage
    {
        get
        {
            return _enemyDamage;
        }

        set
        {
            _enemyDamage = value;
        }
    }

    public string[] enemyNames
    {
        get
        {
            return _enemyNames;
        }

        set
        {
            _enemyNames = value;
        }
    }

    public GameObject animUI
    {
        get
        {
            return _animUI;
        }

        set
        {
            _animUI = value;
        }
    }

    public GameObject lifeUI
    {
        get
        {
            return _lifeUI;
        }

        set
        {
            _lifeUI = value;
        }
    }

    public GameObject coin
    {
        get
        {
            return _coin;
        }

        set
        {
            _coin = value;
        }
    }

    public float Speed
    {
        get
        {
            return _speed;
        }

        set
        {
            _speed = value;
        }
    }

    public float expGiven
    {
        get
        {
            return _expGiven;
        }

        set
        {
            _expGiven = value;
        }
    }

    public int potcap
    {
        get
        {
            return _potcap;
        }

        set
        {
            _potcap = value;
        }
    }

    public bool obstacle
    {
        get
        {
            return _obstacle;
        }

        set
        {
            _obstacle = value;
        }
    }

    #endregion

    private void Awake()
    {
        instance = this;
    }
    public void SearchingNames()
    {
        foreach (string enemyName in enemyNames)
        {

        }
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dead = false;
        potcap = 0;
    }

    public void Idle()
    {
        animBehaviour = GetComponent<Animator>();
        animBehaviour.SetBool("Idle", true);
        animBehaviour.SetBool("walking", false);
        animBehaviour.SetBool("attacking", false);
    }

    public void Attack()
    {
        animBehaviour = GetComponent<Animator>();
        animBehaviour.SetBool("Idle", false);
        animBehaviour.SetBool("attacking", true);
        animBehaviour.SetBool("walking", false);

    }
    public void Run()
    {
        animBehaviour = GetComponent<Animator>();
        animBehaviour.SetBool("Idle", false);
        animBehaviour.SetBool("walking", false);
        animBehaviour.SetBool("attacking", false);
        animBehaviour.SetBool("running", true);
    }
    public void Movimento()
    {
        animBehaviour = GetComponent<Animator>();
        animBehaviour.SetBool("walking", true);
        animBehaviour.SetBool("Idle", false);
        animBehaviour.SetBool("attacking", false);
    }


    public void Checking()
    {
        if (Physics2D.Linecast(transform.position, groundDetector.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            touchGround = true;
        }
        else
        {
            touchGround = false;
        }

        if (Physics2D.Linecast(transform.position, wallDetector.position, 1 << LayerMask.NameToLayer("Wall")))
        {
            touchWall = true;
        }
        else
        {
            touchWall = false;
        }

        if (Physics2D.Linecast(transform.position, enemyView.position, 1 << LayerMask.NameToLayer("Player")))
        {
            lookingHero = true;
        }
        else
        {
            lookingHero = false;
        }
    }


    public void DeadOrAlive()
    {
        animBehaviour = GetComponent<Animator>();

        if (dead == true)
        {
            chanceToGivePotion = Random.value;
            chanceToGiveCoin = Random.value;
            animBehaviour.SetBool("dying", true);

            GetComponentInChildren<Rigidbody2D>().isKinematic = true;
            GetComponentInChildren<Rigidbody2D>().transform.position = (new Vector2(transform.position.x, transform.position.y));
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (chanceToGivePotion <= 0.1f && potcap == 0)
            {
                Instantiate(healthPotion[Random.Range(0, 1)], new Vector3(potSpawn.position.x, potSpawn.position.y + 1, potSpawn.position.z), Quaternion.identity, GameObject.Find("Itens").transform);
            }

            if((chanceToGivePotion > 0.1f && chanceToGivePotion < 0.11f) && potcap == 0)
            {
                Instantiate(healthPotion[2], new Vector3(potSpawn.position.x, potSpawn.position.y + 1, potSpawn.position.z), Quaternion.identity, GameObject.Find("Itens").transform);
            }

            if (chanceToGiveCoin <= 0.2f && potcap == 0)
            {
                Instantiate(coin, new Vector3(potSpawn.position.x, potSpawn.position.y + 1, potSpawn.position.z), Quaternion.identity, GameObject.Find("Itens").transform);
            }
            StartCoroutine(ToDestroy());
            potcap = 1;
        }
    }

    public void DeadByFire()
    {
        animBehaviour = GetComponent<Animator>();

        animBehaviour.SetBool("dying", true);

        GetComponentInChildren<Rigidbody2D>().isKinematic = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(ToDestroyByFire());
    }

    public void BossIsDead()
    {
        if (dead == true)
        {
            animBehaviour.SetBool("dying", true);

            GetComponentInChildren<Rigidbody2D>().simulated = false;
            GetComponentInChildren<Rigidbody2D>().transform.position = (new Vector2(transform.position.x, transform.position.y));
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (potcap <= (int)chanceToGiveCoin)
            {
                Instantiate(coin, new Vector3(potSpawn.position.x,
                    potSpawn.position.y + 1, potSpawn.position.z),
                    Quaternion.identity, GameObject.Find("Itens").transform);
                potcap++;
             }
        }
    }
    public void Routine()
    {
        animBehaviour = GetComponent<Animator>();

        if (dead == false)
        {
            if (lookingHero)
            {
                Attack();
            }
            else
            {
                Movimento();
            } 
        }
        Checking();
    }

    public void Routine2()
    {
        animBehaviour = GetComponent<Animator>();

        Vector3 v2 = player.transform.position - transform.position;
        v2 = v2.normalized;

        if (dead == false)
        {
            if (Physics2D.Linecast(transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Player")))
            {
                if (lookingHero)
                {
                    Attack();
                }
                else
                {
                    if (Vector2.Distance(player.transform.position, transform.position) > 1.0f && v2.x > 0)
                    {
                        contador2 += Time.deltaTime;
                        if (contador2 < 2)
                        {
                            Idle();
                        }
                        if (contador2 > 2)
                        {
                            Movimento();
                            transform.position += (v2 * speed * Time.deltaTime);
                            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                            contador = 0;
                        }
                    }
                    else
                    {
                        contador += Time.deltaTime;
                        if (contador < 2)
                        {
                            Idle();
                        }
                        if (contador > 2)
                        {
                            Movimento();
                            transform.position += (v2 * speed * Time.deltaTime);
                            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                        }
                        contador2 = 0;
                    }
                }
            }
        }
    }

    public void Routine3(float vel)
    {
        animBehaviour = GetComponent<Animator>();

        if (dead == false)
        {
            if (Physics2D.Linecast(transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Player")))
            {
                Vector3 v2 = player.transform.position - transform.position;
                v2 = v2.normalized;

                if (Vector2.Distance(player.transform.position, transform.position) > 1.0f && v2.x > 0)
                {
                    contador2 += Time.deltaTime;
                    if (contador2 < 2)
                    {
                        Idle();
                        if (contador2 > .5f && contador2 < 1)
                        {
                            Idle();
                            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                        }
                        if (contador2 > 1 && contador2 < 2)
                        {
                            Idle();
                            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                        }
                    }
                    if (contador2 > 2)
                    {
                        transform.position += (v2 * vel * Time.deltaTime);
                        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                        contador = 0;
                    }
                }
                else
                {
                    contador += Time.deltaTime;
                    if (contador < 2)
                    {
                        Idle();
                    }
                    if (contador > 2)
                    {
                        transform.position += (v2 * vel * Time.deltaTime);
                        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                    }
                    contador2 = 0;
                }
            }
        }
    }

    public void SantaRoutine()
    {
        animBehaviour = GetComponent<Animator>();

        Vector3 v2 = player.transform.position - transform.position;
        v2 = v2.normalized;

        if (dead == false)
        {
            IsOnGround();

            if (Physics2D.Linecast(transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Player")))
            {
                if (obstacle)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 25 * 100));
                }
                else
                {
                    if ((Vector2.Distance(player.transform.position, transform.position) > 1.0f && v2.x > 0) && lookingHero)
                    {
                        contador2 += Time.deltaTime;
                        if (contador2 < 2)
                        {
                            Idle();
                        }
                        if (contador2 > 2)
                        {
                            Movimento();
                            transform.position += (v2 * speed * Time.deltaTime);
                            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                            contador = 0;
                        }
                    }
                    else
                    {
                        contador += Time.deltaTime;
                        if (contador < 2)
                        {
                            Idle();
                        }
                        if (contador > 2)
                        {
                            Movimento();
                            transform.position += (v2 * speed * Time.deltaTime);
                            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                        }
                        contador2 = 0;
                    }
                }
            }
        }
    }

    void IsOnGround()
    {
        animBehaviour = GetComponent<Animator>();

        if (touchGround)
        {
            animBehaviour.SetBool("jumping", false);
        }
        else
        {
            animBehaviour.SetBool("jumping", true);
        }
    }

    public void AtributesBonusForEnemies(int currentLevel)
    {
        if (currentLevel > 1)
        {
            float bonus = currentLevel * .5f;
            GetComponent<EnemyBase>().armor += (int)(bonus + 2 * .5f);
            GetComponent<HealthPointsEnemy>().fullHealth += ((int)bonus * .5f) + 20;
            GetComponent<EnemyBase>().enemyDamage += (int)((bonus * 2) * .5f);
        }
    }
    public void PlayerHitEnemy(int wpnDmg, int plyrDmg, int enemyDef, float cRate, float eLife)
    {
        float randomValue = Random.value;

        cRate = (cRate / 100) + randomValue;

        if (cRate >= 1f)
        {
            cRate = 1f;
        }

        if (cRate < 0.9f)
        {
            player.GetComponent<AttackBase>().criticalDamage = false;
            eLife = Mathf.Clamp((wpnDmg + plyrDmg) - enemyDef, 0f, 9999);
            player.GetComponent<AttackBase>().damageSuffer = eLife;
            GetComponent<HealthPointsEnemy>().ShowText("", Color.white, eLife);
        }
        else
        {
            player.GetComponent<AttackBase>().criticalDamage = true;
            eLife = Mathf.Clamp((wpnDmg + plyrDmg) * 2, 0f, 9999);
            player.GetComponent<AttackBase>().damageSuffer = eLife;
            GetComponent<HealthPointsEnemy>().ShowText("CRITICO!!\n", Color.yellow, eLife);
        }
        GetComponent<HealthPointsEnemy>().currentHealth -= eLife;
        
      //  Instantiate(GetComponent<HealthPointsEnemy>().dmgTxt,
      //new Vector3(GetComponent<HealthPointsEnemy>().pivot.transform.position.x,
      //            GetComponent<HealthPointsEnemy>().pivot.transform.position.y,
      //            GetComponent<HealthPointsEnemy>().pivot.transform.position.z),
      //            Quaternion.identity,
      //            GetComponent<HealthPointsEnemy>().pivot.transform);
    }
    public IEnumerator ToDestroy()
    {
        yield return new WaitForSeconds(3);

        if (Time.time >= 3)
        {
            HealthPointsEnemy temp = GetComponent<HealthPointsEnemy>();
            temp.GetKilled();
            GameObject.Find("LevelUpManager").GetComponent<LevelUpManager>().EarnExp(expGiven);

            Destroy(thisEnemy);
            Destroy(lifeUI);
        }
    }

    public IEnumerator ToDestroyByFire()
    {
        yield return new WaitForSeconds(3);

        if (Time.time >= 3)
        {
            HealthPointsEnemy temp = GetComponent<HealthPointsEnemy>();
            temp.GetKilled();

            Destroy(thisEnemy);
            Destroy(lifeUI);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            DeadByFire();
        }
    }
}

