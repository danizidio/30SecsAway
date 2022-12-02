using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingItens : MonoBehaviour {

    float a, b, c;
    float timer1 = 1, timer2 = 2, timer3;
    Transform player;
    private bool hopping = false;
    string objName;

    [SerializeField] GameObject xplosion;

    void Start()
    {
        a = 15;
        b = 30;
        c = Random.Range(a, b);
        timer3 = Random.Range(timer1, timer2);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Hop(new Vector2(player.position.x, player.position.y), timer3));
        objName = gameObject.name;

        if (objName == "SantaPot" || objName == "SantaCoin")
        {
            StartCoroutine(TimeToDestroy());
        }

    }

    IEnumerator Hop(Vector3 dest, float time)
    {
        if (hopping) yield break;

        hopping = true;
        var startPos = transform.position;
        var timer = 0.0f;

        while (timer <= 1.0f)
        {
            var height = Mathf.Sin(Mathf.PI * timer) * c;
            transform.position = Vector3.Lerp(startPos, dest, timer) + Vector3.up * height;

            timer += Time.deltaTime / time;
            yield return null;
        }
        hopping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !(objName == "SantaPot" || objName == "SantaCoin"))
        {
            //  collision.collider.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-100, 100), ForceMode2D.Force);
            
            //collision.GetComponent<AttackBase>().TakingDamage(collision.gameObject, GetComponent<EnemyBase>().enemyDamage);

            Component damageable = collision.gameObject.GetComponent(typeof(IPlayerCombat));
            if (damageable)
            {
                (damageable as IPlayerCombat).EnemyHitPlayer(10, 
                        collision.gameObject.GetComponent<Atributes>().playerArmor, 
                        collision.gameObject.GetComponent<Atributes>().currentHealth);

            }

            collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-20, 20), ForceMode2D.Impulse);

            Instantiate(xplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Ground") && !(objName == "SantaPot" || objName == "SantaCoin"))
        {
            Instantiate(xplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(10);

        Destroy(gameObject);
    }
}
