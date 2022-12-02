using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBase;

public class EnemyZombie : MonoBehaviour
{
    HealthPointsEnemy healthEnemy;
    EnemyBase enemyBase;

    void Start()
    {
        healthEnemy = GetComponent<HealthPointsEnemy>();

        enemyBase = GetComponent<EnemyBase>();

        enemyBase.AtributesBonusForEnemies(LevelUpManager.instance.currentLevel);

        healthEnemy.HealthStart();
    }

    void Update()
    {
        healthEnemy.HealthUpdate();
        GetComponent<EnemyBase>().DeadOrAlive();
        GetComponent<EnemyBase>().Checking();
        GetComponent<EnemyBase>().Routine2();
        healthEnemy.Burning();
    }

    void OnDrawGizmosSelected()
    {
        if (GetComponent<EnemyBase>().player != null)
        {
            Gizmos.DrawLine(transform.position, GetComponent<EnemyBase>().enemyView.position);
            Gizmos.DrawLine(transform.position, GetComponent<EnemyBase>().player.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-100, 100), ForceMode2D.Force);
            //collision.GetComponent<AttackBase>().TakingDamage(collision.gameObject, GetComponent<EnemyBase>().enemyDamage);

            Component damageable = collision.gameObject.GetComponent(typeof(IPlayerCombat));
            if(damageable)
            {
                (damageable as IPlayerCombat).EnemyHitPlayer(GetComponent<EnemyBase>().enemyDamage,
                    collision.GetComponent<Player>().playerArmor, collision.GetComponent<Player>().currentHealth);

            }
        }
    }
}
