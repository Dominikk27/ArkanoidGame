using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] bonusPrefabs;
    private int bonusCategory;
    private string currentBonus;
    private int bonusApplyFor;

    private static List<Bonus> rareBonuses = new List<Bonus>();
    private static List<Bonus> legendaryBonuses = new List<Bonus>();



    private Vector3 currentSpawnPosition;
    private List<Bonus> selectedList = null;


    private void InitBonusLists()
    {
        // RARE bonusy
        rareBonuses.Add(new Bonus("RareMoreBalls", 2, 0));  //moreBalls
        rareBonuses.Add(new Bonus("RareResizeGood", 1, 1)); //paddleResizeGood
        rareBonuses.Add(new Bonus("RareResizeBad", 1, 2)); //paddleResizeBad
        

        // LEGENDARY bonusy
        legendaryBonuses.Add(new Bonus("LegendaryMoreBalls", 2, 0));  //moreBalls
        legendaryBonuses.Add(new Bonus("LegendaryResizeGood", 1, 1)); //paddleResizeGood
        legendaryBonuses.Add(new Bonus("LegendaryResizeBad", 1, 2)); //paddleResizeBad

    }


    public void SetBonusDetails(int bonusType, Vector3 spawnPosition)
    {
        currentSpawnPosition = spawnPosition;
        bonusCategory = bonusType - 1;
        //Debug.Log("bonusType: " + bonusType + ", CategoryType: " + bonusCategory);
        Bonus selectedBonus = PickBonus(bonusType);
        
        HandleBonusDetails(bonusCategory, selectedBonus, currentSpawnPosition);
        
    }


    private Bonus PickBonus(int bonusType)
    {
        switch (bonusType)
        {
            case 1: // RARE
                selectedList = rareBonuses;
                //Debug.Log("RARE");
                break;
            case 2: // LEGENDARY
                selectedList = legendaryBonuses;
                //Debug.Log("Legendary");
                break;
            default: // COMMON
                break;
        }

        int randIndex = Random.Range(0, selectedList.Count);
        return selectedList[randIndex];
    }


    private void HandleBonusDetails(int bonusType, Bonus selectedBonus, Vector3 currentSpawnPosition)
    {
        GameObject spawnBonus = Instantiate(bonusPrefabs[bonusType], currentSpawnPosition, Quaternion.identity);

        BonusController bonusController = bonusPrefabs[bonusType].GetComponent<BonusController>();

        if (bonusController != null)
        {
            //Debug.Log($"[BONUS DETAILS] BonusType: {bonusType} | BonusName: {selectedBonus.name} | BonusType: {selectedBonus.bonusApplyFor}");
            bonusController.SetBonusDetails(selectedBonus.name, selectedBonus.bonusApplyFor, selectedBonus.spriteIndex);
            //GameObject spawnBonus = Instantiate(bonusPrefabs[bonusType], currentSpawnPosition, Quaternion.identity);
        }
    }




    void Start()
    {
        InitBonusLists();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
