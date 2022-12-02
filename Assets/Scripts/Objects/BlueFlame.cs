using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFlame : MonoBehaviour
{
    [SerializeField] bool inTheDark;
    [SerializeField] float timer, spawnSpeed;
    [SerializeField] GameObject[] aberrations;
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject cam;

    private void Start()
    {
        foreach(GameObject eyes in aberrations)
        {

        }

        cam = GameObject.Find("Main Camera");
    }

    private void Update()
    {

        if (timer <= 0)
        {
            timer = 0;
        }

        if (inTheDark)
        {
            timer += Time.deltaTime;
            
            spawnSpeed = 5 - (int)timer;

            InvokeRepeating("SpawningAberrations", 1, spawnSpeed);
        }
        else
        {
            CancelInvoke();
        }
        if(timer >= 5)
        {
            GameBehaviour.instance.nextState = GameStates.GAMEOVER;
        }
    }

    void SpawningAberrations()
    {
        if (inTheDark)
        {
            eyes = Instantiate(aberrations[Random.Range(0, aberrations.Length)],
                new Vector3(Random.Range(cam.transform.position.x - 20, cam.transform.position.x + 20),
                            Random.Range(cam.transform.position.y - 10, cam.transform.position.x + 10), -.5f),
                            Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            inTheDark = true;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTheDark = false;

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
    }
}
