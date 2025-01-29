using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    [SerializeField] int exp;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerExperienceSystem.CollectExperience(exp);
            PoolManager.Despawn(gameObject);
        }
    }
}
