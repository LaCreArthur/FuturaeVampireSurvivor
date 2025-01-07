using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BirdAnimation : MonoBehaviour
{
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        GameManager.OnGameStart += PlayFly;
        GameManager.OnGamePause += StopFly;
        GameManager.OnGameResume += PlayFly;
        GameManager.OnGameEnd += StopFly;
    }

    void OnDestroy()
    {
        GameManager.OnGameStart -= PlayFly;
        GameManager.OnGamePause -= StopFly;
        GameManager.OnGameResume -= PlayFly;
        GameManager.OnGameEnd -= StopFly;
    }

    void PlayFly() => _animator.SetBool("InGame", true);
    void StopFly() => _animator.SetBool("InGame", false);
}
