using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus
{
    public string name;
    public int spriteIndex;
    public int bonusApplyFor; // 1 - paddle | 2 - ball

    public Bonus(string name, int bonusApplyFor, int spriteIndex)
    {
        this.name = name;
        this.bonusApplyFor = bonusApplyFor;
        this.spriteIndex = spriteIndex;
        
    }
}
    
