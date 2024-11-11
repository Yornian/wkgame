using System;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Chase,
        Attack
    }
    public float chaseDistance = 5f;
    public float attackDistance = 1.5f;
    public float attackCooldown = 1.5f;
    public int healthPoint = 3;
    public Transform player;
    protected float lastAttackTime = 0;
    protected MonsterState currentState;

    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;

    public float knockbackForce = 1.0f;
    public float verticalOffset = 1.0f;
    public float knockbackDuration = 0.3f;
    public bool die = false;
    private Color originalColor;
    private bool isKnockedBack = false;
    private float knockbackTimer;


    public AudioSource audioSource;
    public AudioClip AttackaudioClip;
    public AudioClip DieaudioClip;
    //event 
    public static event Action<Transform> attackPlayer;
    protected virtual void Start()
    {
        // Common setup for all enemies
        player = GameManager.Instance.player.transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        currentState = MonsterState.Idle;
        originalColor = spriteRenderer.color;  // 
    }

    protected virtual void Update()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;  
                spriteRenderer.color = originalColor; 
            }
        }
        if (!isKnockedBack)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            spriteRenderer.flipX = (player.position.x - transform.position.x) > 0 ? false : true;
            if(die)
            {
                SwitchState(MonsterState.Idle);
            }
            else
            {
                if (distanceToPlayer <= attackDistance)
                {
                    SwitchState(MonsterState.Attack);
                }
                else if (distanceToPlayer <= chaseDistance)
                {
                    SwitchState(MonsterState.Chase);
                }
                else
                {
                    SwitchState(MonsterState.Idle);
                }
            }
           

            // Call the behavior based on current state
            switch (currentState)
            {
                case MonsterState.Idle:
                    Idle();
                    break;
                case MonsterState.Chase:
                    Chase();
                    break;
                case MonsterState.Attack:
                    Attack();
                    break;
            }
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player") )
    //    {
    //        Debug.Log("1");
    //        attackPlayer?.Invoke(gameObject.transform);

    //    }

    //}
    // Define common Idle behavior for all enemies
    protected virtual void Idle()
    {
        animator.SetBool("isWalking", false);
    }

    // Abstract methods for different enemy behavior in subclasses
    protected abstract void Chase();
    protected abstract void Attack();
     public    void takeDamage()
    {
        Debug.Log(healthPoint);
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
        healthPoint--;
       
        rb.velocity = new Vector2(0, 0);
        Vector2 knockbackDirection = transform.position - player.position;
        knockbackDirection = new Vector2(knockbackDirection.x, verticalOffset);

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        spriteRenderer.color = Color.green;
        if (healthPoint <= 0)
        {
            audioSource.clip = DieaudioClip;
            audioSource.Play();
            die = true;
            Invoke("Die", 0.2f);
        }
    }
    private void Die()
    {
      
        animator.SetBool("ifDie", true);
      
        Invoke("DestroyGameObject", 1f);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    // Method to switch states
    protected void SwitchState(MonsterState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
           Debug.Log($"Switched to state: {newState}");
        }
    }
}
