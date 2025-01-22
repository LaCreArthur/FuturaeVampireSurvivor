using UnityEngine;

public class MoveTowardPlayerPoolable : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;

    void Update()
    {
        Vector3 targetPos = PlayerController.PlayerTransform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
