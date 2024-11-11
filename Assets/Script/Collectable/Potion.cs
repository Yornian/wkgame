using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, ICollectable, IUseable
{
 
    [SerializeField]PotionSO potionSO;
    bool hasTriggered = false;


    public void Collect()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& !hasTriggered)
        {
            hasTriggered = true;    
            Use();
           
        }
           
    }
    public void Use()
    {
        potionSO.Use();
        Destroy(gameObject);
    }
}
