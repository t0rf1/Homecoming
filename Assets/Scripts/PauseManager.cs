using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    bool isGamePaused;
    public KeyCode pauseGame = KeyCode.Escape;


    public Scene currentScene;

    int lastScene = 0;

   [SerializeField] GameObject GameUi;
    [Header("main panels")]

    public GameObject pausePanel;

    //PlayerState playerState;
    //FakeLoading fakeLoading;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        //SceneChecker(currentScene.buildIndex);

        //Limit FPS
        //QualitySettings.vSyncCount = 0; 
        //Application.targetFrameRate = 60;
    }
    void Update()
    {

        if (Input.GetButtonDown("pauseGame"))
        {
           
            if (isGamePaused == true)
            {
                UnpauseGame();
                
            }
            else
            {
                PauseGame();
            }
           
        }
        //if (currentScene.buildIndex == 0)
        //{
        //    isGamePaused = false;
        //    Time.timeScale = 1;
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
    }

    public void LoadLastScene()
    {
        SceneManager.LoadScene(lastScene);
    }
    public void OnChangeScene(int SceneId)
    {
        lastScene = currentScene.buildIndex;
        SceneManager.LoadScene(SceneId);

    }
    //public void ChangeSceneWithLoadingScreen(int SceneId)
    //{
    //    loadingScreen.SetActive(true);
    //    fakeLoading = GetComponentInChildren<FakeLoading>();
    //    fakeLoading.StartLoading(SceneId);
    //}

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        
        isGamePaused = true;
        GameUi.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        GameUi.SetActive(true);
        pausePanel.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void SetTimeScale(float TimeScale)
    {
        Time.timeScale = TimeScale;
    }

    //void SceneChecker(int level)
    //{
    //    switch (level)
    //    {
    //        case 0:
    //            // code block
    //            break;
    //        case 2:
    //            Cursor.lockState = CursorLockMode.None;
    //            Cursor.visible = true;

                

    //            break;
    //        default:
    //            Cursor.lockState = CursorLockMode.Locked;
    //            Cursor.visible = false;

    //            break;
    //    }
    //}
    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene();
   
        //SceneChecker(level);

    }
}
