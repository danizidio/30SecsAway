using UnityEngine;
using System.Collections;

public class PuloDuplo : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "PowerUPJump")
        {
            PowerUpManager.instance.puloDuploBool = true;
            Destroy(collider.gameObject);
        }
    }
}