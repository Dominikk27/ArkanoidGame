using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField] AudioClip breakSound;

    [Header("Effects")]
    [SerializeField] GameObject blockDestroyVFX;
    [SerializeField] bool moreSprites;
    [SerializeField] Sprite[] hitSprites;

    [Header("Misc")]
    [SerializeField] BlockType blockType;
    [SerializeField]  BonusSpawner bonusSpawner;
    
    
    private int bonusType;
    //Block Types
    private enum BlockType
    {
        Common,
        Rare,
        Legendary
    }

    [SerializeField] int health;
    private GameObject sparkles;
    private CameraSetup cameraSetup;
    private LevelController levelController;


    private void OnCollisionEnter2D(Collision2D collision) 
    {
        health--;
        if(health != 0)
        {
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
            if(moreSprites)
            {
                DisplayNextSprite(health);
            }
        }
        else{
            DeystroyBlock();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bonusSpawner = FindObjectOfType<BonusSpawner>();
        levelController = FindObjectOfType<LevelController>();
        InitBlock();
        

    }


    void InitBlock()
    {
        switch (blockType)
        {
            case BlockType.Common:
                health = 1;
                bonusType = 0;
                break;
            case BlockType.Rare:
                health = 2;
                bonusType = 1;
                break;
                
            case BlockType.Legendary:
                health = 3;
                bonusType = 2;
                break;
        }
    }




    private void DisplayNextSprite(int spriteIndex)
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex-1];
    }

    private void DeystroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        levelController.decreseBlockCount();
        GameObject sparkles = Instantiate(blockDestroyVFX, transform.position, transform.rotation);
        Destroy(sparkles, 0.25f);
        if(bonusType != 0){
            int spawnBonus_random = Random.Range(1, 10);
            //Debug.Log($"[RandomNumber is]: {spawnBonus_random}");
            if(spawnBonus_random < 5)
            {
                bonusSpawner.SetBonusDetails(bonusType, transform.position);
            }
        }
        Destroy(gameObject);
    }




}