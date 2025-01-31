using System.Collections;
using UnityEngine;

public class MagicMissileSpriteAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float timeBetweenSprites;

    void OnEnable()
    {
        spriteRenderer.sprite = sprites[0];
        StartCoroutine(SpriteRoutine());
    }

    void OnDisable() => StopAllCoroutines();

    IEnumerator SpriteRoutine()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            yield return new WaitForSeconds(timeBetweenSprites);
            spriteRenderer.sprite = sprites[i];
        }
    }
}
