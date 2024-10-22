using System;
using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New PlayerSO", menuName = "ScriptableObjects/PlayerSO", order = 3)]
public class playerSO : ScriptableObject
{
    public int Health;
    public float MoveSpeed;
    public List<SkillSO> skillUnlocked;
    public SkillSO currentSelectedSkill;

    public bool IsSKillUnlockedIsEmty()
    {
        return skillUnlocked == null || skillUnlocked.Count == 0;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
