using UnityEngine;

/// <summary>
///     Convenient way to access player's components from other scripts.
///     This works because there is only one player in the game.<br></br>
///     For example: <br></br>
///     The enemies need to access the player's transform to follow him.
///     They cannot have a reference to the player's GameObject because they are prefabs instantiated at runtime.
/// </summary>
public class PlayerStaticReferences : MonoBehaviour
{
    public static Transform PlayerTransform;
    public static HealthSystem PlayerHealthSystem;
    public static WeaponSystem PlayerWeaponSystem;

    void Awake()
    {
        PlayerTransform = transform;
        PlayerHealthSystem = GetComponent<HealthSystem>();
        PlayerWeaponSystem = GetComponent<WeaponSystem>();
    }
}
