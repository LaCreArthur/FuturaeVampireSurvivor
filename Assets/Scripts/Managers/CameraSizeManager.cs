using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class CameraSizeManager : MonoBehaviour
{
    [SerializeField] float homeSize = 3;
    [SerializeField] float gameSize = 7;
    [SerializeField] float lerpSpeed = 5;

    CinemachineCamera _camera;
    float _currentSize;

    void Awake()
    {
        _camera = GetComponent<CinemachineCamera>();
        _currentSize = _camera.Lens.OrthographicSize;
        GameStateManager.OnHome += SetHomeSize;
        GameStateManager.OnPlaying += SetGameSize;
    }

    void OnDestroy()
    {
        GameStateManager.OnHome -= SetHomeSize;
        GameStateManager.OnPlaying -= SetGameSize;
    }

    IEnumerator TransitionRoutine(float targetSize)
    {
        while (Mathf.Abs(_currentSize - targetSize) > 0.01f)
        {
            _currentSize = Mathf.Lerp(_currentSize, targetSize, lerpSpeed * Time.deltaTime);
            _camera.Lens.OrthographicSize = _currentSize;
            yield return null;
        }
        _camera.Lens.OrthographicSize = targetSize;
    }

    void SetHomeSize() => StartCoroutine(TransitionRoutine(homeSize));
    void SetGameSize() => StartCoroutine(TransitionRoutine(gameSize));
}
