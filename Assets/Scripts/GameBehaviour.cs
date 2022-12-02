using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameStates
{
    START,
    PAUSE,
    GAMEOVER,
    TIMEOVER,
    STAGECLEAR
}

public class GameBehaviour : MonoBehaviour
{

    public static GameBehaviour instance;
    #region - Variables -

    [SerializeField] GameObject[] heroes;
    [SerializeField] GameObject hero;

    [SerializeField] Transform spawnPoint;

    private float ads = 0;
    private float i = 0;
    private float b = 0;

    [SerializeField] bool hasBoss, _superAttack;
    [SerializeField] int _coin;

    Scene scene;
   [SerializeField] string sceneName;

    public int hiScore;

    public AudioClip music;

    GameObject data;

    public GameStates state;
    public GameStates nextState;

    [SerializeField] private bool _itsOver, _bossSpawn;

    [SerializeField] int _enemiesKilled, _maxToKill;

    public int enemiesKilled { get { return _enemiesKilled; } set { _enemiesKilled = value; } }
    public int maxToKill { get { return _maxToKill; } set { _maxToKill = value; } }
    public bool itsOver { get { return _itsOver; } set { _itsOver = value; } }
    public bool bossSpawn { get { return _bossSpawn; } set { _bossSpawn = value; } }

    public int coin { get { return _coin; } set { _coin = value; } }

    public bool superAttack
    {
        get
        {
            return _superAttack;
        }

        set
        {
            _superAttack = value;
        }
    }

    #endregion

    void Awake()
    {
        Application.targetFrameRate = 60;

        instance = this;

        hero = Instantiate(CoinsHighScoreSave.instance.playerSelect, new Vector2(spawnPoint.position.x, spawnPoint.position.y), Quaternion.identity);
    }

    void Start()
    {
        Time.timeScale = 1;
        
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        enemiesKilled = 0;

        GameObject.Find("BossBattle").GetComponent<Animator>().SetBool("BossBattle", false);
        data = GameObject.FindGameObjectWithTag("DATA");
        data.GetComponent<CoinsHighScoreSave>().LoadingGame();
    }

    void Update()
    {
        state = nextState;

        switch (state)
        {
            case GameStates.START:
                {
                    GameObject.Find("PauseMenu").GetComponent<Animator>().SetBool("pause", false);
                    GameObject.Find("GameOver").GetComponent<Animator>().SetBool("GameOver", false);
                    GameObject.Find("StageClear").GetComponent<Animator>().SetBool("StageClear", false);
                    GameObject.Find("TimeIsOver").GetComponent<Animator>().SetBool("EndTime", false);
                    GameObject.Find("CanvasOver").GetComponent<Animator>().SetBool("Over", false);

                    Cronometro.instance.relogio = true;

                    if (superAttack)
                    {
                        Time.timeScale = 0;
                    }
                    else
                    {
                        Time.timeScale = 1;
                    }

                    if (enemiesKilled >= maxToKill && hasBoss)
                    {
                        bossSpawn = true;
                    }

                    if (bossSpawn)
                    {
                        GameObject.Find("BossBattle").GetComponent<Animator>().SetBool("BossBattle", true);
                    }

                    if (itsOver)
                    {
                        nextState = GameStates.GAMEOVER;
                    }

                    if (Cronometro.instance.timeIsOver)
                    {
                        nextState = GameStates.TIMEOVER;
                    }

                    break;
                }

            case GameStates.PAUSE:
                {
                    GameObject.Find("PauseMenu").GetComponent<Animator>().SetBool("pause", true);
                    Time.timeScale = 0;
                    Cronometro.instance.relogio = false;
                    break;
                }

            case GameStates.GAMEOVER:
                {
                    Time.timeScale = 0;
                    GameObject.Find("GameOver").GetComponent<Animator>().SetBool("GameOver", true);
                    hero.GetComponent<PlayerSave>().LosingCharacter();
                    LevelManagerSave.instance.DeleteSaveManager(hero);
                    GameObject.Find("CanvasOver").GetComponent<Animator>().SetBool("Over", true);
                    GameObject.Find("NextStage").GetComponent<Button>().interactable = false;
                    Cronometro.instance.relogio = false;

                    break;
                }

            case GameStates.TIMEOVER:
                {
                    Time.timeScale = 0;
                    int a = 1;
                    GameObject.Find("TimeIsOver").GetComponent<Animator>().SetBool("EndTime", true);
                    GameObject.Find("NextStage").GetComponent<Button>().interactable = false;
                    GameObject.Find("CanvasOver").GetComponent<Animator>().SetBool("Over", true);
                    hero.GetComponent<PlayerSave>().SavingPlayer();
                    LevelManagerSave.instance.SavingManager(hero);
                    if (a == 1)
                    {
                        data.GetComponent<CoinsHighScoreSave>().SavingGame(coin);

                        a = 2;
                    }

                    Cronometro.instance.relogio = false;

                    break;
                }
            case GameStates.STAGECLEAR:
                {
                    LevelUpManager.instance.canSave = true;
                    hero.GetComponent<PlayerSave>().SavingPlayer();
                    LevelManagerSave.instance.SavingManager(hero);
                    int a = 1;
                    Time.timeScale = 0;
                    GameObject.Find("StageClear").GetComponent<Animator>().SetBool("StageClear", true);

                    if (a == 1)
                    {
                        data.GetComponent<CoinsHighScoreSave>().SavingGame(coin);
                        a = 2;
                    }

                    Cronometro.instance.relogio = false;
                    GameObject.Find("NextStage").GetComponent<Button>().interactable = true;
                    GameObject.Find("CanvasOver").GetComponent<Animator>().SetBool("Over", true);

                    break;
                }
        }
    }

    //Graphic User Interface for pause menu
    public GameStates GetCurrentState()
    {
        return state;
    }

    public void ChangeState(GameStates newState)
    {
        nextState = newState;
    }

    public void CancelChoose()
    {

    }
    public void CloseOperation()
    {


    }
    public void CloseError()
    {


    }
    public void ContinueMatch()
    {

    }

    public void WillSpendCoin(int vlr)
    {
        if (ManagementPrefs.instance.coins >= vlr)
        {
            ManagementPrefs.instance.coins -= vlr;

            //   SpendingCoins(vlr);

            CloseOperation();

            nextState = GameStates.START;
        }
        else
        {
            CancelChoose();
        }
    }

    public void ReturnMatch()
    {
        if (state == GameStates.PAUSE)
        {
            hero.GetComponent<PlayerSave>().SavingPlayer();

            nextState = GameStates.START;
        }
    }

    public bool ItsOver()
    {
        return itsOver;
    }

    public void GameOver()
    {

    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NextStage()
    {
        StartCoroutine(LoadingNextScene());
    }

    public void ButtonToPause()
    {
        if (state == GameStates.START)
        {
            nextState = GameStates.PAUSE;
        }
        else if (state == GameStates.PAUSE)
        {
            nextState = GameStates.START;
        }
    }

    IEnumerator LoadingNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int indexScene = scene.buildIndex;

        int randomScene = Random.Range(1, SceneManager.sceneCountInBuildSettings);

        AsyncOperation loading = SceneManager.LoadSceneAsync(randomScene);

        print("buscando!");
        if (randomScene != indexScene)
        {
            while (!loading.isDone)
            {
                GameObject.Find("LoadText").GetComponent<Text>().text = "Loading...";
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
