using System.Collections;
using UnityEngine;

public class Android_JumpObstacleState : AndroidState
{
    private const float _jumpDuration = 1.0f;
    private bool isJumping = false;
    private float jumpForce = 4f; // ���� ������ ������
    private float jumpHeight = 1.5f; // ������ ������
    private Vector3 jumpDirection;

    void OnEnable() => StartJump();
    public void StartJump() => StartCoroutine(PerformJump());
    private IEnumerator PerformJump()
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        // ������ ����������� ������ ������ (�� ����������� �������� �������)
        jumpDirection = transform.forward; // ��� ����������� ������ �����������, ���� �����

        float targetDistance = jumpForce; // ��������� ������ ������
        float targetHeight = jumpHeight; // ������������ ������ ������

        while (true)
        {
            float elapsedTime = Time.time - startTime;
            float progress = elapsedTime / _jumpDuration; // ������������ ������ (1 �������)

            if (progress >= _jumpDuration)
            {
                // ��������� ������
                isJumping = false;
                transform.position = startPosition + jumpDirection * targetDistance;
                yield break;
            }

            // ������������ ������ ������ (��������)
            float height = Mathf.Sin(progress * Mathf.PI) * targetHeight;

            // ���������� ������
            transform.position = startPosition + jumpDirection * targetDistance * progress + Vector3.up * height;

            yield return null;
        }
    }
}
