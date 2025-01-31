using UnityEngine;

public class WanderMove : MoveBehavior
{
    [SerializeField] float nextMoveCooldown;
    [SerializeField] float nextMoveRadius;

    Vector3 _targetPos;
    float _nextMoveTime;

    public override void Move()
    {
        _nextMoveTime -= Time.deltaTime;
        if (_nextMoveTime <= 0)
        {
            SetNewTarget();
            _nextMoveTime = nextMoveCooldown;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);
    }

    void SetNewTarget()
    {
        var randomPos = new Vector3(Random.Range(-nextMoveRadius, nextMoveRadius), Random.Range(-nextMoveRadius, nextMoveRadius), 0);
        _targetPos = transform.position + randomPos;
    }
}
