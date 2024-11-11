using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal.Internal;

public class Bag : MonoBehaviour
{
    static int bagVolume = 10;
    public GameObject[] Bags = new GameObject[bagVolume];
    int index = 0;

    public void UpdateItem(GameObject gameObject)
    {
        if(index < Bags.Length)
        {
            Bags[index] = gameObject;
            index++;
        }
    }

    public void UseItem(int index)
    {
        Bags[index] = null;
        
        SortBag(index);
    }
    public void SortBag(int index)
    {
        for (int i = index; i < Bags.Length-1; i++)
        {
            Bags[index] = Bags[index+1];
        }
        index--;
    }
}
