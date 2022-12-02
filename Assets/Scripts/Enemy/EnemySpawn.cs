using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] bool normalEnemies;
    [SerializeField] int totalEnemies;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject enemyToSpawn = null;
    [Space(10)]
    [SerializeField] bool bossEnemies;
    [SerializeField] GameObject[] bosses;
    [SerializeField] GameObject bossToSpawn = null;
    [SerializeField] GameObject bossPosition;
    [Space(10)]
    [SerializeField] bool specialEnemy;
    [SerializeField] GameObject[] specialEnemies;
    [SerializeField] GameObject SpecialEnemyToSpawn = null;
    [Space(10)]

    private GameObject wheretoSpawn;
    [SerializeField] float timeToSpawn;

    int enemyCount, bossCount;
    float a = 0;

    void Start()
    {
        wheretoSpawn = this.gameObject;

        bossCount = 0;

        foreach (GameObject enemyToSpawn in enemies)
        {

        }

        foreach (GameObject bossToSpawn in bosses)
        {

        }

        if (specialEnemy)
        {
           SpecialEnemyToSpawn =  Instantiate(specialEnemies[Random.Range(0, specialEnemies.Length)],
                                  (new Vector3(wheretoSpawn.transform.position.x,
                                   wheretoSpawn.transform.position.y, 20)),
                                   Quaternion.identity,
                                   GameObject.Find("EnemiesContainer").transform);
        }
    }


    void Update()
    {
        if (enemyCount <= totalEnemies && normalEnemies)
        {
            a += Time.deltaTime;
            if (a >= timeToSpawn)
            {
                enemyToSpawn = Instantiate(enemies[Random.Range(0, enemies.Length)],
                              (new Vector3(wheretoSpawn.transform.position.x,
                               wheretoSpawn.transform.position.y, 20)),
                               Quaternion.identity,
                               GameObject.Find("EnemiesContainer").transform);
                enemyCount++;
                a = 0;
            }
        }

        if (bossEnemies)
        {
            if (GameBehaviour.instance.enemiesKilled >= GameBehaviour.instance.maxToKill && bossCount < 1)
            {
                int newCount=0;
                bossToSpawn = Instantiate(bosses[Random.Range(0, bosses.Length)],
                              (new Vector3(bossPosition.transform.position.x,
                               bossPosition.transform.position.y + 5, 20)),
                               Quaternion.identity,
                               GameObject.Find("EnemiesContainer").transform);
                bossCount++;
                if (newCount <= 5)
                {

                    enemyToSpawn = Instantiate(enemies[Random.Range(0, enemies.Length)],
                                  (new Vector3(bossPosition.transform.position.x + newCount,
                                   bossPosition.transform.position.y, 20)),
                                   Quaternion.identity,
                                   GameObject.Find("EnemiesContainer").transform);
                    newCount++;
                }
            }
        }
    }
}
