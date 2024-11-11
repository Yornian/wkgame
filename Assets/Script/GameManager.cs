using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public GameObject currentPortal = null;
    public GameObject[] portals = new GameObject[10];
    public GameObject player;
    public MapG map;
    public AudioClip audioClip;
    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
        Instance.player = GameObject.FindGameObjectWithTag("Player");
        map.init();
    }
}
