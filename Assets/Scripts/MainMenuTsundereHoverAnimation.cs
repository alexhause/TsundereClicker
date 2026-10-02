using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuTsundereHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite mainSprite;
    [SerializeField] private Sprite secondSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        spriteRenderer.sprite = secondSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.sprite = mainSprite;
    }
}
