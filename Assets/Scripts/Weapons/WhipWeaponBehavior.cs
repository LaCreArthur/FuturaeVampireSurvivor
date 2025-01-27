using UnityEngine;

public class WhipWeaponBehavior : WeaponBehavior
{
    [SerializeField] ParticleSystem whipParticles;
    [SerializeField] Vector3 spawnOffset;

    void OnTriggerEnter(Collider other) {}

    public override void ExecuteAttack(GameObject attacker) {}
}
