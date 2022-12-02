using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIClass;

public class PlayerUIManagerKnight : PlayerUIManager
{
    public void StartUIPoints()
    {
        lifeBar = GameObject.FindWithTag("PlayerRedBar").GetComponent<Image>();
        manaBar = GameObject.FindWithTag("PlayerBlueBar").GetComponent<Image>();
    }
}
