using System;
using System.Collections;
using System.Collections.Generic;
using AbstractClass;
using Manager;
using modeling.Defination;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : AbsController
{

    private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        
        playerMovement.ActionHanndler();
    }    
    
}
