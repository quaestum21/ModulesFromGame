using UnityEngine;

public abstract class Android_MoveState : AndroidState
{
    private Vector3 _targetOffset; // �������� ��� �������� ����
    private bool _offsetInitialized = false; // ���� ��� ������������� ��������

    public void MoveToTarget(float speed)
    {
        // �������������� �������� ������ ���� ���
        if (!_offsetInitialized)
        {
            _targetOffset = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
            _offsetInitialized = true;
        }

        Vector3 targetPositionWithOffset = Vector3.zero;

        // ������� ������� � ������ ��������
        if (Android.CarTarget != null)
            targetPositionWithOffset = Android.CarTarget.position + _targetOffset;
        else if (Android.PlayerTarget != null)
            targetPositionWithOffset = Android.PlayerTarget.position;
        

        // ��������� ����������� � ����
        Vector3 direction = (targetPositionWithOffset - transform.position).normalized;

        // ��������� ���������� �� ����
        float distanceToTarget = Vector3.Distance(transform.position, targetPositionWithOffset);

        // ���� ���������� �� ���� ����� ���������, �������, ��� ��� ������ ����
        if (distanceToTarget < 2.3f)
            // ���� �� ���������� ������ � ����, ������������� ��������
            return;
        
        // ��������� � ����
        transform.position += direction * speed * Time.deltaTime;
        // ������������ ���� � ����
        transform.LookAt(targetPositionWithOffset);
    }
}
