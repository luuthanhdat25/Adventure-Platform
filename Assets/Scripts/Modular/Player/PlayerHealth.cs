using System;
using System.Collections;
using System.Collections.Generic;
using AbstractClass;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{

    public Action gotHit;
    public void SetHealth(int maxHealth)
    {
        this.hpMax = maxHealth;   
        this.hpCurrent = maxHealth;
    }

    public override void Deduct(int hpDeduct)
    {
        if (IsDead()) return;
        this.hpCurrent -= hpDeduct;
        CallOnHealthChangedEvent(hpCurrent);
        gotHit.Invoke();
        this.CheckIsDead();
    }
}
