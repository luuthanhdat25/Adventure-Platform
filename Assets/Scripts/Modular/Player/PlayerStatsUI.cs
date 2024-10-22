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

    [SerializeField] 
    private CharacterHealth characterHealth;

    private void Start()
    {
        characterHealth.OnHealthChanged += CharacterHealth_OnHealthChanged;
    }

    private void CharacterHealth_OnHealthChanged(object sender, CharacterHealth.OnHealthChangedEventArgs e)
    {
        UpdateHealthBar(e.HealthPersent);
    }

    public void UpdateHealthBar(float healthPersent)
    {
        healthBar.value = healthPersent;
    }
}
