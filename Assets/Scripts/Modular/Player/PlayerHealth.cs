using System.Collections;
using System.Collections.Generic;
using AbstractClass;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    public void SetHealth(int maxHealth)
    {
        this.hpMax = maxHealth;   
        this.hpCurrent = maxHealth;
    }
}
