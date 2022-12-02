using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBase;

public class FireAttack : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] int damage;
    GameObject player;
    bool dir;

    private void Awake()
    {
        player = GameObject.Find("MKnight(Clone)");

        if (player.GetComponent<Knight>().isRight)
        {
            dir = true;
        }
    }

    private void LateUpdate()
    {
        if (dir)
        {
            transform.Translate(Vector2.right * speed);
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.Translate(Vector2.left * speed);
            transform.eulerAngles = new Vector2(0, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject e = collision.gameObject;

            Component damageable = collision.gameObject.GetComponent(typeof(IEnemyCombat));
            if (damageable)
            {
                e.GetComponent<HealthPointsEnemy>().isHit = true;
                (damageable as IEnemyCombat).PlayerHitEnemy(damage, player.GetComponent<Player>().damage,
                    e.GetComponent<EnemyBase>().armor, player.GetComponent<Player>().criticalChance, e.GetComponent<HealthPointsEnemy>().currentHealth);
                e.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(60, 20), ForceMode2D.Impulse);
                e.GetComponent<HealthPointsEnemy>().isBurnning = true;
            }
        }
    }
}
