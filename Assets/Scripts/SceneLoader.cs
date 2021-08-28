using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void LoadStart()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<Game>().SetScore();
        Cursor.visible = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public int CurrentSceneBuildIndex()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        return sceneIndex;
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(CurrentSceneBuildIndex());
    }
}
