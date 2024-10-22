using RepeatUtils;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkillController : RepeatMonoBehaviour
{
    private readonly string SKILLDATA_PATH = "/skill-data.json";

    private SkillRepository _skillRepository;

    public SkillController()
    {
        _skillRepository = new SkillRepository(SKILLDATA_PATH);
    }

    public void Start()
    {
        _skillRepository = new SkillRepository(SKILLDATA_PATH);

    }
    public void UnlockSkill(SkillSO skillAbility)
    {
        Debug.Log("unlock skill");
        _skillRepository.UnlockSkill(SkillSO.FromSkillSO(skillAbility));
    }

    public void SelectSkill(SkillSO skillAbility)
    {
        _skillRepository.SaveCurrentSkill(SkillSO.FromSkillSO(skillAbility));
    }




    public Task<SkillDTO> GetCurrentSKill()
    {
        return _skillRepository.GetCurrentSkill();
    }
}
    