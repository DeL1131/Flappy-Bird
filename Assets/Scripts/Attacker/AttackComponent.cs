using UnityEngine;

public abstract class AttackComponent : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] protected LayerMask LayerMask;

    public abstract void ExecuteAttack();
}