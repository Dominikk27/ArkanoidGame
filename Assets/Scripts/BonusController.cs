using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] bonusIcon;

    [Header("Main")]
    [SerializeField] string bonusName;
    private int currentSpriteIndex;
    [SerializeField] private int applyBonus;

    [Header("Others")]
    [SerializeField] float gravity = -0.5f; 
    [SerializeField] Vector3 velocity;
    [SerializeField] bool isCollected = false; 



    public void SetBonusDetails(string name, int bonusApplyFor, int spriteIndex)
    {
        this.bonusName = name;
        this.applyBonus = bonusApplyFor;
        this.currentSpriteIndex = spriteIndex;

        //Debug.Log($"[BonusController]: SpriteIcons: {bonusIcon.Length} SpriteIndex: {currentSpriteIndex} Apply for: {applyBonus});


        SetSprite(currentSpriteIndex);

    }

    private void SetSprite(int currentIndex)
    {

        if (spriteRenderer != null && bonusIcon.Length > 0)
        {
            spriteRenderer.sprite = bonusIcon[currentIndex]; 
            //Debug.Log("[BonusController] SpriteIndex: " + currentIndex);
        }
        else
        {
            //Debug.LogWarning("Error SpriteRenderer");
        }
    }
    
    
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "LoseCollider"){
            //Debug.Log("DESTORY BONUS");
            Destroy(gameObject);
        }
        else if(collider.tag == "Paddle"){
            LevelController levelController = FindObjectOfType<LevelController>();
            Debug.Log($"[BonusName]: {bonusName}");
            switch(applyBonus)
            {
                case 1: // 1 - apply bonus for paddle
                    PaddleController paddleBonus = FindObjectOfType<PaddleController>();
                    if (paddleBonus != null){
                        switch(bonusName)
                        {
                            case "RareResizeGood":
                                paddleBonus.ApplyResize(true, 5f);
                                break;
                            case "LegendaryResizeGood":
                                paddleBonus.ApplyResize(true, 3f);
                                break;

                            case "RareResizeBad":
                                paddleBonus.ApplyResize(false, 7f);
                                break;
                            case "LegendaryResizeBad":
                                paddleBonus.ApplyResize(false, 5f);
                                break;
                            default:
                                break;
                        }
                    }else{Debug.LogError("Paddle Controller not found");}
                    break;
                case 2: // 2 - apply bonus for balls
                    if (levelController != null){
                        switch(bonusName)
                        {
                            case "RareMoreBalls":
                                levelController.SpawnBalls(2);
                                break;
                            case "LegendaryMoreBalls":
                                levelController.SpawnBalls(5);
                                //ballsBonus.SpawnBall(5);
                                break;
                            default:
                                break;
                        }
                    }else{Debug.LogError("Balls Controller not found");}

                    break;
                default:
                    Debug.LogError("Bad value of ApplyBonus!");
                    break;

            }
            Destroy(gameObject);
        }
    }










    void Start()
    {
        
    }

   
    void Update()
    {
        if (!isCollected)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        transform.position += velocity * Time.deltaTime;
    }
}
