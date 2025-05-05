using UnityEngine;
using System.Collections;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected float SmoothDecreaseDuration = 1f;
    [SerializeField] private CanvasGroup _windowGroup;

    private float _alphaTarget;

    protected CanvasGroup WindowGroup => _windowGroup;

    protected abstract void OnButtonClick();

    public virtual void Close()
    {
        WindowGroup.interactable = false;
        _alphaTarget = 0;
        StartCoroutine(FadeCanvasGroup(_alphaTarget));
    }

    public virtual void Open()
    {
        WindowGroup.interactable = true;
        _alphaTarget = 1;
        StartCoroutine(FadeCanvasGroup(_alphaTarget));
    }

    private IEnumerator FadeCanvasGroup(float target)
    {
        float elapsedTime = 0f;
        float previousValve = WindowGroup.alpha;

        while (elapsedTime < SmoothDecreaseDuration)
        {
            elapsedTime += Time.fixedDeltaTime;
            float normalizedPosition = elapsedTime / SmoothDecreaseDuration;
            float intermediateValue = Mathf.Lerp(previousValve, target, normalizedPosition);
            WindowGroup.alpha = intermediateValue;

            yield return null;
        }
    }
}