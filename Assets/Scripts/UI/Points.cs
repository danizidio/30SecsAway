using UnityEngine;


public class Points : MonoBehaviour {

    public int points = 0;

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "GivePoints")
                points++; 
       }
    }

