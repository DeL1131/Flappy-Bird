using System.Collections;
using UnityEngine;

public class AttackCooldownHandler : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;

    public bool CanAttack { get; private set; }

    private void OnEnable()
    {
        CanAttack = true;
    }

    public IEnumerator CooldownRoutine()
    {
        CanAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        CanAttack = true;
    }
}