using UnityEngine;

public class WanderMoveController : MonoBehaviour
{
    Vector3 _targetPos;

    float _speed;
    float _nextMoveTime;
    float _nextMoveCooldown;
    float _nextMoveRadius;

    void Update()
    {
        _nextMoveTime -= Time.deltaTime;
        if (_nextMoveTime <= 0)
        {
            SetNewTarget(transform.position);
            _nextMoveTime = _nextMoveCooldown;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
    }

    public void Initialize(float speed, float nextMoveCooldown, float nextMoveRadius)
    {
        _speed = speed;
        _nextMoveCooldown = nextMoveCooldown;
        _nextMoveRadius = nextMoveRadius;
    }

    void SetNewTarget(Vector3 ownerPos) =>
        _targetPos =
            ownerPos + new Vector3(Random.Range(-_nextMoveRadius, _nextMoveRadius), Random.Range(-_nextMoveRadius, _nextMoveRadius), 0);
}
