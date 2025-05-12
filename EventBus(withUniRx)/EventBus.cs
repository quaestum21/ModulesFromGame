using System;
using UniRx;
using System.Collections.Generic;

public static class EventBus
{
    // ��������� Subject�� �� ���� �������
    private static readonly Dictionary<Type, object> _subjects = new Dictionary<Type, object>();

    // �������� �� ������� ������������ ����
    public static IObservable<T> OnEvent<T>()
    {
        var type = typeof(T);
        if (!_subjects.ContainsKey(type))
        {
            _subjects[type] = new Subject<T>();
        }
        return ((Subject<T>)_subjects[type]).AsObservable();
    }

    // ���������� �������
    public static void Publish<T>(T eventData)
    {
        var type = typeof(T);
        if (_subjects.TryGetValue(type, out var subject))
        {
            ((Subject<T>)subject).OnNext(eventData);
        }
    }

    // ������� (��������, ��� ����� �����)
    public static void Clear()
    {
        _subjects.Clear();
    }
}
