using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneSwitcher : MonoBehaviour
{
    public Button myButton;
    public string targetSceneName = "Rooms";  
    // Start is called before the first frame update
    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }
    public void switchScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonClick()
    {

        switchScene();
    }
}
