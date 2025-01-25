using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Attack Behaviors/MeleeAttack")]
public class MeleeAttackBehaviorSO : AttackBehaviorSO
{
    [SerializeField] GameObject meleePrefab;
    [SerializeField] Vector3 spawnOffset;

    public override void ExecuteAttack(GameObject attacker)
    {
        GameObject meleeGO = PoolManager.Spawn(meleePrefab, attacker.transform.position, attacker.transform.rotation, attacker.transform);
        meleeGO.transform.localPosition = spawnOffset;
        var meleeController = meleeGO.GetComponent<MeleeController>();
        if (meleeController != null)
        {
            meleeController.Initialize(damage, cooldown, attacker);
        }
    }
}
