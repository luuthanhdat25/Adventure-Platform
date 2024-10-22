using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UpgradeUI upgradeUI;

    [SerializeField] private PlayerStatsUI playerStatsUI;

    private void Start()
    {
        upgradeUI.gameObject.SetActive(false);
    }


}
