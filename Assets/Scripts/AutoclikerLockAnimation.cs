using UnityEngine;
using DG.Tweening;

public class AutoclikerLockAnimation : MonoBehaviour
{
    [Header("Настройки анимации")]
    [SerializeField] private float duration = 1.0f;     // Время одного цикла покачивания
    [SerializeField] private float jumpPower = 15.0f;    // Высота покачивания 

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Start()
    {
        UpgradeManager.Instance.OnNoCoinForUnlock += Instance_OnNoCoinForUnlock;
    }

    private void OnDestroy()
    {
        UpgradeManager.Instance.OnNoCoinForUnlock -= Instance_OnNoCoinForUnlock;
    }


    private void Instance_OnNoCoinForUnlock()
    {
        PlayAnimation();
    }


    private void PlayAnimation()
    {
        // 1. Мгновенно останавливаем предыдущую анимацию, если она шла
        transform.DOKill();

        // 2. Принудительно возвращаем замок в исходную точку перед стартом нового твина
        transform.position = startPosition;

        // 3. Запускаем покачивание вверх-вниз относительно стартовой точки
        transform.DOMoveY(startPosition.y + jumpPower, duration)
            .SetEase(Ease.OutQuad)       // Плавное замедление в верхней точке
            .SetLoops(2, LoopType.Yoyo); // Движение вверх и возврат обратно в старт
    }
}
