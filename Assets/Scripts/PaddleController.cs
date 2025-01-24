using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private Rigidbody2D rb; 
    //[SerializeField] private float speed = 5;
    
    
    private Camera cam;

    private CameraSetup cameraSetup;

    private GameStatus theGameStatus;
    private BallController theBall;


    // Bonus stuff
    private bool isGood;
    private float duration;
    private Vector3 defaultSize;
    private Vector3 smallSize;
    private Vector3 largeSize;

    [Header("Audio")]
    private AudioSource audioSource;
    [SerializeField] AudioClip catchEffect;




    private IEnumerator ResizePaddle(bool isGood, float duration)
    {
        float elapsedTime = 0;
        float animationTime = 2f;

        Vector3 targetSize = isGood ? largeSize : smallSize;

        // Animation
        while (elapsedTime < animationTime)
        {
            transform.localScale = Vector3.Lerp(defaultSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetSize;

        // Wait for duration time
        yield return new WaitForSeconds(duration);

        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(targetSize, defaultSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        transform.localScale = defaultSize;
    }


    public void ApplyResize(bool isGood, float duration)
    {
        this.isGood = isGood;
        this.duration = duration;

        StartCoroutine(ResizePaddle(isGood, duration));
    }



    private void OnTriggerEnter2D(Collider2D collision) 
    {
        audioSource.clip = catchEffect;
        audioSource.Play();
    }




    private float GetXPos()
    {
        if(theGameStatus.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * cameraSetup.screenWidthUnits;
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //camPosition = cam.transform.position;
        
        
        cam = Camera.main;
        cameraSetup = cam.GetComponent<CameraSetup>();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultSize = transform.localScale;
        smallSize = new Vector3(defaultSize.x / 2, defaultSize.y, defaultSize.z);
        largeSize = new Vector3(defaultSize.x * 2, defaultSize.y, defaultSize.z);

        //Debug.Log($"Default Size: {defaultSize}, Small Size: {smallSize}, Large Size: {largeSize}");
        theGameStatus = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<BallController>();

    }



    private void Update()
    {
        if(!theGameStatus.IsPaused)
        {
            float mousePosInUnits = Input.mousePosition.x / Screen.width * cameraSetup.screenWidthUnits;
            mousePosInUnits = Mathf.Clamp(mousePosInUnits, cameraSetup.MinX, cameraSetup.MaxX);

            Vector2 paddlePos = new Vector2(mousePosInUnits, transform.position.y);
            paddlePos.x = Mathf.Clamp(GetXPos(), cameraSetup.MinX, cameraSetup.MaxX);
            transform.position = paddlePos;
            

            // KEYBOARD MOVEMENT
            //float movementHorizontal = Input.GetAxis("Horizontal");
            //rb.velocity = new Vector2(movementHorizontal * speed, 1 );
        }
    }
}
