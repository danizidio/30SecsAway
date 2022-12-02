using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBase;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private int _weaponDamage;

    [SerializeField]
    [Tooltip("O quanto o ataque vai empurrar o inimigo no eixo X")]
    private float _empurrandoX;

    [SerializeField]
    [Tooltip("O quanto o ataque vai empurrar o inimigo no eixo Y")]
    private float _empurrandoY;

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

    public float empurrandoX
    {
        get
        {
            return _empurrandoX;
        }

        set
        {
            _empurrandoX = value;
        }
    }

    public float empurrandoY
    {
        get
        {
            return _empurrandoY;
        }

        set
        {
            _empurrandoY = value;
        }
    }

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject e = collision.gameObject;

            Component damageable = collision.gameObject.GetComponent(typeof(IEnemyCombat));
            if (damageable)
            {
                if(!player.GetComponent<Player>().isRight)
                {
                    empurrandoX *= -1;
                }
                else
                {
                    empurrandoX *= 1;
                }

                e.GetComponent<HealthPointsEnemy>().isHit = true;
                (damageable as IEnemyCombat).PlayerHitEnemy(weaponDamage, player.GetComponent<Player>().damage,
                    e.GetComponent<EnemyBase>().armor, player.GetComponent<Player>().criticalChance, e.GetComponent<HealthPointsEnemy>().currentHealth);

                e.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(empurrandoX, empurrandoY), ForceMode2D.Impulse);

            }
            else
            {
                e.GetComponent<HealthPointsEnemy>().isHit = false;
            }
        }
    }
}
