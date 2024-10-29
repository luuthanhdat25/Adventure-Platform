using System;
using System.Collections;
using System.Collections.Generic;
using AbstractClass;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] 
    private Slider healthBar;

    [SerializeField] private Slider staminaBar;

    [SerializeField] private Slider expBar;

    [SerializeField] 
    private PlayerHealth characterHealth;

    private void Start()
    {
        characterHealth.OnHealthChanged += CharacterHealth_OnHealthChanged;
        PlayerSingleton.Instance.OnStaminaChanged += CharacterStamina_OnStaminaChanged;
        PlayerSingleton.Instance.OnExpChanged += CharacterExp_OnExpChanged;
    }

    private void CharacterExp_OnExpChanged(object sender, PlayerSingleton.OnExperienceChangedEventArgs e)
    {
        UpdateExpBar(e.exeperiencePersen); 
    }

    private void CharacterStamina_OnStaminaChanged(object sender, PlayerSingleton.OnStaminaChangeEventArgs e)
    {
        UpdateStaminaBar(e.staminaPersen);
    }

    private void CharacterHealth_OnHealthChanged(object sender, CharacterHealth.OnHealthChangedEventArgs e)
    {
        UpdateHealthBar(e.HealthPersent);
    }

    public void UpdateHealthBar(float healthPersent)
    {
        healthBar.value = healthPersent;
    }

    public void UpdateExpBar(float expPersen)
    {
        expBar.value = expPersen;
    }

    public void UpdateStaminaBar(float staminaPersent)
    {

        staminaBar.value = staminaPersent;
    }
}
