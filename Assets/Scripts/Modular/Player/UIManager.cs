using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]    
    private UpgradeUI upgradeUI;

    [SerializeField] private PlayerStatsUI playerStatsUI;

    // public void FixedUpdate()
    // {
    //     playerStatsUI.UpdateHealthBar(PlayerSingleton.Instance.GetHealthPersen());
    // }
}
