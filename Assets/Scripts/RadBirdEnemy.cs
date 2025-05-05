using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadBirdEnemy : Enemy
{
    [SerializeField] private float _damage;

    private void Update()
    {
        if (AttackCooldownHandler.CanAttack)
        {
            Attacker.Attack(_damage);
            StartCoroutine(AttackCooldownHandler.CooldownRoutine());
        }
    }
}                                           