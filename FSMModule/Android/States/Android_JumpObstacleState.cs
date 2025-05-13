using System.Collections;
using UnityEngine;

public class Android_JumpObstacleState : AndroidState
{
    private const float _jumpDuration = 1.0f;
    private bool isJumping = false;
    private float jumpForce = 4f;// Forward Jump Force
    private float jumpHeight = 1.5f;
    private Vector3 jumpDirection;

    void OnEnable() => StartJump();
    public void StartJump() => StartCoroutine(PerformJump());
    private IEnumerator PerformJump()
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        // Set the direction of the jump forward (in the direction of the object's movement)
        jumpDirection = transform.forward; // Or use another direction if needed

        float targetDistance = jumpForce; // Forward jump distance
        float targetHeight = jumpHeight; // Maximum jump height

        while (true)
        {
            float elapsedTime = Time.time - startTime;
            float progress = elapsedTime / _jumpDuration; // Jump duration (1 second)
            if (progress >= _jumpDuration)
            {
                // ending jump
                isJumping = false;
                transform.position = startPosition + jumpDirection * targetDistance;
                yield break;
            }

            // Calculate the jump height (parabola)
            float height = Mathf.Sin(progress * Mathf.PI) * targetHeight;

            // Move the object
            transform.position = startPosition + jumpDirection * targetDistance * progress + Vector3.up * height;

            yield return null;
        }
    }
}
