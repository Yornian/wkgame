using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSO : ScriptableObject
{
    public static event Action<PotionSO> UsePotion;
    [SerializeField]protected string name;
    [SerializeField]public int number;
    public AudioClip audioClip;
    public virtual void Use()
    {
        UsePotion?.Invoke(this);
    }
}

/// <summary>
/// HealthPotion Health the number amout of health
/// </summary>
[CreateAssetMenu(menuName = "Potion/HealthPotion", fileName = "HealthPotion")]
public class HealthPotion : PotionSO
{
    
    public override void Use()
    { 
        base.Use();
      
    }
}

/// <summary>
/// SheildPotion Sheild the number amout of sheild
/// </summary>
[CreateAssetMenu(menuName = "Potion/ShieldPotion", fileName = "SheildPotion")]
public class SheildPotion : PotionSO
{
 
    public override void Use()
    {
        base.Use();
      
    }
}