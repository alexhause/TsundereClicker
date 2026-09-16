using UnityEngine;
using UnityEngine.EventSystems;

public class AutoclikerUnlockSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite unlockSprite;
    [SerializeField] private Sprite lockSprite;
    [SerializeField] UpgradeManager upgradeManager;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
       spriteRenderer.sprite = lockSprite;

        upgradeManager.OnAutoclickerUnlock += UpgradeManager_OnAutoclickerUnlock;
    }

    private void OnDestroy()
    {
        upgradeManager.OnAutoclickerUnlock -= UpgradeManager_OnAutoclickerUnlock;
    }

    private void UpgradeManager_OnAutoclickerUnlock()
    {
        spriteRenderer.sprite = null;
        this.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(CoinManager.Instance.CurrentCoinCount >= upgradeManager.AutoclickerUnlockCost)
        spriteRenderer.sprite = unlockSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.sprite = lockSprite;
    }
}
