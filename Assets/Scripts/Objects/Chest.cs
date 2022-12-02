using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject coin;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int potcap;
    float chanceToGiveCoin, timer;
    bool canRain;

    private void Start()
    {
        chanceToGiveCoin = Random.Range(1, 10);
    }

    private void Update()
    {
        if(canRain)
        {
            Cronometro.instance.relogio = false;
            StartCoroutine(RainOfCoins());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {

            StartCoroutine(TimeToCollect());

            canRain = true;
        }
    }

    IEnumerator RainOfCoins()
    {
        yield return null;

        timer += Time.deltaTime;
        if (timer >= .2f && potcap <= (int)chanceToGiveCoin)
        {
            Instantiate(coin, new Vector3(spawnPoint.position.x,
                spawnPoint.position.y + 1, spawnPoint.position.z),
                Quaternion.identity);
            
            potcap++;

            timer = 0;
        }
    }
    IEnumerator TimeToCollect()
    {
        yield return new WaitForSeconds(10);

        GameBehaviour.instance.nextState = GameStates.STAGECLEAR;
    }
}
