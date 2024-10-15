using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New SkillSO", menuName = "ScriptableObjects/SkillSO")]
    public class SkillSO : ScriptableObject
    {
        public SkillAbilityEnum skillName;
        public float cooldown;
        public float activeTime;

        public virtual void Active() { return; }
    }
}

