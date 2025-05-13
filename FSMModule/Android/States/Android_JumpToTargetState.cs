using System.Collections;
using UnityEngine;

public class Android_JumpToTargetState : AndroidState
{
    private Vector3 _jumpStartPosition;
    private Vector3 _jumpEndPosition;
    private float _jumpStartTime;
    private bool _isJumping;
    private CarAttachPoints _targetAttachPoints;
    Transform transformForAttach;

    private void OnEnable()
    {
        _targetAttachPoints = Android.CarTarget.gameObject.GetComponent<CarAttachPoints>();
        transformForAttach = _targetAttachPoints.GetTransformByReferens(this.Android);

        _isJumping = false;

        if (transformForAttach != null)
            StartCoroutine(Jump());
    }

    private void Update()
    {
        JumpToTarget();
    }

    private void JumpToTarget()
    {
        if (Android.MobHealth.CurrentHealth < 0)
        {
            StopAllCoroutines();
            _targetAttachPoints.ClearPlaceForNewMob(this.Android);
            return;
        }
        // Target position given offset
        Vector3 targetPosition = Android.PlayerTarget.position;

        // Direction to offset position
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Turn the mob towards the target taking into account the offset
        transform.LookAt(targetPosition);
    }

    private IEnumerator Jump()
    {
        gameObject.GetComponent<AndroidSounds>().PlayClipOneShot(AndroidClipType.Jump);
        if (transformForAttach == null) yield break;
        // Wait for the animation to start
        _jumpStartPosition = transform.position;
        _jumpStartTime = Time.time;
        _isJumping = true;

        float jumpDuration = Android.JumpDuration;

        for (float t = 0; t < jumpDuration; t += Time.deltaTime)
        {
            float progress = t / jumpDuration;

            // **Update the final position every frame if the point is moving***
            _jumpEndPosition = transformForAttach.position;

            Vector3 currentPosition = Vector3.Lerp(_jumpStartPosition, _jumpEndPosition, progress);
            currentPosition.y += Mathf.Sin(progress * Mathf.PI) * Android.JumpForce; // Параболическая траектория

            transform.position = currentPosition;
            yield return null;
        }
        // Final mob pinning
        AttachToTarget();
    }
    private void AttachToTarget()
    {
        transform.parent = Android.CarTarget;
        Debug.Log("закрепление");
        _isJumping = false;
    }
}