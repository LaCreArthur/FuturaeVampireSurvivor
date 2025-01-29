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
    float _targetSize;
    bool _isLerp;

    void Start()
    {
        _camera = GetComponent<CinemachineCamera>();
        _currentSize = _camera.Lens.OrthographicSize;
        GameStateManager.OnHome += SetHomeSize;
        GameStateManager.OnPlaying += SetGameSize;
    }

    void Update()
    {
        if (!_isLerp) return;
        _currentSize = Mathf.Lerp(_currentSize, _targetSize, lerpSpeed * Time.deltaTime);
        _camera.Lens.OrthographicSize = _currentSize;
        if (Mathf.Abs(_currentSize - _targetSize) < 0.01f)
        {
            _isLerp = false;
            _camera.Lens.OrthographicSize = _targetSize;
        }
    }

    void OnDestroy()
    {
        GameStateManager.OnHome -= SetHomeSize;
        GameStateManager.OnPlaying -= SetGameSize;
    }

    void SetHomeSize()
    {
        _targetSize = homeSize;
        _isLerp = true;
    }

    void SetGameSize()
    {
        _targetSize = gameSize;
        _isLerp = true;
    }
}
