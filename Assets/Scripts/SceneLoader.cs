using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    private GameStatus gameStatus;

    [SerializeField] private GameObject levelMenu;
    private bool levelMenu_isActive = false;
    private string sceneName;
    private int currentLevel;

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void LoadLevel(int LevelID)
    {
        string levelName = "Level_" + LevelID;
        SceneManager.LoadScene(levelName);
    }

    public void DisplayLevelMenu()
    {
        if(!levelMenu_isActive)
        {
            levelMenu.SetActive(true);
            levelMenu_isActive = true;
        }
        else
        {
            levelMenu.SetActive(false);
            levelMenu_isActive = false;
        }
    }

    public int GetCurrentLevel
    {
        get { 
            sceneName = SceneManager.GetActiveScene().name;
            currentLevel = ExtractLevelNumber(sceneName);
            return currentLevel; }
        set { currentLevel = value; }
    }


    public void ChangeLevel()
    {
        
        sceneName = SceneManager.GetActiveScene().name;

       
        currentLevel = ExtractLevelNumber(sceneName);

        if (currentLevel >= 0)
        {
            int nextLevel = currentLevel + 1;

            string nextSceneName = "Level_" + nextLevel;
            //Debug.Log($"Current Scene: {sceneName}");
            //Debug.Log($"Next Scene: {nextSceneName}");
            if (SceneExist(nextSceneName))
            {
                
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                //Debug.LogWarning($"[SceneLoader] {nextSceneName} not found. Returning to MainMenu.");
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private bool SceneExist(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string scene = System.IO.Path.GetFileNameWithoutExtension(path);
            if (scene == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private int ExtractLevelNumber(string sceneName)
    {
        string[] chars = sceneName.Split('_');
        if (chars.Length > 1 && int.TryParse(chars[1], out int levelNumber))
        {
            return levelNumber;
        }
        return -1;
    }



    public void LoadStartScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
