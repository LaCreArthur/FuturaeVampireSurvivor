using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "GameData/Enemy")]
public class EnemySO : ScriptableObject
{
    public GameObject prefab;
    public float maxHealth;
    public MoveBehaviorSO moveBehavior;
    public AttackBehaviorSO attackBehavior;

    public GameObject CreateEnemy(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject newEnemy = PoolManager.Spawn(prefab, position, rotation, parent);

        TrySetMove(newEnemy);
        TrySetAttack(newEnemy);
        TrySetHealth(newEnemy);

        return newEnemy;
    }

    void TrySetHealth(GameObject newEnemy)
    {
        var healthSystem = newEnemy.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.InitializeHealth(maxHealth);
        }
    }

    void TrySetAttack(GameObject newEnemy)
    {
        var attackSystem = newEnemy.GetComponent<AttackSystem>();
        if (attackSystem != null && attackBehavior != null)
        {
            attackSystem.AttackBehavior = attackBehavior;
        }
    }

    void TrySetMove(GameObject newEnemy)
    {
        var moveSystem = newEnemy.GetComponent<MoveSystem>();
        if (moveSystem != null && moveBehavior != null)
        {
            moveSystem.MoveBehavior = moveBehavior;
        }
    }
}
