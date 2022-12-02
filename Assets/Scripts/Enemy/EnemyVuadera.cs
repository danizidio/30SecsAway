using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyVuadera : MonoBehaviour
{

    HealthPointsEnemy healthEnemy;
    EnemyBase enemyBase;
    [SerializeField] Transform enemyView1;
    [SerializeField] Transform enemyView2;
    [SerializeField] Transform enemyView3;
    [SerializeField] Transform viewLine;

    [SerializeField] private bool heroFar;
    [SerializeField] private bool vuaderaReady;

    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        enemyBase.healthPotion = null;
        enemyBase.animBehaviour = GetComponent<Animator>();
        healthEnemy = GetComponent<HealthPointsEnemy>();
        healthEnemy.HealthStart();
        heroFar = false;
        

        //LevelUpManager.instance.AtributesBonusForEnemies(enemyBase.armor,
        //                                             healthEnemy.fullHealth,
        //                                             healthEnemy.currentHealth,
        //                                             enemyBase.enemyDamage);

        //GameBehaviour.instance.bossSpawn = false;
    }

    void Update()
    {
        healthEnemy.HealthUpdate();
        GetComponent<EnemyBase>().DeadOrAlive();
        LineCasters();


        if (vuaderaReady)
        {
            Vuadera();
        }
        else
        {
            if (heroFar)
            {
                GetComponent<EnemyBase>().Run();
                GetComponent<EnemyBase>().Routine3(GetComponent<EnemyBase>().speed * 5);

            }
            else
            {
                GetComponent<EnemyBase>().Movimento();
                GetComponent<EnemyBase>().Routine3(GetComponent<EnemyBase>().speed * 1);

            }
        }
    }
    void LineCasters()
    {
        if (Physics2D.Linecast(viewLine.position, enemyView2.transform.position, 1 << LayerMask.NameToLayer("Player")) && GetComponent<EnemyBase>().lookingHero == false)
        {
            heroFar = true;
        }
        else
        {
            heroFar = false;
        }

        if (Physics2D.Linecast(viewLine.position, enemyView1.position, 1 << LayerMask.NameToLayer("Player")))
        {
            GetComponent<EnemyBase>().lookingHero = true;
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            GetComponent<EnemyBase>().lookingHero = false;
        }

        if (Physics2D.Linecast(viewLine.position, GetComponent<EnemyBase>().enemyView.position, 1 << LayerMask.NameToLayer("Player")))
        {
            GetComponent<EnemyBase>().lookingHero = true;
        }
        else
        {
            GetComponent<EnemyBase>().lookingHero = false;
        }

        if (Physics2D.Linecast(viewLine.position, enemyView3.position, 1 << LayerMask.NameToLayer("Player")) && !Physics2D.Linecast(viewLine.position, GetComponent<EnemyBase>().enemyView.position, 1 << LayerMask.NameToLayer("Player")))
        {
            vuaderaReady = true;
        }
        else
        {
            vuaderaReady = false;
        }

        if (Physics2D.Linecast(transform.position, GetComponent<EnemyBase>().groundDetector.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            GetComponent<EnemyBase>().touchGround = true;
        }
        else
        {
            GetComponent<EnemyBase>().touchGround = false;
        }

        if (Physics2D.Linecast(transform.position, GetComponent<EnemyBase>().wallDetector.position, 1 << LayerMask.NameToLayer("Wall")))
        {
            GetComponent<EnemyBase>().touchWall = true;
        }
        else
        {
            GetComponent<EnemyBase>().touchWall = false;
        }
    }

    void Vuadera()
    {
        if (transform.rotation.y ==  0)
        {
            GetComponent<EnemyBase>().Attack();
            GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x + 500 * Time.deltaTime, transform.position.y * 0), ForceMode2D.Impulse);
        }
        if (transform.rotation.y == -180)
        {
            GetComponent<EnemyBase>().Attack();
            GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x + (-1 * 500 * Time.deltaTime), transform.position.y * 0), ForceMode2D.Impulse);
        }
    }

    void Escaping()
    {

    }
    void OnDrawGizmosSelected()
    {
        if (GetComponent<EnemyBase>().player != null)
        {
            Gizmos.DrawLine(viewLine.position, GetComponent<EnemyBase>().enemyView.position);
            Gizmos.DrawLine(viewLine.position, GetComponent<EnemyBase>().player.transform.position);
            Gizmos.DrawLine(viewLine.position, enemyView1.position);
            Gizmos.DrawLine(viewLine.position, enemyView2.position);
            Gizmos.DrawLine(viewLine.position, enemyView3.position);
        }
    }

}
