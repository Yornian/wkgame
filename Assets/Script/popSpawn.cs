using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popSpawn : MonoBehaviour
{
    
    public GameObject healthPo;
    public GameObject sheildPo;

    void Start()
    {
        
        if (Random.value < 0.5f)
        {
            
            GameObject prefabToSpawn = Random.value < 0.5f ? healthPo : sheildPo;

            
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
    }

  
    void Update()
    {

    }
}