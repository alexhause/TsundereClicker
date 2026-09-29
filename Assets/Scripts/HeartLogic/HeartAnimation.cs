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

    private Tweener clickTweener;

    private void Awake()
    {
        clickHandler.OnClick += PlayAnimation;
    }

    private void OnDestroy()
    {
        clickHandler.OnClick -= PlayAnimation;
    }

    private void PlayAnimation()
    {
        // Если твин активен, плавно или мгновенно останавливаем его
        if (clickTweener != null && clickTweener.IsActive())
        {
            clickTweener.Kill();
        }

        // Сбрасываем размер
        transform.localScale = Vector3.one;

        // Запускаем твин «туда» и сразу через OnComplete запускаем твин «обратно»
        clickTweener = transform.DOScale(targetScale, squeezeDuration)
            .SetEase(squeezeEase)
            .OnComplete(() =>
            {
                // Когда сжатие завершилось, возвращаем размер назад
                clickTweener = transform.DOScale(1f, returnDuration).SetEase(returnEase);
            });
    }
}
