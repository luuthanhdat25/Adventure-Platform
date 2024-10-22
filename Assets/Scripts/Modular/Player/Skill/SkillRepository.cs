using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class SkillRepository : Repository<SkillData>
{

    private SkillData SkillData;

    public SkillRepository(string path) : base(path)
    {
        SkillData = Get();
        if(SkillData == null || SkillData.skillList == null)
        {
            SkillData = new SkillData();
            SkillData.skillList = new List<SkillDTO>();
            Save(SkillData);
        } 

    }

    public async Task SaveCurrentSkill(SkillDTO currentKill)
    {
        if (SkillData.currentSkill == null)
        {
            SkillData.currentSkill = currentKill;
        }
        if (SkillData.currentSkill.skillName.ToString().Contains(currentKill.skillName.ToString()))
        {
            await Task.CompletedTask;
        }
        else {

            SkillData.currentSkill = currentKill;
        }
        Save(SkillData);
        await Task.CompletedTask;
    }

    public async Task UnlockSkill(SkillDTO unlockedSkill) {

        var checkExistingSKill = GetSkillBySkillName(unlockedSkill.skillName.ToString()).Result;
        if (checkExistingSKill == null)
        {
            SkillData.skillList.Add(unlockedSkill);
        }

        await Task.CompletedTask; 
    }
    public async Task<List<SkillDTO>> GetSkillsUnlockedList() {
        SkillData = Get();
        return await Task.FromResult(SkillData.skillList);
    }

    public async Task<SkillDTO> GetCurrentSkill() {

        SkillData = Get();
        return await Task.FromResult(SkillData.currentSkill);
    }
    
    public async Task<SkillDTO> GetSkillBySkillName(string SkillName)
    {
        SkillData = Get();
        return await Task.Run(() =>
        {
            return SkillData.skillList.FirstOrDefault(
                skill => skill.skillName.ToString() == SkillName
                );
        });
    }


}
