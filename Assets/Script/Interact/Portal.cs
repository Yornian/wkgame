using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour,IUseable
{
    [SerializeField] int portalID;
    bool ifIn=false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { ifIn = true; }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { ifIn = false; }
    }


void Update()
    {
    if (ifIn)
    {
        Use();
    }
    }
       
    
    public void Use()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(portalID/2 < 4)
            {
                Debug.Log("GoUp to portal" + (portalID + 2));
                GameManager.Instance.player.transform.position = GameManager.Instance.portals[portalID+2].transform.position;
            }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(portalID/2 > 0)
            {
                Debug.Log("GoDown" + (portalID - 2));
                GameManager.Instance.player.transform.position = GameManager.Instance.portals[portalID-2].transform.position;
            }
        }
    }
}

