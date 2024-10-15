using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    private GameObject upgradeUI;
    void Start()
    {
        upgradeUI = GameObject.Find("UpgradeUI");
        upgradeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
