using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIClass;

public class PlayerUIManagerNinja : PlayerUIManager
{
    [SerializeField] private Image kunaiCounter;
    [SerializeField] private Text kunaiTxt;

    private Image kunaiBorder;

    public void StartUIPoints()
    {
        lifeBar = GameObject.FindWithTag("PlayerRedBar").GetComponent<Image>();
        manaBar = GameObject.FindWithTag("PlayerBlueBar").GetComponent<Image>();
        kunaiCounter = GameObject.Find("GreenImg").GetComponent<Image>();
        kunaiTxt = GameObject.Find("KunaiCounter").GetComponent<Text>();

        kunaiBorder = GameObject.Find("RedImg").GetComponent<Image>();
    }

    public void CountingUpdate()
    {
        Ninja ninja = GetComponent<Ninja>();

        kunaiTxt.text = ninja.currentShuriken.ToString();
        kunaiCounter.GetComponent<Image>().fillAmount = (GetComponent<Ninja>().counter * 2) / 10;

        if (kunaiCounter.fillAmount < 1)
        {
            kunaiBorder.color = Color.green;
            kunaiCounter.color = new Color(1, 0, 0, kunaiCounter.fillAmount + .1f);
        }
        else
        {
            kunaiBorder.color = Color.red;
            kunaiCounter.color = Color.green;
        }
    }
}
