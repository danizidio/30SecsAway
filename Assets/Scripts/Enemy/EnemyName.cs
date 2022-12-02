using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyName : MonoBehaviour
{
    public GameObject obj;

    private void Update()
    {
        this.name = obj.GetComponent<EnemyBase>().thatName;
    }
}
