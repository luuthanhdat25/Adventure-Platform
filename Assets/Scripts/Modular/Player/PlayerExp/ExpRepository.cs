using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExpRepository : Repository<PlayerStat>
{

    private PlayerStat _playerStat;
    public ExpRepository(string path) : base(path)
    {
        Debug.Log("Is Connected: ");
        _playerStat = Get();
        if (_playerStat == null || _playerStat.point == null)
        {
            _playerStat = new PlayerStat();
            _playerStat.expPoint = 0;
            _playerStat.point = 0;
            _playerStat.level = 1;
            Save(_playerStat);
        }
    }

    public async Task PlusExept(int plusExp)
    {
        _playerStat.expPoint += plusExp;
        Save(_playerStat);
        await Task.CompletedTask;
    }

    public async Task<int> GetLevel()
    {
        _playerStat = Get();
        return await Task.FromResult(_playerStat.level);
    }

    public async Task PlusLevel()
    {

        if(_playerStat.expPoint == _playerStat.level*500) 
        _playerStat.level++;
        await Task.CompletedTask;
    }

    public async Task<int> GetExpPoint()
    {
        _playerStat = Get();
        return await Task.FromResult(_playerStat.expPoint);
    }

    public async Task<float> getExpPointPersen()
    {
        _playerStat = Get();
        return await Task.FromResult((float)_playerStat.expPoint / (_playerStat.level * 5000));
    }

}
