using UnityEngine;

[RequireComponent (typeof(RangeAttacker))]

public class RadBirdEnemy : BirdEnemy
{
    private RangeAttacker _attacker;

    protected override void Awake()
    {
        base.Awake();
        _attacker = GetComponent<RangeAttacker>();
    }

    private void Update()
    {
        if (AttackCooldownHandler.IsReady)
        {
            AttackCooldownHandler.StartTimer(AttackCooldown);
            _attacker.ExecuteAttack();
        }
    }
}                                           