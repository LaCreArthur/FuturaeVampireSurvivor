using UnityEngine;

[CreateAssetMenu(fileName = "WhipAttack", menuName = "Attack Behaviors/WhipAttack")]
public class WhipWeaponSO : WeaponSO
{
    [SerializeField] GameObject meleePrefab;
    [SerializeField] Vector3 spawnOffset;

    public override void ExecuteAttack(GameObject attacker)
    {
        GameObject meleeGO = PoolManager.Spawn(meleePrefab, attacker.transform.position, attacker.transform.rotation, attacker.transform);
        meleeGO.transform.localPosition = spawnOffset;
        var meleeController = meleeGO.GetComponent<WhipController>();
        if (meleeController != null)
        {
            meleeController.Initialize(damage, cooldown, attacker);
        }
    }
}
