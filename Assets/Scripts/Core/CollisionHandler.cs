using System;
using UnityEngine;

public abstract class CollisionHandler<T> : MonoBehaviour where T : Component
{
    public event Action<T> Collided;

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected virtual void InvokeCollision(T collision)
    {
        Collided?.Invoke(collision);
    }
}