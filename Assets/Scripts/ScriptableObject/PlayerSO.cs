using System;
using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New PlayerSO", menuName = "ScriptableObjects/PlayerSO", order = 3)]
public class playerSO : ScriptableObject
{
    public int Health;
    public float MoveSpeed;
    public int Stamina;

}
