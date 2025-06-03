using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Collider2D> Collided;

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        Collided?.Invoke(collision);
    }
}