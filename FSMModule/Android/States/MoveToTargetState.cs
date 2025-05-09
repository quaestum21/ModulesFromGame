using UnityEngine;

public abstract class MoveToTargetState : AndroidState
{
    private const float _targetOffsetValue = 3f;
    private Vector3 _targetOffset; // Смещение для текущего моба
    private bool _offsetInitialized = false; // Флаг для инициализации смещения

    public void MoveToTarget(float speed)
    {
        if (Android.CarTarget == null)
            return;

        // Инициализируем смещение только один раз
        if (!_offsetInitialized)
        {
            _targetOffset = new Vector3(Random.Range(-1 * _targetOffsetValue, _targetOffsetValue), 0, Random.Range(-1 * _targetOffsetValue, _targetOffsetValue));
            _offsetInitialized = true;
        }

        // Целевая позиция с учетом смещения
        Vector3 targetPositionWithOffset = Android.PlayerTarget.position + _targetOffset;

        // Направление к смещенной позиции
        Vector3 direction = (targetPositionWithOffset - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPositionWithOffset);

        // Двигаемся к цели
        transform.position += direction * speed * Time.deltaTime;

        // Поворачиваем моба к цели с учетом смещения
        transform.LookAt(targetPositionWithOffset);
    }
}
