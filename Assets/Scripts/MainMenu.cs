using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script for Main Menu

public class MainMenu : ManagementPrefs
{
    EnterTheScene enterTheScene;

    public GameObject circleLoader;
    [SerializeField] Text textNinjaLevel, textCoins;

    GameObject datas;

    Scene scene;
    [SerializeField] int indexScene, _qntCoins;

    [SerializeField]
    bool show, _exitMenu;

    public int qntCoins { get { return _qntCoins; } set { _qntCoins = value;} }

    public bool exitMenu { get { return _exitMenu; } set { _exitMenu = value; } }

    private void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60;
    }
    void Start()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        circleLoader.SetActive(false);

        enterTheScene = GetComponent<EnterTheScene>();

        scene = SceneManager.GetActiveScene();
        indexScene = scene.buildIndex;

        qntCoins = CoinsHighScoreSave.instance.coins;

    }

    void Update()
    {
        textCoins.GetComponent<Text>().text = "Coins: " + qntCoins;

        if (Input.GetButton("Cancel") && exitMenu)
        {
            enterTheScene.ClosingTypes();
        }

        else if (Input.GetButton("Cancel") && (!exitMenu && show))
        {
            show = false;
            StageSelect();
            
        }
    }

    public void StageSelect()
    {
        show = !show;
        GameObject.Find("PanelStage").GetComponent<Animator>().SetBool("show", show);

        if (show)
        {
            GameObject.Find("BossStages").GetComponent<Button>().Select();
        }

        if(!show)
        {
            GameObject.Find("Start").GetComponent<Button>().Select();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(LoadingNextScene());
    }

    //Co routine to load next scene before launch
    IEnumerator LoadingNextScene()
    {
        int randomScene = Random.Range(1, SceneManager.sceneCountInBuildSettings);
        AsyncOperation loading = SceneManager.LoadSceneAsync(randomScene);

        if (randomScene != indexScene)
        {
            while (!loading.isDone)
            {
                circleLoader.SetActive(true);
                yield return null;
            }
        }
        else
        {
            print("falhou!");
            StartCoroutine(LoadingNextScene());
        }
    }

}
