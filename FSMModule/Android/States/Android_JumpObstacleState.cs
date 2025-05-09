using System.Collections;
using UnityEngine;

public class Android_JumpObstacleState : AndroidState
{
    private const float _jumpDuration = 1.0f;
    private bool isJumping = false;
    private float jumpForce = 4f; // Сила прыжка вперед
    private float jumpHeight = 1.5f; // Высота прыжка
    private Vector3 jumpDirection;

    void OnEnable() => StartJump();
    public void StartJump() => StartCoroutine(PerformJump());
    private IEnumerator PerformJump()
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        // Задаем направление прыжка вперед (по направлению движения объекта)
        jumpDirection = transform.forward; // Или используйте другое направление, если нужно

        float targetDistance = jumpForce; // Дистанция прыжка вперед
        float targetHeight = jumpHeight; // Максимальная высота прыжка

        while (true)
        {
            float elapsedTime = Time.time - startTime;
            float progress = elapsedTime / _jumpDuration; // Длительность прыжка (1 секунда)

            if (progress >= _jumpDuration)
            {
                // Завершаем прыжок
                isJumping = false;
                transform.position = startPosition + jumpDirection * targetDistance;
                yield break;
            }

            // Рассчитываем высоту прыжка (парабола)
            float height = Mathf.Sin(progress * Mathf.PI) * targetHeight;

            // Перемещаем объект
            transform.position = startPosition + jumpDirection * targetDistance * progress + Vector3.up * height;

            yield return null;
        }
    }
}
