using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomObjectPool<T> where T : MonoBehaviour
{
    private readonly Func<T> _preloadFunc;
    private readonly Action<T> _getAction;
    private readonly Action<T> _releaseAction;


    private Queue<T> _objects = new Queue<T>();

    public CustomObjectPool(Func<T> preloadFunc, Action<T> getAction, Action<T> releaseAction, int defaultObjectsCount)
    {
        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _releaseAction = releaseAction;

        for(int i = 0; i < defaultObjectsCount; i++)
            Release(preloadFunc());
    }

    public T Get()
    {
        T tempObject = _objects.Count > 0 ? _objects.Dequeue() : _preloadFunc();
        _getAction(tempObject);
        return tempObject;
    }

    public void Release(T tempObject)
    {
        _releaseAction(tempObject);
        _objects.Enqueue(tempObject);
    }
}
