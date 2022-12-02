using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySanta : MonoBehaviour
{

    HealthPointsEnemy healthEnemy;
    EnemyBase enemyBase;

    [SerializeField] Transform enemyView1;
    [SerializeField] Transform enemyView2;

    [SerializeField] Transform viewLine;

    [SerializeField] bool canFollow;
    [SerializeField] bool heroFar;

    [SerializeField] GameObject[] gifts;

    float cooldown, cooldown2, countdown;

    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
 
        enemyBase.animBehaviour = GetComponent<Animator>();
        healthEnemy = GetComponent<HealthPointsEnemy>();
        heroFar = false;
        enemyBase.AtributesBonusForEnemies(LevelUpManager.instance.currentLevel);

        healthEnemy.HealthStart();
    }

    void Update()
    {
        healthEnemy.HealthUpdate();
        enemyBase.DeadOrAlive();

        if (enemyBase.dead != true)
        {
            LineCasters();

            if (heroFar)
            {
                enemyBase.Run();
                ThrowGifts();
            }
            else
            {
                if (canFollow)
                {
                    enemyBase.SantaRoutine();
                }
            }
        }
    }
    void LineCasters()
    {
        if (Physics2D.CircleCast(viewLine.position, enemyView2.localPosition.x, Vector2.zero, enemyView2.localPosition.x, 1 << LayerMask.NameToLayer("Player")) && GetComponent<EnemyBase>().lookingHero == false)
        {
            heroFar = true;
        }
        else
        {
            heroFar = false;
        }

        if (canFollow)
        {
            if (Physics2D.CircleCast(viewLine.position, enemyView1.localPosition.x, Vector2.zero, enemyView1.localPosition.x, 1 << LayerMask.NameToLayer("Player")))
            {
                enemyBase.lookingHero = true;
                Escaping();
            }
            else
            {
                enemyBase.lookingHero = false;
            }
        }

        if (Physics2D.Linecast(transform.position, GetComponent<EnemyBase>().groundDetector.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            enemyBase.touchGround = true;
        }
        else
        {
            enemyBase.touchGround = false;
        }

        if (Physics2D.Linecast(transform.position, GetComponent<EnemyBase>().wallDetector.position, 1 << LayerMask.NameToLayer("Wall")))
        {
            enemyBase.touchWall = true;
        }
        else
        {
            enemyBase.touchWall = false;
        }

        if (Physics2D.Linecast(transform.position, GetComponent<EnemyBase>().wallDetector.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            enemyBase.obstacle = true;
        }
        else
        {
            enemyBase.obstacle = false;
        }
    }

    void Escaping()
    {
        Vector3 v2 = new Vector3(enemyBase.player.transform.position.x - transform.position.x, 0, 0);
        v2 = v2.normalized;

        if (canFollow)
        {
            if (Vector2.Distance(enemyBase.player.transform.position, transform.position) > 1.0f && v2.x > 0)
            {
                transform.position -= (v2 * 10 * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.position -= (v2 * 10 * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
            }

            heroFar = false;
        }
    }

    void ThrowGifts()
    {
        if (countdown <= 15)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= .2f)
            {
                Instantiate(gifts[Random.Range(0, gifts.Length)],
                    (new Vector3(viewLine.position.x, viewLine.position.y, viewLine.position.z)),
                    Quaternion.identity);
                cooldown = 0;
                countdown++;
            }
        }
        if(countdown >= 15)
        {
            cooldown2 += Time.deltaTime;
            if(cooldown2 >=3)
            {
                countdown = 0;
                cooldown2 = 0;
            }
        }
    }
}

