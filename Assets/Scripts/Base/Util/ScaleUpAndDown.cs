using UnityEngine;
using DG.Tweening;

public class ScaleUpAndDown : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f);
    public float duration = 0.3f;
    public bool loop = false;

    private Vector3 originalScale;
    private Tween scaleTween;

    void Start()
    {
        originalScale = transform.localScale;

        // Optional: Start scaling immediately
        PlayScaleEffect();
    }

    public void PlayScaleEffect()
    {
        if (scaleTween != null && scaleTween.IsActive()) scaleTween.Kill();

        scaleTween = transform.DOScale(targetScale, duration / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOScale(originalScale, duration / 2)
                    .SetEase(Ease.InQuad)
                    .SetLoops(loop ? -1 : 0, LoopType.Yoyo);
            });
    }

    public void StopScaleEffect()
    {
        if (scaleTween != null) scaleTween.Kill();
        transform.localScale = originalScale;
    }
}