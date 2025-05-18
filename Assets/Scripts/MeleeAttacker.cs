using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : AttackType
{
    [SerializeField] private float _attackRadius;

    private float _visionAngle = 45;

    private List<IDamagable> _targets = new List<IDamagable>();

    public override void ExecuteAttack()
    {
        _targets = SearchTargets();

        if (_targets.Count > 0)
        {
            InvokeAttacked();

            foreach (var target in _targets)
            {
                target.TakeDamage(Damage);
            }
        }
    }

    private List<IDamagable> SearchTargets()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent(out IDamagable damagable) && LayerMask == (LayerMask | (1 << target.gameObject.layer)) && IsTargetInView(transform, target.transform))
            {
                _targets.Add(damagable);
            }
        }

        return _targets;
    }

    private bool IsTargetInView(Transform observer, Transform target)
    {
        Vector2 targetDirection = (target.position - observer.position).normalized;
        Vector2 forward = observer.right;

        float dot = Vector2.Dot(forward, targetDirection);

        float angleThreshold = Mathf.Cos(_visionAngle * 0.5f * Mathf.Deg2Rad);

        return dot >= angleThreshold;
    }
}