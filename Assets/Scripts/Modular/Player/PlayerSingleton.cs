using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using AbstractClass;
using RepeatUtils.DesignPattern.SingletonPattern;
using UnityEngine;

public class PlayerSingleton : Singleton<PlayerSingleton>
{
    [SerializeField] private playerSO playerStat;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private ExpHandler _expHandler;
    public EventHandler<OnStaminaChangeEventArgs> OnStaminaChanged;
    public EventHandler<OnExperienceChangedEventArgs> OnExpChanged;

    public class OnStaminaChangeEventArgs : EventArgs
    {
        public float staminaPersen;
    }
    public class OnExperienceChangedEventArgs : EventArgs
    {
        public float exeperiencePersen;
    }



    private int currentHealth;
    private int maxHealth;
    private int maxStamina;
    private int currentStamina;
    private int expPoint;
    private int point;
    private int level;


    private void Start()
    {
        this.expPoint = GetExperience();
        this.level = GetLevel();

        CallOnStaminaChangedEvent();
        CallOnExpChangedEvent();
    }

    protected override void Awake()
    {
        base.Awake();
        //_playerHealth = GetComponentInChildren<PlayerHealth>();

        this.maxHealth = playerStat.Health;
        this.currentHealth = playerStat.Health;
        this.maxStamina = playerStat.Stamina;
        this.currentStamina = playerStat.Stamina;
        _playerHealth.SetHealth(this.maxHealth);
        
    }


    public void AddHealth(int hpAdd)
    {
        _playerHealth.Add(hpAdd);
        currentHealth = Mathf.Clamp(_playerHealth.GetCurrentHp(), 0, maxHealth);
    }

    public void DeductHealth(int hpDeduct)
    {
        Debug.Log("current health: " + _playerHealth.GetCurrentHp());
        _playerHealth.Deduct(hpDeduct);
        
        currentHealth = Mathf.Clamp(_playerHealth.GetCurrentHp(), 0, maxHealth);
    }
    public void DeductStamina(int staminaComsume)
    {
        this.currentStamina -= staminaComsume;
        CallOnStaminaChangedEvent();
    }
    public void AddExperience(int exp)
    {
        expPoint += exp;
        _expHandler.AddExperience(exp);
        CallOnExpChangedEvent();
    }
    public int GetHealth() => currentHealth;

    public float GetHealthPersen() => (float)currentHealth / maxHealth;

    public float GetMaxStamina() => (float)currentHealth / maxHealth;

    public void CallOnStaminaChangedEvent()
    {
        OnStaminaChanged?.Invoke(this, new OnStaminaChangeEventArgs
        {
            staminaPersen = (float)currentStamina / maxStamina
        });
    }

    public void CallOnExpChangedEvent()
    {
        OnExpChanged?.Invoke(this, new OnExperienceChangedEventArgs
        {
            exeperiencePersen = (float)expPoint / (level * 5000),
        });
    }


    public bool IsOutOfStamina() => currentStamina <= 0;


    public void AddStamina(int staminaPlus)
    {

        if (IsFullStamina())
        {
            currentStamina = maxStamina;
        }
        else
        {
            currentStamina += staminaPlus;
        }
        CallOnStaminaChangedEvent();
    }
    public void AddExperience()
    {

    }
    public bool IsFullStamina()
    {
        return currentStamina >= maxStamina;

    }

    public int GetExperience()
    {
        return _expHandler.GetExperience();
    }

    public int GetLevel()
    {
        return _expHandler.GetLevel();
    }

    public void UpdateSkill()
    {

    }

    public void GetMoveSpeed()
    {

    }

}
