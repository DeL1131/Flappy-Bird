using System.Collections.Generic;
using UnityEngine;

public class CustomObjectPool<T> where T : MonoBehaviour
{
    private readonly Queue<T> _availableObjects = new Queue<T>();
    private readonly HashSet<T> _activeObjects = new HashSet<T>();

    private readonly T _prefab;
    private readonly Transform _parentTransform;

    public CustomObjectPool(T prefab, Transform parentTransform = null)
    {
        _prefab = prefab;
        _parentTransform = parentTransform;
    }

    public T Get()
    {
        T instance;

        if (_availableObjects.Count > 0)
        {
            instance = _availableObjects.Dequeue();
            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Object.Instantiate(_prefab, _parentTransform);
        }

        _activeObjects.Add(instance);
        return instance;
    }

    public void ReturnToPool(T instance)
    {
        instance.transform.rotation = Quaternion.identity;
        instance.gameObject.SetActive(false);
        _activeObjects.Remove(instance);
        _availableObjects.Enqueue(instance);
    }

    public void DeactivateAll()
    {
        foreach (var obj in _activeObjects)
        {
            obj.gameObject.SetActive(false);
            _availableObjects.Enqueue(obj);
        }
        _activeObjects.Clear();
    }
}