using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;


    private GameObject deadUI;
    void Start()
    {
        deadUI = GameObject.Find("Dead UI");
        deadUI.SetActive(false);
        playerHealth.OnDead += DeadUISpawn;
    }

    public void DeadUISpawn()
    {
        deadUI.SetActive(true);
    }
    public void ReBorn()
    {
        deadUI.SetActive(false);
    }

}
