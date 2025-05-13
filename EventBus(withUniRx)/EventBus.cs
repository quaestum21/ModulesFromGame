using System;
using UniRx;
using System.Collections.Generic;

public static class EventBus
{
    //Subject Storage by Event Type
    private static readonly Dictionary<Type, object> _subjects = new Dictionary<Type, object>();

    // Subscribe to events of a certain type
    public static IObservable<T> OnEvent<T>()
    {
        var type = typeof(T);
        if (!_subjects.ContainsKey(type))
        {
            _subjects[type] = new Subject<T>();
        }
        return ((Subject<T>)_subjects[type]).AsObservable();
    }

    // Publish event
    public static void Publish<T>(T eventData)
    {
        var type = typeof(T);
        if (_subjects.TryGetValue(type, out var subject))
        {
            ((Subject<T>)subject).OnNext(eventData);
        }
    }
    // Cleanup (e.g. when changing scene)
    public static void Clear()
    {
        _subjects.Clear();
    }
}
