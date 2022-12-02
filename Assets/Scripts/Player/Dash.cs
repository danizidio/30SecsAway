using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "PowerUPDash")
        {
            PowerUpManager.instance.dashBool = true;
            Destroy(collider.gameObject);
        }
    }
}
