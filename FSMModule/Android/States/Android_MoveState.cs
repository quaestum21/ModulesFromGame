using UnityEngine;

public abstract class Android_MoveState : AndroidState
{
    private Vector3 _targetOffset; // Смещение для текущего моба
    private bool _offsetInitialized = false; // Флаг для инициализации смещения

    public void MoveToTarget(float speed)
    {
        // Инициализируем смещение только один раз
        if (!_offsetInitialized)
        {
            _targetOffset = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
            _offsetInitialized = true;
        }

        Vector3 targetPositionWithOffset = Vector3.zero;

        // Целевая позиция с учетом смещения
        if (Android.CarTarget != null)
            targetPositionWithOffset = Android.CarTarget.position + _targetOffset;
        else if (Android.PlayerTarget != null)
            targetPositionWithOffset = Android.PlayerTarget.position;
        

        // Вычисляем направление к цели
        Vector3 direction = (targetPositionWithOffset - transform.position).normalized;

        // Вычисляем расстояние до цели
        float distanceToTarget = Vector3.Distance(transform.position, targetPositionWithOffset);

        // Если расстояние до цели очень маленькое, считаем, что моб достиг цели
        if (distanceToTarget < 2.3f)
            // Если мы достаточно близко к цели, останавливаем движение
            return;
        
        // Двигаемся к цели
        transform.position += direction * speed * Time.deltaTime;
        // Поворачиваем моба к цели
        transform.LookAt(targetPositionWithOffset);
    }
}
