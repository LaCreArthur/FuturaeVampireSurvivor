using DG.Tweening;
using UnityEngine;

public class DOTweenMovingWall : MonoBehaviour
{
    [SerializeField] Ease ease;
    [SerializeField] float distance = 10f;

    Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.position;
        StartTween();
    }

    // this moves the transform back and forth on the z-axis
    // using the distance and ease values
    // the tween is set to loop indefinitely in a yoyo fashion
    void StartTween() => transform.DOMoveZ(distance, 1).SetEase(ease).SetLoops(-1, LoopType.Yoyo);

    #if UNITY_EDITOR
    Ease _previousEase;
    float _previousDistance;
    void Update()
    {
        if (ease != _previousEase || !Mathf.Approximately(distance, _previousDistance))
        {
            _previousDistance = distance;
            _previousEase = ease;
            DOTween.Kill(transform);
            transform.position = _initialPosition;
            StartTween();
        }
    }

    #endif
}
