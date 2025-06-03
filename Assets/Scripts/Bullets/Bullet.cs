using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _damage;
    private CollisionHandler _collisionHandler;
    private LayerMask _layerMask;

    public event Action<Bullet> Returned;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        _collisionHandler.Collided += ProcessCollision;      
    }

    private void OnDisable()
    {
        _collisionHandler.Collided -= ProcessCollision;
    }

    private void Update()
    {
        Vector3 bulletPositionZ = transform.position;
        bulletPositionZ.z = 0;
        transform.position = bulletPositionZ;

        transform.position += transform.right * _speed * Time.deltaTime;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetLayerMask(LayerMask layerMask)
    {
        _layerMask = layerMask;
    }

    private void ProcessCollision(Component collision)
    {
        if(collision.TryGetComponent(out Wall wall))
        {
            Returned?.Invoke(this);
        }

        if (_layerMask == (_layerMask | (1 << collision.gameObject.layer)) && collision.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
            Returned?.Invoke(this);
        }
    }
}