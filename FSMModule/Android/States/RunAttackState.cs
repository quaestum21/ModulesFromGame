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
        // Начальная позиция и позиция цели
        _jumpStartPosition = transform.position;
        _jumpEndPosition = Android.CarTarget.position;
        _jumpStartTime = Time.time;
        _isJumping = true;

        // Ждём длительность прыжка
        yield return new WaitForSeconds(Android.JumpDuration);

        // Завершаем прыжок, фиксируем позицию на машине и запускаем атаку
        AttachToTarget();
        CauseDamage();

        // Ждём задержку после атаки
        yield return new WaitForSeconds(Android.DelayAfterAttack);

        // Завершаем атаку и открепляем робота от машины
        AttackEnded();
        DetachFromTarget();
    }
    private void UpdateJump()
    {
        // Вычисляем прогресс прыжка
        float jumpProgress = (Time.time - _jumpStartTime) / Android.JumpDuration;
        Vector3 currentPosition = Vector3.Lerp(_jumpStartPosition, _jumpEndPosition, jumpProgress);
        
        // Высота прыжка
        currentPosition.y += Mathf.Sin(jumpProgress * Mathf.PI) * Android.JumpForce;
        
        transform.position = currentPosition;
        
        // Обновляем конечную позицию (если машина движется)
        _jumpEndPosition = Android.CarTarget.position;
    }
    private void AttachToTarget()
    {
        // Смещаем робота чуть назад относительно позиции машины
        Vector3 offsetPosition = Android.CarTarget.position - Android.CarTarget.forward * 0.7f; // Смещение на 1 метр назад (настраиваемое значение)
        transform.position = offsetPosition;

        // Делаем машину родительским объектом, чтобы робот двигался вместе с ней
        transform.parent = Android.CarTarget;
        _isJumping = false;
    }
    private void DetachFromTarget() => transform.parent = null;
}
