using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaiseTheflag : MonoBehaviour
{

    [SerializeField] GameObject coins;
    float timer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            print("foi");
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                Instantiate(coins, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            GameBehaviour.instance.nextState = GameStates.STAGECLEAR;
            timer += Time.deltaTime;

        }
    }
}
