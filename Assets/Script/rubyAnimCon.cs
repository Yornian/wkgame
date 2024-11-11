using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rubyAnimCon : MonoBehaviour
{
    
    public GameObject rubyUI;
    public Transform concentUI;
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        GameObject rewardObject = GameObject.Find("rewardConcent");
        concentUI = rewardObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void endLoot()
    {
        
      

        GameObject uiInstance = Instantiate(rubyUI);

        uiInstance.GetComponent<Image>().sprite = sprite;
         uiInstance.transform.SetParent(concentUI, false);
        gameObject.SetActive(false);

    }
}
