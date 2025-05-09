using UnityEngine;

public class Android_AttackState : AndroidState
{
    [SerializeField] private float _attackTimeDelay; // �������� ����� �������� ������
    private Vector3 _targetOffset; // �������� ��� �������� ����
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
        // ������� ������� � ������ ��������
        Vector3 targetPositionWithOffset = Android.PlayerTarget.position + _targetOffset;

        // ����������� � ��������� �������
        Vector3 direction = (targetPositionWithOffset - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPositionWithOffset);

        // ������������ ���� � ���� � ������ ��������
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
