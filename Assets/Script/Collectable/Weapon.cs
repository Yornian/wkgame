using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, ICollectable, IUseable
{
    public static event Action WeaponCollect;
    public void Collect()
    {
        //weapon相关的行为
        WeaponCollect?.Invoke();
    }

    public void Use()
    {
        //替换当前武器
    }
}
