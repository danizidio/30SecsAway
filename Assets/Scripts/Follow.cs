using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform target;
    public float targety;
    public float targetx;
    public float targetz;

    void Update()
    {
        transform.position = new Vector3(target.position.x + targetx, target.position.y + targety, targetz);
        transform.eulerAngles = new Vector2(0, 0);
    }
}
