using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRandomizer : MonoBehaviour, IPoolable
{
    [SerializeField] Sprite[] sprites;
    SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        AssignRandom();
    }
    public void OnSpawn() => AssignRandom();
    void AssignRandom() => _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
}
