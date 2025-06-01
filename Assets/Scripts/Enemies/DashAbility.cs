using System.Collections;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    private float _delay = 1f;
    private float _speed = 20;
    private bool _isAbilityActive = false;

    public bool IsAbilityActive => _isAbilityActive;

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
        StartCoroutine(DelayDash());
    }

    public void Reset()
    {
        _isAbilityActive = false;
    }

    private void ActivateAbility()
    {
        _isAbilityActive = true;
    }

    private IEnumerator DelayDash()
    {
        yield return new WaitForSeconds(_delay);
        ActivateAbility();        
    }
}