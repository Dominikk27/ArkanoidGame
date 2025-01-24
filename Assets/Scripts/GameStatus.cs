using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    //[Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 2;
    [SerializeField] int amountOfBalls;


    [SerializeField] GameData gameData;

    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI score;
    
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;


    private bool isAlive = true;
    private bool isPaused = false;
    private int totalScore = 0;
    private int currentHealth;


    [SerializeField] bool isAutoPlayEnabled;


    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public bool IsPaused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }

    public bool IsAlive
    {
        get { return isAlive; } 
        set { isAlive = value; }
    }

    public int AmountOfBalls
    {
        get { return amountOfBalls; }
        set { amountOfBalls = value; }
    }

    public int TotalScore
    {
        get { return totalScore; }
        set { totalScore = value; }
    }

    public int GetCurrentHealth
    {
        get { return gameData.CurrentHealth; }
    }





    public void AddToScore()
    {
        totalScore = gameData.AddToScore(pointsPerBlockDestroyed);
        UpdateScoreUI();
    }

    public void EditAmountOfBalls(bool increase, int amount)
    {
        if(!increase)
        {
            amountOfBalls = amountOfBalls - amount;
        }
        else{
            amountOfBalls = amountOfBalls + amount;
        }

        gameData.AmountOfBalls = amountOfBalls;
    }
    


    public bool decreaseLives()
    {
        isAlive = gameData.DecreaseLives();
        UpdateScoreUI();

        //Debug.Log(IsAlive);
        return isAlive;
    }

    private void UpdateScoreUI()
    {
        currentHealth = gameData.CurrentHealth;
        health.text = currentHealth.ToString();
        score.text = gameData.totalScore.ToString();
    }


    private void SetupUI()
    {
        gameData.ResetScore();
        gameData.SetupHealth();
        UpdateScoreUI();
    }

    public void PauseMenu()
    {
        if(!isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else{
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    private void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        //IsAlive;
        SetupUI();
        UpdateScoreUI();
        amountOfBalls = 1;
        Time.timeScale = 1;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused)
        {
            //Time.timeScale = gameSpeed;
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu();
            }
        }
        else{
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu();
            }
        }

        if(!isAlive)
        {
            ShowGameOverMenu();
        }

    }
}