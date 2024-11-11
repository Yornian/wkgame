using System;
using UnityEngine;
public class MelleEnemy : EnemyAI
{
    public float chaseSpeed = 6f;
    public int attackDamage = 5;

    public Animator EffactAnimator;
    public GameObject attackEffect;
    public static event Action<Transform> attackPlayerM;
    public MelleEnemy()
    {
        healthPoint = 2;
        attackDistance = 0.6f;
    }
    
   
    protected override void Chase()
    {
         
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;
       animator.SetBool("isWalking", true);
    }
 

 
  
    protected override void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
          
            attackPlayerM?.Invoke(gameObject.transform);
            EffactAnimator .SetBool("canBeSeen", true);
            audioSource.clip = AttackaudioClip;
            audioSource.Play();
            //animator.SetTrigger("Attack");
            // Apply small damage to player
            //   Debug.Log("Fast enemy attacks for " + attackDamage + " damage.");
        }
    }
    public static void SetGameObjectVisibility(GameObject targetGameObject, bool isVisible)
    {
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(isVisible);
        }
        else
        {
           
        }
    }
}
