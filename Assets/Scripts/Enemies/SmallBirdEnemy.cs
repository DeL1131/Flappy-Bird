using UnityEngine;

[RequireComponent(typeof(DashAbility))]

public class SmallBirdEnemy : BirdEnemy
{
    private DashAbility _dashAbility;

    protected override void Awake()
    {
        base.Awake();
        _dashAbility = GetComponent<DashAbility>();
    }

    public override void Reset()
    {
        base.Reset();
        _dashAbility.Reset();
    }
}