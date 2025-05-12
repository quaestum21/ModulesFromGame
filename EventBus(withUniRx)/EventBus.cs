using System;
using UniRx;
using System.Collections.Generic;

public static class EventBus
{
    // Хранилище Subjectов по типу событий
    private static readonly Dictionary<Type, object> _subjects = new Dictionary<Type, object>();

    // Подписка на события определённого типа
    public static IObservable<T> OnEvent<T>()
    {
        var type = typeof(T);
        if (!_subjects.ContainsKey(type))
        {
            _subjects[type] = new Subject<T>();
        }
        return ((Subject<T>)_subjects[type]).AsObservable();
    }

    // Публикация события
    public static void Publish<T>(T eventData)
    {
        var type = typeof(T);
        if (_subjects.TryGetValue(type, out var subject))
        {
            ((Subject<T>)subject).OnNext(eventData);
        }
    }

    // Очистка (например, при смене сцены)
    public static void Clear()
    {
        _subjects.Clear();
    }
}
