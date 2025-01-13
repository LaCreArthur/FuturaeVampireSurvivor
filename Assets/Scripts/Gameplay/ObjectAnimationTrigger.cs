using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
    [SerializeField] float delay = 0.5f;

    void OnTriggerEnter(Collider other) => StartCoroutine(AnimationCoroutine());

    IEnumerator AnimationCoroutine()
    {
        foreach (GameObject stage in objects)
        {
            if (stage.TryGetComponent(out IObjectAnimation objectAnimation))
            {
                objectAnimation.PlayAnimation();
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
