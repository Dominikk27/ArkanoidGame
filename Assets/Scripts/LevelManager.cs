using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


using System.IO;


[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public int score;
    public int stars;
    public bool isUnlocked;
}

[System.Serializable]
public class GameProgress
{
    public List<LevelData> levels = new List<LevelData>();
}



public class LevelManager : MonoBehaviour
{
    private string saveFile;
    private GameProgress levelData;
    private int levelCounter;

    [SerializeField] private GridLayoutGroup levelGrid;
    [SerializeField] private GameObject lockPrefab;
    
    [SerializeField] GameData gameData;

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/gamedata.json";

        Debug.Log($"[GAMEDATA] is {gameData.SaveFileExist}");

        if(!gameData.SaveFileExist)
        {
            if(File.Exists(saveFile))
            {
                //Debug.Log("[SaveFile] exist!");
            }
            else
            {
                Debug.Log("[SaveFile] Not exist!");
                CreateDefaultFile();
            }
            gameData.SaveFileExist = true;
        }
        else{
            CompleteLevel();
        }
        LoadDetails();

        //Debug.Log(saveFile);
    }


    private void LoadDetails()
    {
        if (File.Exists(saveFile))
        {
            string json = File.ReadAllText(saveFile);
            levelData = JsonUtility.FromJson<GameProgress>(json);
            //Debug.Log("[LoadData]: Progress successfully loaded!");

        }
        else
        {
            Debug.LogWarning("[LoadData]: Save file not found. Creating default data.");
            CreateDefaultFile();
        }

        UpdateLevelButtons();
    }

    private int CountLevels()
    {
        int levelCount = levelGrid.transform.childCount;
        //Debug.Log($"[Levels]: there is  {levelCount} levels");
        return levelCount;
    }



    private void CreateDefaultFile()
    {
        int levelCounter = CountLevels();
        levelData = new GameProgress();

        for (int i = 1; i <= levelCounter; i++)
        {
            levelData.levels.Add(new LevelData
            {
                levelNumber = i,
                score = 0,
                stars = 0,
                isUnlocked = (i == 1) // Unlock first level
            });
        }

        SaveData();
    }



    private void UpdateLevelButtons()
    {
        foreach (Transform child in levelGrid.transform)
        {
            Button levelButton = child.GetComponent<Button>();
            int levelNumber = int.Parse(child.name.Replace("Level_", ""));

            LevelData level = levelData.levels.Find(l => l.levelNumber == levelNumber);
            if (level != null)
            {
                if (!level.isUnlocked)
                {
                    levelButton.interactable = false;

                    GameObject starsGrid = child.Find("Stars").gameObject;
                    starsGrid.SetActive(false);
                    
                    Transform existingLock = child.Find("LockOverlay");
                    if (existingLock == null)
                    {
                        GameObject lockInstance = Instantiate(lockPrefab, child);
                        lockInstance.name = "LockOverlay";
                    }
                }
                else
                {
                    
                    levelButton.interactable = true;

                    Transform existingLock = child.Find("LockOverlay");
                    if (existingLock != null)
                    {
                        Destroy(existingLock.gameObject);
                    }

                    GameObject starsGrid = child.Find("Stars").gameObject;
                    starsGrid.SetActive(true);

                    foreach (Transform star in starsGrid.transform)
                    {
                        star.gameObject.SetActive(false);
                    }

                    for (int i = 0; i < level.stars; i++)
                    {
                        if(i <= starsGrid.transform.childCount)
                        {
                            starsGrid.transform.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                levelButton.interactable = false;
            }
        }
    }

    private void CompleteLevel()
    {
        Debug.Log($"SAVE DATA!");

        int currentLevel = gameData.CurrentLevel;
        int totalScore = gameData.CurrentTotalScore;
        int currentStars = gameData.CurrentStars;

        string jsonData = File.ReadAllText(saveFile);
        levelData = JsonUtility.FromJson<GameProgress>(jsonData);

        LevelData currentLevelData = levelData.levels.Find(l => l.levelNumber == currentLevel);

        if (currentLevelData != null)
        {
            if(currentLevelData.score < totalScore){
                currentLevelData.score = totalScore;
                currentLevelData.stars = currentStars;
            }

            int nextLevelNumber = currentLevel + 1;
            LevelData nextLevelData = levelData.levels.Find(l => l.levelNumber == nextLevelNumber);
            if (nextLevelData != null)
            {
                nextLevelData.isUnlocked = true;
            }
        }


        SaveData();
    }

    public void ResetLevels()
    {
        CreateDefaultFile();
    }

    public void UnlockAllLevels()
    {
        foreach (var level in levelData.levels)
        {
            level.isUnlocked = true;
        }

        SaveData();
    }



    private void SaveData()
    {
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(saveFile, json);
        Debug.Log($"Progress saved to {saveFile}");

        LoadDetails();

    }

}