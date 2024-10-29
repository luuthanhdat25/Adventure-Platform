using RepeatUtils;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExpHandler : RepeatMonoBehaviour
{

    private readonly string PLAYERSTAT_PATH = "/player-stat.json";

    private ExpRepository _expRepository;



    private void Start()
    {
        _expRepository = new ExpRepository(PLAYERSTAT_PATH);
    }

    public int GetExperience()
    {
        if(_expRepository == null) return 0; 
        return  _expRepository.GetExpPoint().Result;    
    }

    public float GetExperiencePersen()
    {
        return _expRepository.getExpPointPersen().Result;
    }

    public void AddExperience(int exp)
    {
        _expRepository.PlusExept(exp);
    }

    public int GetLevel()
    {
        return _expRepository.GetLevel().Result;
    }

}
