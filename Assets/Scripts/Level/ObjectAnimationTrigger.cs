using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
    [SerializeField] float delay = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject o = objects[i];
            if (o.TryGetComponent(out IObjectAnimation objectAnimation))
            {
                objectAnimation.PlayAnimation();
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
