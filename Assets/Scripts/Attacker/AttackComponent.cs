using System;
using UnityEngine;

public abstract class AttackComponent : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] protected LayerMask LayerMask;

    public event Action Attacked;
    public abstract void ExecuteAttack();

    protected void InvokeAttacked()
    {
        Attacked?.Invoke();
    }
}