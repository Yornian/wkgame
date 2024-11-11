using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAnimCon : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    BoxCollider2D ownCollider;
    public List<EnemyAI> enemies;
    void Start()
    {
        animator= GetComponent<Animator>();
        ownCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
       
        Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(ownCollider.bounds.center, ownCollider.bounds.size, 0);

        enemies = new List<EnemyAI>();
        foreach (Collider2D collider in overlappingColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyAI enemyComponent = collider.GetComponent<EnemyAI>();
                if (enemyComponent != null)
                {
                    enemies.Add(enemyComponent);
                }
            }
        }
    }
    public void endAnim()
    {
        animator.SetBool("ifAtt", false);
    }
    public void Attack()
    {
        
        foreach (EnemyAI enemy in enemies)
        {
            enemy.takeDamage();
        }

    }
    // Update is called once per frame
    
}
