using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDagger : Dagger {

    [SerializeField] GameObject shadowNinja;
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpecialDagger();
        StartCoroutine(TimeToDie());
	}
}
