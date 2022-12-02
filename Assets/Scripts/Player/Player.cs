using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using AttackClass;
using UIClass;

namespace PlayerBase
{
    public class Player : MonoBehaviour, IPlayerCombat
    {
        [SerializeField] protected ScriptableCharacters _class;
        public ScriptableCharacters classAtributes { get { return _class; } }

        public static Player instance;
        [SerializeField] private Transform _footDetector1, _footDetector2;
        [SerializeField] private GameObject character;
        [SerializeField] private Transform spawnPoint;

        private int count, count2 = 1;

        private float timer = 0;

        public Animator animBehaviour { get; set; }

        private int _isRightInt;

        public bool dashCooldown { get; set; }

        [Header("Custo de Uso Habilidades")]
        [SerializeField]
        int _costMobility;

        [SerializeField] int _costShadow;

        private ParticleSystem tr;
        private Vector2 moving;

        private bool doubleJump;
       
        int isRightInt;
        private bool touchGround;

        public bool isOnGround { get { return touchGround; } set { touchGround = value; } }

        bool _isRight = true;
        public bool isRight { get { return _isRight; } set { _isRight = value; } }
        
        private bool _canMove;
        public bool canMove { get { return _canMove; } set { _canMove = value; } }

        [SerializeField] int _playerLevel;
        public int playerLevel { get { return _playerLevel; } set { _playerLevel = value; } }

        int _damage;
        public int damage { get { return _damage; } set { _damage = value; } }

        int _playerArmor;
        public int playerArmor { get { return _playerArmor; } set { _playerArmor = value; } }

        [SerializeField] float _maxHealth;
        public float maxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

        [SerializeField] float _maxMana;
        public float maxMana { get { return _maxMana; } set { _maxMana = value; } }

        float _manaRegen;
        public float manaRegen { get { return _manaRegen; } set { _manaRegen = value; } }

        float _criticalChance;
        public float criticalChance { get { return _criticalChance; } set { _criticalChance = value; } }

        float _currentMana;
        public float currentMana { get { return _currentMana; } set { _currentMana = value; } }
        
        float _currentHealth;
        public float currentHealth { get { return _currentHealth; } set { _currentHealth = value; } }


        bool _canRegenMana;
        public bool canRegenMana { get { return _canRegenMana; } set { _canRegenMana = value; } }

        public Transform footDetector1
        {
            get
            {
                return _footDetector1;
            }
        }

        public Transform footDetector2
        {
            get
            {
                return _footDetector2;
            }
        }

        protected void Awake()
        {
            instance = this;
            character = this.gameObject;

            Instantiate(_class.lifeBar);
        }

        public void SettingPlayerLevel(int level)
        {
            _playerLevel = level;

            if (_playerLevel > 1)
            {
                float bonus = _playerLevel * .2f;

                playerArmor += (int)bonus + (int).2f;
                _currentHealth += (int)bonus;
                damage += (int)bonus + (int).2f;
                _currentMana += (int)bonus;
            }
        }

        public void EnemyHitPlayer(int enmyDmg, int plyrDef, float pLife)
        {

            pLife = Mathf.Clamp(enmyDmg - plyrDef, 0f, 9999);
            _currentHealth -= pLife;

            GetComponent<AttackBase>().damageSuffer = pLife;

            GetComponent<PlayerUIManager>().ShowText("",Color.white, GetComponent<AttackBase>().damageSuffer);

            GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-50, 50), ForceMode2D.Impulse);

            PlayerUIManager.instance.HealthUpdate(maxHealth, currentHealth);
        }

        public void DefaultMoving()
        {
            if (isRight)
            {
                isRightInt = 1;
            }
            else
            {
                isRightInt = -1;
            }

            animBehaviour = GetComponent<Animator>();
            Vector2 pos2 = GetComponent<Player>().transform.position;
            animBehaviour.SetFloat("walk", Mathf.Abs(Input.GetAxis("Horizontal")));

            if (Input.GetAxis("Horizontal") > 0)
            {
                isRight = true;
                transform.Translate(Vector2.right * _class.speed);
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

                if (Input.GetKey(KeyCode.V) & ((_currentMana >= 2 & dashCooldown == false)))
                {
                    dashCooldown = true;
                    SpecialDash(true);
                    StartCoroutine(DashCoolDown());
                    _currentMana -= _class.costMobility;
                    
                    PlayerUIManager.instance.ManaUpdate(maxMana, currentMana);

                    transform.position = new Vector3(pos2.x + 10, pos2.y, 20);
                }
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                isRight = false;
                transform.Translate(Vector2.left * _class.speed);
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

                if (Input.GetKey(KeyCode.V) & ((_currentMana >= _class.costMobility & dashCooldown == false)))
                {
                    dashCooldown = true;
                    SpecialDash(true);
                    StartCoroutine(DashCoolDown());
                    _currentMana -= _class.costMobility;

                    PlayerUIManager.instance.ManaUpdate(maxMana, currentMana);

                    transform.position = new Vector3(pos2.x - 10, pos2.y, 20);
                }
            }
        }

        public void CanJump()
        {
            this.GetComponent<Rigidbody2D>();
            if (Player.instance.isOnGround)
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _class.speedJump * 100));
        }
        public void CanDoubleJump()
        {
            this.GetComponent<Rigidbody2D>();
            if (!Player.instance.isOnGround)
            {
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _class.speedJump * 100));
            }
            _currentMana -= _class.costMobility;

            PlayerUIManager.instance.ManaUpdate(maxMana, currentMana);
        }
        public void Jumping()
        {
            this.GetComponent<Animator>();
            //Pulo só é habilitado enquanto o footDetector do player estiver encostado na layer "Ground"
            if ((Physics2D.Linecast(transform.position, footDetector1.position, 1 << LayerMask.NameToLayer("Ground")) | (Physics2D.Linecast(transform.position, footDetector1.position, 1 << LayerMask.NameToLayer("FloatingPlatform")) | (Physics2D.Linecast(transform.position, footDetector1.position, 1 << LayerMask.NameToLayer("PushPull"))))) | (Physics2D.Linecast(transform.position, footDetector2.position, 1 << LayerMask.NameToLayer("Ground")) | (Physics2D.Linecast(transform.position, footDetector2.position, 1 << LayerMask.NameToLayer("FloatingPlatform")) | (Physics2D.Linecast(transform.position, footDetector2.position, 1 << LayerMask.NameToLayer("PushPull"))))))
            {
                isOnGround = true;
                this.GetComponent<Animator>().SetBool("jump", false);
                count = 1;
                count2 = 1;
            }
            else
            {
                isOnGround = false;
                this.GetComponent<Animator>().SetBool("jump", true);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CanJump();

                if (((Input.GetKeyDown(KeyCode.Space) && isOnGround == false) && currentMana >= _class.costMobility))
                {

                    if (count > 0 && count2 > 0)
                    {
                        count -= 1;
                        count2 -= 1;
                        CanDoubleJump();
                    }
                }
            }
        }

        public void SpecialDash(bool dash)
        {
            timer = 0;
        }



        public void EnterSuperTime()
        {
            GameObject _globalLight = GameObject.FindGameObjectWithTag("GLOBAL_LIGHT");
            _globalLight.GetComponent<Light2D>().intensity = 0;

            GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;

            canMove = false;

            GameBehaviour.instance.superAttack = true;

        }
        public void ExitSuperTime()
        {
            GameObject _globalLight = GameObject.FindGameObjectWithTag("GLOBAL_LIGHT");
            _globalLight.GetComponent<Light2D>().intensity = 1;

            GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;

            canMove = true;

            GameBehaviour.instance.superAttack = false;
        }

        public void ManaRegen(float regenRate)
        {
            regenRate = (regenRate / 100);

            if (currentMana < maxMana)
            {
                currentMana += regenRate;

                PlayerUIManager.instance.ManaUpdate(maxMana, currentMana);
            }
        }


        public IEnumerator DashCoolDown()
        {
            yield return new WaitForSeconds(1);

            dashCooldown = false;
        }

        public IEnumerator CoolDownToRegenMana()
        {
            yield return new WaitForSeconds(10);

            canRegenMana = true;

            yield return 0;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                EnemyHitPlayer(collision.gameObject.GetComponent<EnemyBase>().enemyDamage,
                    playerArmor, currentHealth);
            }

            if (collision.collider.CompareTag("Fire"))
            {
                GameBehaviour.instance.itsOver = true;
            }

            if (collision.collider.CompareTag("Coin"))
            {
                GameBehaviour.instance.coin++;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Fire"))
            {
                _currentHealth = 0;

                PlayerUIManager.instance.HealthUpdate(maxHealth, currentHealth);
            }
        }
    }
}