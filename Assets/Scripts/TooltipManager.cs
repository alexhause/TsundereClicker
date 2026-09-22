using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipManager : MonoBehaviour
{
    #region Singleton
    public static TooltipManager Instance;

    private void Awake()
    {
        // Исправлено условие: если Instance уже существует и это НЕ этот скрипт, уничтожаем дубликат
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    [SerializeField] private RectTransform tooltip; // Ссылка на выключенный объект TooltipBox
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector2 cursorOffset = new Vector2(10, -10);
    [SerializeField] private float showDelay = 0.5f;

    private Coroutine delayCoroutine;

    private void Start()
    {
        // ИСПРАВЛЕНО: Выключаем конкретно объект тултипа, а не сам менеджер!
        if (tooltip != null)
        {
            tooltip.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // ОПТИМИЗАЦИЯ: Если тултип сейчас скрыт, нет смысла тратить ресурсы на подсчет позиции мыши
        if (tooltip == null || !tooltip.gameObject.activeSelf) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        // ИСПРАВЛЕНО: Берём родителя самого ТУЛТИПА, а не менеджера
        RectTransform parentRect = tooltip.parent as RectTransform;

        if (parentRect != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                mousePos,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            // ИСПРАВЛЕНО: Двигаем сам объект тултипа
            tooltip.anchoredPosition = localPoint + cursorOffset;
        }
    }

    public void SetText(string tooltipTxt)
    {
        tooltipText.text = tooltipTxt;
    }

    public void Show()
    {
        if (delayCoroutine != null) StopCoroutine(delayCoroutine);
        delayCoroutine = StartCoroutine(ShowWithDelayCoroutine());
    }

    public void Hide()
    {
        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
            delayCoroutine = null;
        }
        // ИСПРАВЛЕНО: Выключаем объект тултипа
        if (tooltip != null) tooltip.gameObject.SetActive(false);
    }

    private IEnumerator ShowWithDelayCoroutine()
    {
        yield return new WaitForSeconds(showDelay);

        // ИСПРАВЛЕНО: Включаем объект тултипа
        if (tooltip != null) tooltip.gameObject.SetActive(true);
        delayCoroutine = null;
    }
}
