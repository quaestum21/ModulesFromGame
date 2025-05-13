using UnityEngine;

public abstract class MoveToTargetState : AndroidState
{
    private const float _targetOffsetValue = 3f;
    private Vector3 _targetOffset; // Offset for the current mob
    private bool _offsetInitialized = false; // Flag to initialize offset

    public void MoveToTarget(float speed)
    {
        if (Android.CarTarget == null)
            return;

        // Initialize the offset only once
        if (!_offsetInitialized)
        {
            _targetOffset = new Vector3(Random.Range(-1 * _targetOffsetValue, _targetOffsetValue), 0, Random.Range(-1 * _targetOffsetValue, _targetOffsetValue));
            _offsetInitialized = true;
        }

        // Target position given offset
        Vector3 targetPositionWithOffset = Android.PlayerTarget.position + _targetOffset;

        // Direction to offset position
        Vector3 direction = (targetPositionWithOffset - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPositionWithOffset);

        // Moving towards the goal
        transform.position += direction * speed * Time.deltaTime;

        // Turn the mob towards the target taking into account the offset
        transform.LookAt(targetPositionWithOffset);
    }
}
