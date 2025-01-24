using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    private Camera cam;
    private float _screenWidthUnits;
    private float _screenHeightUnits;
    
    private float minX;
    private float maxX;

    private float minY;
    private float maxY;

    private float camCenterX;
    private float camCenterY;

    private Vector3 camPosition;

    [SerializeField] GameObject ballArea;
    private EdgeCollider2D edgeCollider;


    //GETTERS & SETTERS

    //CAM MIN
    public float MinX
    {
        get { return minX; }
        set { minX = value; }
    }


    //CAM MAX
    public float MaxX
    {
        get { return maxX; }
        set { maxX = value; }
    }


    public float MinY
    {
        get { return minY; }
        set { minY = value; }
    }


    public float MaxY
    {
        get { return maxY; }
        set { maxY = value; }
    }


    public float CamCenterX
    {
        get { return camCenterX; }
        set { camCenterX = value; }
    }

    public float CamCenterY
    {
        get { return camCenterY; }
        set { camCenterY = value; }
    }


    //CAM HEIGHT UNITS
    public float screenHeightUnits
    {
        get { return _screenHeightUnits; }
        set { _screenHeightUnits = value; }
    }


    //CAM WIDTH UNITS
    public float screenWidthUnits
    {
        get { return _screenWidthUnits; }
        set { _screenWidthUnits = value; }
    }


    
    private void Awake(){
        cam = Camera.main;
        camPosition = cam.transform.position;


        edgeCollider = ballArea.GetComponent<EdgeCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        setupPaddlePlace();
        setupBallArea();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setupPaddlePlace() 
    {
        _screenHeightUnits = 2f * cam.orthographicSize;
        _screenWidthUnits = screenHeightUnits * cam.aspect;

        minX = camPosition.x - _screenWidthUnits / 2 + 0.5f;
        maxX = camPosition.x + _screenWidthUnits / 2 - 0.5f;

        minY = camPosition.y + _screenHeightUnits / 2;
        maxY = camPosition.y;

        camCenterX = camPosition.x;
        camCenterY = camPosition.y;

        //Debug.Log("SCREEN: " + _screenWidthUnits);
        //Debug.Log("min: " + minY + " max: " + maxY);
    }




    private void setupBallArea()
    {
        Vector2[] colliderPoints = new Vector2[4];
        colliderPoints[0] = new Vector2(camPosition.x - _screenWidthUnits / 2, camPosition.y - _screenHeightUnits / 2); // ľavý dolný roh kamery
        colliderPoints[1] = new Vector2(camPosition.x - _screenWidthUnits / 2, camPosition.y + _screenHeightUnits / 2); //ľavý horný roh kamery
        colliderPoints[2] = new Vector2(camPosition.x + _screenWidthUnits / 2, camPosition.y + _screenHeightUnits / 2); //pravý horný roh kamery
        colliderPoints[3] = new Vector2(camPosition.x + _screenWidthUnits / 2, camPosition.y - _screenHeightUnits / 2); //pravý dolný roh kamery

        edgeCollider.points = colliderPoints;
    }   
}
