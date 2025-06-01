using System.Collections;
using UnityEngine;

public class AttackCooldownHandler : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;

    public bool IsAttackReady { get; private set; }

    private void OnEnable()
    {
        IsAttackReady = true;
    }

    public IEnumerator CooldownRoutine()
    {
        IsAttackReady = false;
        yield return new WaitForSeconds(_attackCooldown);
        IsAttackReady = true;
    }
}