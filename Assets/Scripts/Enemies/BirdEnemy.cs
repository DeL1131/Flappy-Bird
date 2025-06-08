using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CooldownTimer))]

public class BirdEnemy : MonoBehaviour, IDamagable, IInteractable
{
    [SerializeField] protected float AttackCooldown = 1;

    protected Health Health;
    protected CollisionHandler CollisionHandler;
    protected Rigidbody2D Rigidbody2D;
    protected CooldownTimer AttackCooldownHandler;

    public event Action<BirdEnemy> OnCollided;
    public event Action<float> Damaged;
    public event Action<BirdEnemy> OnDied;

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
        AttackCooldownHandler = GetComponent<CooldownTimer>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CollisionHandler = GetComponent<CollisionHandler>();
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

    public virtual void Reset()
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