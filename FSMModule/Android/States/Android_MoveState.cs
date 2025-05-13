using UnityEngine;

public abstract class Android_MoveState : AndroidState
{
    private Vector3 _targetOffset; // Offset for the current mob
    private bool _offsetInitialized = false; // Flag to initialize offset

    public void MoveToTarget(float speed)
    {
        // Initialize the offset only once
        if (!_offsetInitialized)
        {
            _targetOffset = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
            _offsetInitialized = true;
        }

        Vector3 targetPositionWithOffset = Vector3.zero;

        // Target position given offset
        if (Android.CarTarget != null)
            targetPositionWithOffset = Android.CarTarget.position + _targetOffset;
        else if (Android.PlayerTarget != null)
            targetPositionWithOffset = Android.PlayerTarget.position;


        // Calculate the direction to the target
        Vector3 direction = (targetPositionWithOffset - transform.position).normalized;

        // Calculate the distance to the target
        float distanceToTarget = Vector3.Distance(transform.position, targetPositionWithOffset);

        // If the distance to the target is very small, we consider that the mob has reached the target
        if (distanceToTarget < 2.3f)
            // Если мы достаточно близко к цели, останавливаем движение
            return;

        // Moving towards the goal
        transform.position += direction * speed * Time.deltaTime;
        // Turn the mob towards the target
        transform.LookAt(targetPositionWithOffset);
    }
}
