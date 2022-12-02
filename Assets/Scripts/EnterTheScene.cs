using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterTheScene : MonoBehaviour
{
    MainMenu mainMenu;
    [SerializeField] int scene;
    [SerializeField] GameObject panelMaster;

    private void Start()
    {
        mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();
    }
    public void LoadingScene()
    {
        StartCoroutine(LoadingNextScene());
    }

    public void ChoosingType(int typeNumber)
    {
        switch (typeNumber)
        {
            case 1:

                panelMaster.GetComponent<Animator>().SetBool("showboss", true);
                panelMaster.GetComponent<Animator>().SetBool("showchest", false);
                panelMaster.GetComponent<Animator>().SetBool("showrun", false);
                panelMaster.GetComponent<Animator>().SetBool("showflame", false);

                GameObject.Find("BossBtn1").GetComponent<Button>().Select();
                mainMenu.exitMenu = true;
                break;


            case 2:

                panelMaster.GetComponent<Animator>().SetBool("showboss", false);
                panelMaster.GetComponent<Animator>().SetBool("showchest", true);
                panelMaster.GetComponent<Animator>().SetBool("showrun", false);
                panelMaster.GetComponent<Animator>().SetBool("showflame", false);

                GameObject.Find("ChestBtn1").GetComponent<Button>().Select();
                mainMenu.exitMenu = true;
                break;

            case 3:

                panelMaster.GetComponent<Animator>().SetBool("showboss", false);
                panelMaster.GetComponent<Animator>().SetBool("showchest", false);
                panelMaster.GetComponent<Animator>().SetBool("showrun", true);
                panelMaster.GetComponent<Animator>().SetBool("showflame", false);

                GameObject.Find("RunBtn1").GetComponent<Button>().Select();
                mainMenu.exitMenu = true;
                break;

            case 4:

                panelMaster.GetComponent<Animator>().SetBool("showboss", false);
                panelMaster.GetComponent<Animator>().SetBool("showchest", false);
                panelMaster.GetComponent<Animator>().SetBool("showrun", false);
                panelMaster.GetComponent<Animator>().SetBool("showflame", true);

                GameObject.Find("FlameBtn1").GetComponent<Button>().Select();
                mainMenu.exitMenu = true;
                break;
        }
    }

    public void ClosingTypes()
    {
        panelMaster.GetComponent<Animator>().SetBool("showboss", false);
        panelMaster.GetComponent<Animator>().SetBool("showchest", false);
        panelMaster.GetComponent<Animator>().SetBool("showrun", false);
        panelMaster.GetComponent<Animator>().SetBool("showflame", false);

        GameObject.Find("BossStages").GetComponent<Button>().Select();
        mainMenu.exitMenu = false;
    }

    IEnumerator LoadingNextScene()
    {

        AsyncOperation loading = SceneManager.LoadSceneAsync(scene);

        while (!loading.isDone)
        {
            mainMenu.circleLoader.SetActive(true);
            yield return null;
        }
    }
}
