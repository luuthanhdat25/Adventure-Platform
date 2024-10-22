using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData 
{
    public List<SkillDTO> skillList;
    public SkillDTO currentSkill;
}
public class SkillDTO
{
    public SkillAbilityEnum skillName;
    public float cooldown;
    public float activeTime;
    public virtual void Active() { return; }
}



