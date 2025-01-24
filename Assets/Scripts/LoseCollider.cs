using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class LoseCollider : MonoBehaviour
{    

    [SerializeField] GameStatus gameStatus;
    [SerializeField] BallController ball;


    private void Start() {
        //Debug.Log(gameStatus.IsAlive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.tag=="Ball")
        {
            //Debug.Log($"[AmountOfBalls]: {gameStatus.AmountOfBalls}");
            if(gameStatus.AmountOfBalls > 1)
            {
                gameStatus.EditAmountOfBalls(false, 1);
            }
            else{
                if(gameStatus.IsAlive)
                {
                    gameStatus.decreaseLives();
                    ball.ResetBall();
                }
            }
        }
    }
}



