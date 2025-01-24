using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Game/GameData")]
public class GameData : ScriptableObject
{

    public int totalScore = 0;
    private int startLives = 3;
    private int currentLives;
    private int amountOfBalls;

    public int currentStars;
    public int currentScore;
    public int currentLevel;
    public bool saveFileExist = false;

    
    /////////////////////////////////////////
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    public int CurrentTotalScore
    {
        get { return currentScore; }
        set { currentScore = value; }
    }

    public int CurrentStars
    {
        get { return currentStars; }
        set { currentStars = value; }
    }

    public bool SaveFileExist
    {
        get { return saveFileExist; }
        set { saveFileExist = value; }
    }
    /////////////////////////////////////////

    public int CurrentHealth
    {
        get { return currentLives; }
        set { currentLives = value; }
    }

    public int AmountOfBalls
    {
        get { return amountOfBalls; }
        set { amountOfBalls = value; }
    }


    public int AddToScore(int points)
    {
        totalScore += points;

        return totalScore;
    }

    public void ResetScore()
    {
        totalScore = 0;
    }

    public void SetupHealth()
    {
        currentLives = startLives;
    }

    public bool DecreaseLives()
    {
        if(currentLives > 1)
        {
            currentLives = currentLives - 1;
            return true;
        }
        else
        {
            currentLives = currentLives - 1;
            return false;
        }
    }


}
