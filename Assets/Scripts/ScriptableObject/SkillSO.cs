using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "New SkillSO", menuName = "ScriptableObjects/SkillSO")]
    public class SkillSO : ScriptableObject
    {
        public SkillAbilityEnum skillName;
        public float cooldown;
        public float activeTime;

        public virtual void Active() { return; }
        public static SkillDTO FromSkillSO(SkillSO skillSO)
        {
            return new SkillDTO
            {
                skillName = skillSO.skillName,
                cooldown = skillSO.cooldown,
                activeTime = skillSO.activeTime
            };
        }
    }
}

    