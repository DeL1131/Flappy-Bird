using System;
using System.Collections;
using UnityEngine;

public class CooldownTimer : MonoBehaviour
{
    public bool IsReady { get; private set; } = true;

    public event Action Completed;

    public void StartTimer(float duration)
    {
        if (IsReady)
            StartCoroutine(TimerCoroutine(duration));
    }

    private IEnumerator TimerCoroutine(float duration)
    {
        IsReady = false;
        yield return new WaitForSeconds(duration);
        IsReady = true;
        Completed?.Invoke();
    }

}