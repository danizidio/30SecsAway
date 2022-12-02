using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollUVRect : MonoBehaviour
{
    RawImage raw;
    [SerializeField] float timer;

    private void Start()
    {
        raw = GetComponent<RawImage>();
    }

    private void Update()
    {
        raw.GetComponent<RawImage>().uvRect = new Rect(timer * Time.time, 0, 8, 1);
    }

}
