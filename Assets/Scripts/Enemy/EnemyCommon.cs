using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    HealthPointsEnemy healthEnemy;
    EnemyBase enemyBase;
    void Start()
    {
        //lifeUI = Instantiate(lifeUI, GameObject.Find("Canvas").transform);
        //this.name = EnemyBase.instance.enemyNames[Random.Range(0, 
        //            EnemyBase.instance.enemyNames.Length)].ToString();

        healthEnemy = GetComponent<HealthPointsEnemy>();
        healthEnemy.HealthStart();
        enemyBase = GetComponent<EnemyBase>();
        //LevelUpManager.instance.AtributesBonusForEnemies(enemyBase.armor,
        //                                                 healthEnemy.fullHealth,
        //                                                 healthEnemy.currentHealth,
        //                                                 enemyBase.enemyDamage);

    }
	
	void Update ()
    {
        healthEnemy.HealthUpdate();

        if (enemyBase.touchGround)
        {
            if (Physics2D.Linecast(transform.position, GetComponent<EnemyBase>().player.transform.position, 1 << LayerMask.NameToLayer("Player")))
            {
                Vector3 v2 = GetComponent<EnemyBase>().player.transform.position - transform.position;
                v2 = v2.normalized;

                if (Vector2.Distance(GetComponent<EnemyBase>().player.transform.position, transform.position) > 1.0f && v2.x > 0)
                {
                    transform.position += (v2 * GetComponent<EnemyBase>().speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.position += (v2 * GetComponent<EnemyBase>().speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, -180, 0);
                }
            }
        }

        if (Physics2D.Linecast(transform.position, enemyBase.groundDetector.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            enemyBase.touchGround = true;
        }
        else
        {
            enemyBase.touchGround = false;
        }

        if (Physics2D.Linecast(transform.position, enemyBase.wallDetector.position, 1 << LayerMask.NameToLayer("Wall")))
        {
            enemyBase.touchWall = true;
        }
        else
        {
            enemyBase.touchWall = false;
        }

        if (Physics2D.Linecast(transform.position, enemyBase.enemyView.position, 1 << LayerMask.NameToLayer("Player")))
        {
            enemyBase.lookingHero = true;
        }
        else
        {
            enemyBase.lookingHero = false;
        }

        if (enemyBase.dead == true)
        {
            enemyBase.chanceToGivePotion = Random.value;
            enemyBase.chanceToGiveCoin = Random.value;
           

            GetComponentInChildren<Rigidbody2D>().isKinematic = true;

            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (enemyBase.chanceToGivePotion <= 0.2f && enemyBase.potcap == 0)
            {
                Instantiate(enemyBase.healthPotion[Random.Range(0, enemyBase.healthPotion.Length)], new Vector3
                    (enemyBase.potSpawn.position.x,
                     enemyBase.potSpawn.position.y + 1,
                     enemyBase.potSpawn.position.z), 
                     Quaternion.identity, GameObject.Find("Itens").transform);

            }

            if (enemyBase.chanceToGiveCoin <= 0.2f && enemyBase.potcap == 0)
            {
                Instantiate(enemyBase.coin, new Vector3
                    (enemyBase.potSpawn.position.x,
                     enemyBase.potSpawn.position.y + 1,
                     enemyBase.potSpawn.position.z), 
                     Quaternion.identity, GameObject.Find("Itens").transform);
            }
            StartCoroutine(enemyBase.ToDestroy());
            enemyBase.potcap = 1;
        }

    }
}
