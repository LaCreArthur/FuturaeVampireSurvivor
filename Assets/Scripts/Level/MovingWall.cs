using DG.Tweening;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] Ease ease;
    [SerializeField] float zMove = 10f;
    [SerializeField] float duration = 1f;
    [SerializeField] float startDelay;

    Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.position;
        StartTween();
    }

    // this moves the transform back and forth on the z-axis
    // using the distance and ease values
    // the tween is set to loop indefinitely in a yoyo fashion
    // the SetRelative(true) method is used to move the object relative to its current position
    void StartTween() => transform.DOMoveZ(zMove, duration).SetEase(ease).SetLoops(-1, LoopType.Yoyo).SetRelative(true).SetDelay(startDelay);

    // this is only called in the editor
    // it checks if the values have changed and restarts the tween if they have
    // this is useful for testing the tween in the editor
    // but it is not necessary for the game to function
    #if UNITY_EDITOR
    Ease _previousEase;
    float _previousZMove;
    float _previousStartDelay;
    float _previousDuration;
    void Update()
    {
        if (ease != _previousEase || !Mathf.Approximately(zMove, _previousZMove) || !Mathf.Approximately(startDelay, _previousStartDelay) ||
            !Mathf.Approximately(duration, _previousDuration))
        {
            _previousDuration = duration;
            _previousZMove = zMove;
            _previousEase = ease;
            _previousStartDelay = startDelay;
            DOTween.Kill(transform);
            transform.position = _initialPosition;
            StartTween();
        }

    }
    #endif
}
