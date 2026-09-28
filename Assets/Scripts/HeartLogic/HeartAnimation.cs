using DG.Tweening;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    [SerializeField] ClickHandler clickHandler;

    [Header("Scale Settings")]
    [Tooltip("Максимальный размер сердца при клике (1.15 = +15%)")]
    [SerializeField] private float targetScale = 1.15f;

    [Header("Timing Settings")]
    [Tooltip("Время быстрого расширения")]
    [SerializeField] private float squeezeDuration = 0.05f;

    [Tooltip("Время возвращения в исходный размер")]
    [SerializeField] private float returnDuration = 0.2f;

    [Header("Easing Settings")]
    [Tooltip("Тип сглаживания для расширения (рекомендуется OutQuad)")]
    [SerializeField] private Ease squeezeEase = Ease.OutQuad;

    [Tooltip("Тип сглаживания для возврата (рекомендуется OutElastic или OutBack)")]
    [SerializeField] private Ease returnEase = Ease.OutElastic;

    private void Start()
    {
        clickHandler.OnClick += PlayAnimation;
    }

    private void OnDestroy()
    {
        clickHandler.OnClick -= PlayAnimation;
    }

    private void PlayAnimation()
    {
        // Сбрасываем масштаб и убиваем прошлый твин, чтобы быстрые клики не ломали логику
        transform.DOKill();
        transform.localScale = Vector3.one;

        // Запуск последовательности с переменными из инспектора
        Sequence clickSequence = DOTween.Sequence();
        clickSequence.Append(transform.DOScale(targetScale, squeezeDuration).SetEase(squeezeEase));
        clickSequence.Append(transform.DOScale(1f, returnDuration).SetEase(returnEase));
    }
}
