using System.Collections;
using UnityEngine;

public class RunAttackState : AttackState
{
    private Vector3 _jumpStartPosition;
    private Vector3 _jumpEndPosition;
    private float _jumpStartTime;
    private bool _isJumping;

    private void OnEnable()
    {
        _isJumping = false;
        StartCoroutine(Jump());
    }
    private void Update()
    {
        CheckCurrentState();
    }

    private void CheckCurrentState()
    {
        if (Android.MobHealth.CurrentHealth < 0)
        {
            StopAllCoroutines();
            return;
        }
        if (_isJumping)
            UpdateJump();
    }

    private IEnumerator Jump()
    {
        // Start position and target position
        _jumpStartPosition = transform.position;
        _jumpEndPosition = Android.CarTarget.position;
        _jumpStartTime = Time.time;
        _isJumping = true;

        // Wait for the duration of the jump
        yield return new WaitForSeconds(Android.JumpDuration);

        // Complete the jump, lock the position on the car and launch the attack
        AttachToTarget();
        CauseDamage();

        // Wait for a delay after the attack
        yield return new WaitForSeconds(Android.DelayAfterAttack);

        // Complete the attack and detach the robot from the car
        AttackEnded();
        DetachFromTarget();
    }
    private void UpdateJump()
    {
        // Calculate the jump progress
        float jumpProgress = (Time.time - _jumpStartTime) / Android.JumpDuration;
        Vector3 currentPosition = Vector3.Lerp(_jumpStartPosition, _jumpEndPosition, jumpProgress);

        // Jump height
        currentPosition.y += Mathf.Sin(jumpProgress * Mathf.PI) * Android.JumpForce;
        
        transform.position = currentPosition;

        // Update the final position (if the car is moving)
        _jumpEndPosition = Android.CarTarget.position;
    }
    private void AttachToTarget()
    {
        // Move the robot slightly back relative to the car's position
        Vector3 offsetPosition = Android.CarTarget.position - Android.CarTarget.forward * 0.7f; // Смещение на 1 метр назад (настраиваемое значение)
        transform.position = offsetPosition;

        // Make the car a parent object so that the robot moves with it
        transform.parent = Android.CarTarget;
        _isJumping = false;
    }
    private void DetachFromTarget() => transform.parent = null;
}
