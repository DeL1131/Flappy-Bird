using UnityEngine;

[RequireComponent (typeof(RangeAttacker))]

public class RadBirdEnemy : Enemy
{
    private RangeAttacker _attacker;

    protected override void Awake()
    {
        base.Awake();
        _attacker = GetComponent<RangeAttacker>();
    }

    private void Update()
    {
        if (AttackCooldownHandler.CanAttack)
        {
            _attacker.ExecuteAttack();
            StartCoroutine(AttackCooldownHandler.CooldownRoutine());
        }
    }
}                                           