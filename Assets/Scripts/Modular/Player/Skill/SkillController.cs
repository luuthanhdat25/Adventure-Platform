using RepeatUtils;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : RepeatMonoBehaviour
{
    [SerializeField]
    playerSO playerSO;

    public void UnlockSkill(SkillSO skillAbility)
    {
        playerSO.skillUnlocked.Add(skillAbility);
    }

    public void SelectSkill(SkillSO skillAbility)
    {
        playerSO.currentSelectedSkill = skillAbility;
    }
}
