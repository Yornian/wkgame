using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite  sprite ;
    public Sprite rubysprite;
    private bool ifOpened=false;
    public Animator rubyAnimator;
    public SpriteRenderer RUBYspriteRenderer;
    public AudioClip audioClip;
    AudioSource audioSource;
    public rubyAnimCon rubyAnimCon;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openCase()
    {

        spriteRenderer.sprite = sprite;
        ifOpened = true;
        rubyAnimCon.sprite = rubysprite;
        RUBYspriteRenderer.sortingOrder = 2;
        rubyAnimator.SetBool("ifOpen", true);
        audioSource.clip = audioClip;
        audioSource.Play();
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !ifOpened)
        {

            openCase();
        }

    }
}
