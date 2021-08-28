using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;

    [SerializeField] int score = 0;

    [SerializeField] Text scoreText;

    [SerializeField] bool autoplay = false;


    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject lastMainMenuCanvas;
    [SerializeField] GameObject lastQuitGameCanvas;

    float startGameSpeed = 1f;
    bool pauseOn = false;

    SceneLoader sceneLoader;
    ShowMaxScore showNewScore;

    private void Awake()
    {
        int gameCount = FindObjectsOfType<Game>().Length;
        if(gameCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();   
        ShowingScore();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        ShowMaxScore();
        Pause();
    }

    public void Score()
    {
        score = score + 12;
        ShowingScore();
    }

    public void ShowingScore()
    {
        scoreText.text = score.ToString();
    }

    public void SetScore()
    {
        Destroy(gameObject);
    }

    public bool AutoPlayEnable()
    {
        return autoplay;
    }

    void ShowMaxScore()
    {
        if(FindObjectOfType<SceneLoader>().CurrentSceneBuildIndex() != 5)
        {
            return;
        }
        else
        {
            scoreText.gameObject.SetActive(false);
            FindObjectOfType<ShowMaxScore>().GetComponent<Text>().text = score.ToString();
        }
    }

    void Pause()
    {
        if(Input.GetButtonDown("Cancel") && FindObjectOfType<SceneLoader>().CurrentSceneBuildIndex() != 0 && FindObjectOfType<SceneLoader>().CurrentSceneBuildIndex() != 5 && !pauseOn)
        {
            gameSpeed = 0f;
            pauseCanvas.gameObject.SetActive(true);
            Cursor.visible = true;
            pauseOn = true;
        }
        else if(Input.GetButtonDown("Cancel") && pauseOn)
        {
            pauseOn = false;
            pauseCanvas.gameObject.SetActive(false);
            gameSpeed = startGameSpeed;
            Cursor.visible = false;
        }
    }

    public void ResumeGame()
    {
        pauseOn = false;
        pauseCanvas.gameObject.SetActive(false);
        gameSpeed = startGameSpeed;
        Cursor.visible = false;
    }

    public bool GetOnPause()
    {
        return pauseOn;
    }

    public void LoadMainMenu()
    {
        FindObjectOfType<SceneLoader>().LoadStart();
    }

    public void ShowGameOverMenu()
    {
        gameSpeed = 0f;
        pauseOn = true;
        gameOverMenu.SetActive(true);
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        gameSpeed = startGameSpeed;
        gameOverMenu.SetActive(false);
        Cursor.visible = false;
        score = 0;
        ShowingScore();
        pauseOn = false;
        FindObjectOfType<SceneLoader>().LoadCurrentScene();
    }

    public void QuitGame()
    {
        FindObjectOfType<SceneLoader>().Quit();
    }

    public void ShowLastMainMenuCanvas()
    {
        lastMainMenuCanvas.SetActive(true);
    }

    public void ShowLastQuitGameCanvas()
    {
        lastQuitGameCanvas.SetActive(true);
    }
    public void NoButton()
    {
        if(lastMainMenuCanvas.activeInHierarchy)
        {
            lastMainMenuCanvas.SetActive(false);
        }
        else if(lastQuitGameCanvas.activeInHierarchy)
        {
            lastQuitGameCanvas.SetActive(false);
        }
    }
}
