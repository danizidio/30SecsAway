using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBase;


public class Dagger : MonoBehaviour
{
    #region - Atributes -
    [SerializeField] private float _speedThrow;
    [SerializeField] int _weaponDamage;

    GameObject _player;

    private bool isStuck;

    private float counting;

    public float speedThrow
    {
        get
        {
            return _speedThrow;
        }

        set
        {
            _speedThrow = value;
        }
    }

    public int weaponDamage
    {
        get
        {
            return _weaponDamage;
        }

        set
        {
            _weaponDamage = value;
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

    #endregion


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        DaggerCommon();
        StartCoroutine(TimeToDie());
    }

    void Update()
    {
        
    }


    public void DaggerCommon()
    {
        if (isStuck == false && this.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
        {
            
            if (Player.instance.isRight)
            {
                transform.eulerAngles = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().velocity = new Vector2((speedThrow * (1)), 0);
            }
            if (!Player.instance.isRight)
            {
                transform.eulerAngles = new Vector2(0, -180);
                GetComponent<Rigidbody2D>().velocity = new Vector2((speedThrow * (-1)), 0);
            }

            if(Player.instance.isOnGround == false & Player.instance.isRight)
            {
                transform.eulerAngles = new Vector3(0, 0, -45);
                GetComponent<Rigidbody2D>().velocity = new Vector2((speedThrow * (1)),speedThrow * -1);
            }
            if (Player.instance.isOnGround == false & !Player.instance.isRight)
            {
                transform.eulerAngles = new Vector3(0, -180, -45);
                GetComponent<Rigidbody2D>().velocity = new Vector2((speedThrow * (-1)), speedThrow * -1);
            }
        }
    }
    public void SpecialDagger()
    {
        if (Player.instance.isRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2((speedThrow * (1)), 0);
        }
        if (!Player.instance.isRight)
        {
            transform.eulerAngles = new Vector2(0, -180);
            GetComponent<Rigidbody2D>().velocity = new Vector2((speedThrow * (-1)), 0);
        }

        if (ShadowAttack.instance.onTheGround == false & Player.instance.isRight)
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
            GetComponent<Rigidbody2D>().velocity = new Vector2((speedThrow * (1)), speedThrow * -1);
        }
        if (ShadowAttack.instance.onTheGround == false & !Player.instance.isRight)
        {
            transform.eulerAngles = new Vector3(0, -180, -45);
            GetComponent<Rigidbody2D>().velocity = new Vector2((speedThrow * (-1)), speedThrow * -1);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Cenario"))
        {

            if (Player.instance.isRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
                isStuck = true;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
                isStuck = true;
            }

            if (Player.instance.isOnGround == false & Player.instance.isRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(3, 3);
            }

            if (Player.instance.isOnGround == false & !Player.instance.isRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 3);
            }
        }

        if (isStuck == false)
        {
            if (collision.collider.CompareTag("Walls"))
            {
                this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                this.GetComponent<PolygonCollider2D>().enabled = false;
            }
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject e = collision.gameObject;

                Component damageable = collision.gameObject.GetComponent(typeof(IEnemyCombat));
                if (damageable)
                {
                    e.GetComponent<HealthPointsEnemy>().isHit = true;
                    (damageable as IEnemyCombat).PlayerHitEnemy(weaponDamage, player.GetComponent<Player>().damage,
                        e.GetComponent<EnemyBase>().armor, player.GetComponent<Player>().criticalChance, e.GetComponent<HealthPointsEnemy>().currentHealth);

                    Destroy(this.gameObject);
                }
                else
                {
                    e.GetComponent<HealthPointsEnemy>().isHit = false;
                }
            }
        }
    }
    

    public IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(5);

        if(Time.time >= 5)
        {
            Destroy(this.gameObject);
        }
    }
}

