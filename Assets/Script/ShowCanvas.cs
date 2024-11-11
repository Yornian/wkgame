using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCanvas : MonoBehaviour
{
    public GameObject canvas;
    public Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClick()
    {
        if(canvas.activeSelf)
        {
            canvas.SetActive(false);
            
        }
      else
        {
            canvas.SetActive(true);
        }


    }
}
