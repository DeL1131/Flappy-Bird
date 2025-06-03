using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private IDamagable _damagable;

    public event Action<float> HealhChanged;
    public event Action Die;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _damagable = GetComponent<IDamagable>();

        _damagable.Damaged += DamageHealth;
    }

    private void OnDisable()
    {
        _damagable.Damaged -= DamageHealth;
    }

    public void Reset()
    {
        CurrentHealth = _maxHealth;
    }

    public void DamageHealth(float damage)
    {
        if (damage >= 0)
        {
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);

            HealhChanged?.Invoke(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                Die?.Invoke();
            }
        }
    }

    public void Heal(float healAmount)
    {
        if (healAmount >= 0)
        {
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);

            HealhChanged?.Invoke(CurrentHealth);
        }
    }
}