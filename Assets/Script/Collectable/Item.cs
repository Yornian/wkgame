using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable, IUseable
{
    public static event Action ItemCollect;
    public static event Action ItemUse;
    
    public void Collect()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
        // Destroy(gameObject);
        // Bag.UpdateBag(this);
        ItemCollect?.Invoke();
    }

    public void Use()
    {
        ItemUse?.Invoke();   
    }
}
