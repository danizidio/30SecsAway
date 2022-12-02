using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] GameObject mainPanel, charPanel, skillPanel;

    private void Start()
    {
        mainPanel.SetActive(false);
    }
    public void OpenStore()
    {
        mainPanel.SetActive(true);
        charPanel.SetActive(false);
        skillPanel.SetActive(false);
    }

    public void CloseStore()
    {
        mainPanel.SetActive(false);
    }

    public void OpenCharPanel()
    {
        charPanel.SetActive(true);
        skillPanel.SetActive(false);
    }

    public void OpenSkillPanel()
    {
        charPanel.SetActive(false);
        skillPanel.SetActive(true);
    }

    public void CharacterToChoose(int number)
    {
        CoinsHighScoreSave.instance.SelectCharacter(number);
    }
}
