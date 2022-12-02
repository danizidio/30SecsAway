using UnityEngine;
using System.Collections;
using UIClass;
public class Potion : MonoBehaviour
{
    [SerializeField] bool healthPot, manaPot;
    [SerializeField] int amount;
    [SerializeField] string typePot;

    private void Start()
    {
        GetComponent<Animator>().Play(typePot);
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject e = collider.gameObject;

        if (e.tag == "Player")
        {
            if (healthPot)
            {
               e.GetComponent<PlayerUIManager>().EarnLife(amount);
            }

            if(manaPot)
            {
                e.GetComponent<PlayerUIManager>().EarnMana(amount);
            }

            Destroy(this.gameObject);
        }
    }

}

