using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CooldownTimer))]

public class DashAbility : MonoBehaviour
{
    private CooldownTimer _cooldownTimer;

    private float _delay = 2f;
    private float _speed = 20;
    private bool _isAbilityActive = false;

    public bool IsAbilityActive => _isAbilityActive;

    private void Awake()
    {
        _cooldownTimer = GetComponent<CooldownTimer>();
    }

    private void Update()
    {
        if (_isAbilityActive)
        {
            Vector3 fixedPosition = transform.position;
            fixedPosition.z = 0;
            transform.position = fixedPosition;

            transform.position += transform.right * -_speed * Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        _cooldownTimer.StartTimer(_delay);
        _cooldownTimer.Completed += ActivateAbility;
    }

    private void OnDisable()
    {
        _cooldownTimer.Completed -= ActivateAbility;
    }

    public void Reset()
    {
        _isAbilityActive = false;
    }

    private void ActivateAbility()
    {
        _isAbilityActive = true;
    }
}