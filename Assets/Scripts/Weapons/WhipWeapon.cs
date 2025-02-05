using DG.Tweening;
using UnityEngine;

public class WhipWeapon : WeaponBehavior
{
    [SerializeField] ParticleSystem leftWhipParticles;
    [SerializeField] ParticleSystem rightWhipParticles;
    [SerializeField] GameObject leftCollider;
    [SerializeField] GameObject rightCollider;
    [SerializeField] float delayTriggerActivation;
    [SerializeField] float delayRightParticle;

    readonly Vector2[] _sizes = { new Vector2(1.5f, 0.8f), new Vector2(2f, 1.2f) };
    int _currentSizeIndex;
    float _rightLifeTime;
    float _leftLifeTime;
    ParticleSystem.MainModule _leftMain;
    ParticleSystem.MainModule _rightMain;

    void Start()
    {
        _leftMain = leftWhipParticles.main;
        _rightMain = rightWhipParticles.main;

        _leftLifeTime = _leftMain.startLifetime.constant;

        _rightLifeTime = _rightMain.startLifetime.constant;

        leftCollider.SetActive(false);
        rightCollider.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.attachedRigidbody.TryGetComponent(out HealthSystem healthSystem);
            if (healthSystem != null) healthSystem.TakeDamage(Stats.damage);
        }
    }

    public override void Fire(GameObject attacker)
    {
        // play the particles
        leftWhipParticles.Play();
        DOVirtual.DelayedCall(delayRightParticle, () => rightWhipParticles.Play());

        // small delay before activating the triggers
        DOVirtual.DelayedCall(delayTriggerActivation, () =>
        {
            leftCollider.SetActive(true);
        }).OnComplete(() => DOVirtual.DelayedCall(_leftLifeTime, () => leftCollider.SetActive(false)));

        DOVirtual.DelayedCall(delayRightParticle + delayTriggerActivation, () =>
        {
            rightCollider.SetActive(true);
        }).OnComplete(() => DOVirtual.DelayedCall(_rightLifeTime, () => rightCollider.SetActive(false)));
    }


    [ContextMenu("Set Sizes")]
    void SetSizes()
    {
        _currentSizeIndex = (_currentSizeIndex + 1) % _sizes.Length;
        Vector2 size = _sizes[_currentSizeIndex];
        leftCollider.transform.localScale = size;
        rightCollider.transform.localScale = size;
        _leftMain.startSizeX = size.x;
        _leftMain.startSizeY = size.y;
        _rightMain.startSizeX = size.x;
        _rightMain.startSizeY = size.y;
    }
}
