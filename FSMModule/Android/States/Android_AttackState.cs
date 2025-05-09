using UnityEngine;

public class Android_AttackState : AndroidState
{
    [SerializeField] private float _attackTimeDelay; // задержка перед следущей атакой
    private Vector3 _targetOffset; // Смещение для текущего моба
    private IHealth _targetHealth;
    private float _time;
    private void OnEnable()
    {
        _targetHealth = Android.PlayerTarget.gameObject.GetComponent<PlayerHealth>();
        _time = 0;
    }
    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        // Целевая позиция с учетом смещения
        Vector3 targetPositionWithOffset = Android.PlayerTarget.position + _targetOffset;

        // Направление к смещенной позиции
        Vector3 direction = (targetPositionWithOffset - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPositionWithOffset);

        // Поворачиваем моба к цели с учетом смещения
        transform.LookAt(targetPositionWithOffset);

        _time += Time.deltaTime;
        if (_time > _attackTimeDelay)
        {
            _time = 0;

            if (Android.TypeOfMove == MovementAnroidType.Walk)
                _targetHealth.ApplyDamage(10);

            else
            {
                float damage = float.Parse(Android.MobStat.damage);
                _targetHealth.ApplyDamage(damage);
            }
            gameObject.GetComponent<AndroidSounds>().PlayClipOneShot(AndroidClipType.Attack);
        }
    }
}
