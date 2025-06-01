using System;
using System.Collections;
using UnityEngine;

public class CooldownTimer : MonoBehaviour
{
    public bool IsReady { get; private set; }

    public event Action Completed;

    private void OnEnable()
    {
        IsReady = true;
    }

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