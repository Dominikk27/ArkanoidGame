using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    
   
    [SerializeField] int breakableBlocksCount;


    [SerializeField] int ballsCounter;
    private GameObject[] ballsArray;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject paddle;


    private GameStatus gameStatus;
    [SerializeField] GameData gameData;
    private SceneLoader sceneLoader;

    [SerializeField] private int oneStar_score;
    [SerializeField] private int threeStar_score;

    public void CountBreakableBlocks()
    {
        GameObject[] breakableObjects = GameObject.FindGameObjectsWithTag("BreakableBlock");
        breakableBlocksCount = breakableObjects.Length;
        //Debug.Log("[BreakableBlocks count]: " + breakableBlocks);

    }


    public void SpawnBalls(int amountOfBalls)
    {
        //Debug.Log("Ball Spawner!");
        ballsCounter = gameStatus.AmountOfBalls;   
        ballsArray = new GameObject[amountOfBalls];
        for (int i = 0; i < amountOfBalls; i++)
        {
            float offsetX = i * 0.2f;
            float offsetY = 0.2f;
            Vector3 spawnPosition = new Vector3(paddle.transform.position.x + offsetX, paddle.transform.position.y + offsetY, 0);
            ballsArray[i] = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        }
        gameStatus.EditAmountOfBalls(true, amountOfBalls);
    }


    public void decreseBlockCount()
    {
        breakableBlocksCount--;
        gameStatus.AddToScore();
        if (breakableBlocksCount <= 0)
        {
            SaveData();
            changeLevel();
        }
    }


    private void changeLevel()
    {
        //Debug.Log("Menim level");
        sceneLoader.ChangeLevel();
    }

    private void SaveData()
    {
        int totalScore = gameStatus.TotalScore;
        int currentHealth = gameStatus.GetCurrentHealth;
        int currentLevel = sceneLoader.GetCurrentLevel;
        int currentStars;
        
        //Debug.Log($"[TotalScore]: {totalScore}");
        //Debug.Log($"[CurrentHealth]: {currentHealth}");
        //Debug.Log($"[CurrentLevel]: {currentLevel}");

        totalScore = totalScore * currentHealth;

        if (totalScore <= oneStar_score)
        {
            currentStars = 1;
        }
        else if (totalScore > oneStar_score && totalScore < threeStar_score)
        {
            currentStars = 2;
        }
        else{
            currentStars = 3;
        }

        gameData.CurrentLevel = currentLevel;
        gameData.CurrentTotalScore = totalScore;
        gameData.CurrentStars = currentStars;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameStatus = FindObjectOfType<GameStatus>();

        CountBreakableBlocks();

        if (ballPrefab == null)
        {
           // Debug.LogError("ballPrefab is null!");
            return;
        }

        ballsCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
