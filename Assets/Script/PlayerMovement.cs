using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;
    Vector2 movement;
    Bag bag;
    AudioSource audioSource;
    public int healthV = 80;
    public  int shieldV = 80;
    // float movex;
    [SerializeField]private float speed = 10f;
    [SerializeField]private float jumpSpeed = 10f;


    private Vector2 MoveVector;
    public Vector2 lastMotionVector;


    /// <summary>
    /// /
    /// </summary>
    public TextMeshProUGUI  healthValueUI;
    public TextMeshProUGUI sheileValueUI;

    public float knockbackForce = 1.0f;
    public float verticalOffset = 1.0f;
    public float knockbackDuration = 0.3f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isKnockedBack = false;  
    private float knockbackTimer;

    private bool isAttack = false;
    public float AttackDuration = 0.2f;
    private float AttackTimer;
    public  Animator weaponAnimator;
    public AudioClip AttackaudioClip;
    public AudioClip  DamageaudioClip;
    private bool isJPressed = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bag = GetComponent<Bag>();
        audioSource = GetComponent<AudioSource>();
        SetText(healthValueUI, healthV.ToString());
        SetText(sheileValueUI, shieldV.ToString());


        ////
        PotionSO.UsePotion += OnPotionUsed;
      

        //
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;  // 

        animator.SetFloat("Horizontal", 1);
        // EnemyAI.attackPlayer += takeDamage;
        MelleEnemy.attackPlayerM += takeDamage;

    }

 



    // Update is called once per frame
    void Update()
    {
         if (isAttack)
            {
                AttackTimer -= Time.deltaTime;
                if (AttackTimer <= 0)
                {
                    isAttack = false;  // 恢复玩家控制
                   
                }
            }
      
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;  // 恢复玩家控制
                spriteRenderer.color = originalColor;  // 恢复原始颜色
            }
        }

        if (!isKnockedBack)
        {
            
            if (!isAttack)
            {
                if (Input.GetKeyDown(KeyCode.J) && !isJPressed)
                {
                    WeaponAttack();
                    
                    isJPressed = true;
                }
                else if (Input.GetKeyUp(KeyCode.J))
                {
                    isJPressed = false;
                }
            }
            

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.Normalize();
            if (!Mathf.Approximately(movement.x, 0.0f))
            {

                movement.x = Input.GetAxisRaw("Horizontal");
                animator.SetFloat("Vertical", 0);
                animator.SetFloat("Horizontal", movement.x);
              
                lastMotionVector = new Vector2(movement.x, 0);
            }
            else
            {
              
                    animator.SetFloat("Horizontal", lastMotionVector.x);
                animator.SetFloat("Vertical",  0);

            }
            
            animator.SetFloat("Speed", movement.sqrMagnitude);
        
     


            if (rigidbody2D != null)
            {
               if (!GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}

              if(Input.GetKeyDown(KeyCode.Space))
              {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
              }            
            }


            rigidbody2D.velocity = new Vector2(movement.x * speed, rigidbody2D.velocity.y);

            if (GameManager.Instance.currentPortal != null) 
           {
            GameManager.Instance.currentPortal.GetComponent<IUseable>().Use();
             }
        }
     }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<ICollectable>() != null)
        {
            other.GetComponent<ICollectable>()?.Collect();
            bag.UpdateItem(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Portal")
        {
            // GameManager.Instance.canTeleport = true;
            GameManager.Instance.currentPortal = other.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Portal")
        {
            // GameManager.Instance.canTeleport = false;
            GameManager.Instance.currentPortal = null;
        }
    }

    void PlayAudio(PotionSO potionSO)
    {
        audioSource.clip = potionSO.audioClip;
        audioSource.Play();
    }

    private void OnEnable() {
        PotionSO.UsePotion += PlayAudio;
    }

    private void OnDisable() {
        PotionSO.UsePotion -= PlayAudio;
    }


    public void SetText(TextMeshProUGUI valueUI, string value)
    {
        if (valueUI != null)
        {
            valueUI.text = value;
        }

      
    }
    private void OnPotionUsed(PotionSO potion)
    {
    
        if (potion is HealthPotion)
        {
            healthV = (healthV +potion.number > 100) ? 100 : healthV + potion.number;
           // Debug.Log(healthV);
            SetText(healthValueUI, healthV.ToString());
        }
        else if (potion is SheildPotion)
        {
            shieldV = (shieldV + potion.number > 100) ? 100 : shieldV + potion.number;
          //  Debug.Log(shieldV);
            SetText(sheileValueUI, shieldV.ToString());
        }
    }
    private void OnHurt(int damage)
    {

        if (shieldV > 0)
        {
            if (damage >= shieldV)
            {
                damage -= shieldV;
                shieldV = 0;
            }
            else
            {
                shieldV -= damage;
                damage = 0;
            }
        }

        if (damage > 0 && healthV > 0)
        {
            healthV = Math.Max(0, healthV - damage);
            if (healthV == 0)
            {
                Die();
            }
        }
        else if (damage > 0 && healthV <= 0)
        {
            Die();
        }
        SetText(sheileValueUI, shieldV.ToString());
        SetText(healthValueUI, healthV.ToString());
    }

    private void Die()
    {
        // 添加死亡逻辑代码
        Debug.Log("Player died.");
    }
    public void takeDamage(Transform enemyTransform)
    {
        audioSource.clip = DamageaudioClip;
        audioSource.Play();
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
        rigidbody2D.velocity=new Vector2(0,0);
       Vector2 knockbackDirection = transform.position - enemyTransform.position;
        knockbackDirection = new Vector2(knockbackDirection.x, verticalOffset) ;

        rigidbody2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        spriteRenderer.color = Color.red;
        OnHurt(7);
        
    }
    public void WeaponAttack()
    {
        isAttack = true;
        AttackTimer = AttackDuration;
        audioSource.clip =  AttackaudioClip;
        audioSource.Play();
        weaponAnimator.SetBool("ifAtt", true);


    }
    //public static void RotateGameObjectOverTime(GameObject obj, float duration)
    //{
    //    Quaternion startRotation = obj.transform.rotation;
    //    Quaternion endRotation = Quaternion.Euler(0f, 0f, -160f);
    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float t = elapsedTime / duration;
    //        obj.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
    //    }
    //    obj.transform.rotation = endRotation;
    //}
}
