using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    [SerializeField] float timer;
	void Start ()
    {
        StartCoroutine(DestroyItem());
	}
	
IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(timer);

        Destroy(this.gameObject);
    }
}
