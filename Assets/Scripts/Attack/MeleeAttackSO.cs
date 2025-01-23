using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Attack Behaviors/MeleeAttack")]
public class MeleeAttackSO : AttackBehaviorSO
{
    [SerializeField] GameObject meleePrefab;
    [SerializeField] Vector3 spawnOffset;

    public override void ExecuteAttack(GameObject attacker)
    {
        // Instantiate a melee and give it a direction, 
        // using the AttackData to set damage or speed
        GameObject meleeGO = PoolManager.Spawn(meleePrefab, attacker.transform.position, attacker.transform.rotation, attacker.transform);
        meleeGO.transform.localPosition = spawnOffset;
        var meleeController = meleeGO.GetComponent<MeleeController>();
        if (meleeController != null)
        {
            meleeController.Initialize(damage, cooldown, attacker);
        }
    }
}
