using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    private float randomValue;

	void Start ()
    {

        WhereToGo();

    }

    void WhereToGo()
    {

        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), Random.Range(15, 30)), ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetBool("tookIt", true);
            this.GetComponent<CircleCollider2D>().isTrigger = true;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static ;
            StartCoroutine(TimeToDie());  
        }
    }

    IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(1);

        Destroy(this.gameObject);
    }
}
