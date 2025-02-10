using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    public static Transform Value;
    void Awake() => Value = transform;
}
