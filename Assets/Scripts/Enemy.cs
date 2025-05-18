using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyCollisionHandler))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AttackCooldownHandler))]

public abstract class Enemy : MonoBehaviour, IDamagable
{
    protected Health Health;
    protected EnemyCollisionHandler CollisionHandler;
    protected Rigidbody2D Rigidbody2D;
    protected AttackCooldownHandler AttackCooldownHandler;

    public event Action<Enemy> OnCollided;
    public event Action<float> Damaged;
    public event Action<Enemy> OnDied;

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
        AttackCooldownHandler = GetComponent<AttackCooldownHandler>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CollisionHandler = GetComponent<EnemyCollisionHandler>();
    }

    private void OnEnable()
    {
        Health.Die += Die;
        CollisionHandler.Collided += ProcessCollision;        
    }

    private void OnDisable()
    {
        Health.Die -= Die;
        CollisionHandler.Collided -= ProcessCollision;
    }

    public void Reset()
    {
        Rigidbody2D.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        Health.Reset();
    }

    public void TakeDamage(float damage)
    {
        Health.DamageHealth(damage);
    }

    private void Die()
    {
        OnDied?.Invoke(this);
    }

    private void ProcessCollision(Component collision)
    {
        if (collision.TryGetComponent(out Wall wall))
        {
            OnCollided?.Invoke(this);
        }
    }
}