using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{   
    
    [SerializeField] PaddleController  paddle1;
    private Rigidbody2D rb;

    [Header("Effects")]
    private TrailRenderer trailRenderer;
    private GameObject ballParticle;
    [SerializeField] GameObject ballVFX;

    private GameStatus theGameStatus;



    private Vector2 paddleToBallVector; //GAP

    [SerializeField] float xPush = 2;
    [SerializeField] float yPush = 15;



    [Header("Ball Physics")]
    [SerializeField] int maxBounces = 2;
    private int bounceCounter;
    [SerializeField] float anglePunch = 30f;  
    [SerializeField] float constantSpeed = 10f;

    private bool hasStarted = false;

    [Header("Audio")]
    private new AudioSource audio;
    [SerializeField] AudioClip startEffect;
    [SerializeField] AudioClip bounceEffect;
    //private bool startSound = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle1.transform.position;
        theGameStatus = FindObjectOfType<GameStatus>();

        audio = GetComponent<AudioSource>();

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
        ballParticle = Instantiate(ballVFX, transform.position, transform.rotation);
        ballParticle.transform.SetParent(transform);
        ballParticle.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (rb != null && theGameStatus != null && !theGameStatus.IsPaused)
        {
            if (!hasStarted)
            {
                LockBallToPaddle();
                LaunchOnMouseClick();
            }

            rb.velocity = rb.velocity.normalized * constantSpeed; // constant speed fix
        }
        else
        {
            if (rb == null) Debug.LogError("Rigidbody2D is not assigned!");
            if (theGameStatus == null) Debug.LogError("GameStatus is not assigned!");
        }
    }

    public void ResetBall()
    {
        hasStarted = false;
        ballParticle.SetActive(false);
        trailRenderer.enabled = false;
        LockBallToPaddle();
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            hasStarted = true;
            audio.clip = startEffect;
            audio.Play();
            trailRenderer.enabled = true; 
            ballParticle.SetActive(true); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "BallAreaCollider" || collision.gameObject.tag == "BreakableBlock" || collision.gameObject.tag == "Block" || collision.gameObject.tag == "Paddle")
        {
            bounceCounter++;
            audio.clip = bounceEffect;
            audio.Play();
            if (bounceCounter >= maxBounces)
            {
                Vector2 currentVelocity = rb.velocity;
                float currentAngle = Mathf.Atan2(currentVelocity.y, currentVelocity.x) * Mathf.Rad2Deg;

                float newAngle = currentAngle + anglePunch;
                Vector2 newDirection = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
                rb.velocity = newDirection.normalized * currentVelocity.magnitude;

                bounceCounter = 0;

            }
        }
        
    }
}
