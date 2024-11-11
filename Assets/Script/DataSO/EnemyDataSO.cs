using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField]string enemyName;
    [SerializeField]int health;
    [SerializeField]float moveSpeed;
    [SerializeField]int atkDamage;
    [SerializeField]float atkCooldown;
}
