using UnityEngine;

public class PlayerSingleton : SingletonMono<PlayerSingleton>
{
    public static Transform Transform;
    protected override void OnAwake() => Transform = transform;
}
