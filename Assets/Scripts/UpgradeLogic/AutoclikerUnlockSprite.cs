using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoclikerUnlockSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite unlockSprite;
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private GameplayManager gameplayManager;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        upgradeManager.OnAutoclickerUnlock += UpgradeManager_OnAutoclickerUnlock;
        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
    }

    private void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
       spriteRenderer.sprite = lockSprite;
    }

    private void OnDestroy()
    {
        upgradeManager.OnAutoclickerUnlock -= UpgradeManager_OnAutoclickerUnlock;
        gameplayManager.OnGameLoad -= GameplayManager_OnGameLoad;
    }

    private void GameplayManager_OnGameLoad(SaveData save)
    {
        if (save.autoClickerUnlock)
        {
            Destroy(gameObject);
        }
    }


    private void UpgradeManager_OnAutoclickerUnlock()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        spriteRenderer.sprite = null;
        this.enabled = false;    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(coinManager.CurrentCoinCount >= upgradeManager.AutoclickerUnlockCost)
        spriteRenderer.sprite = unlockSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.sprite = lockSprite;
    }
}
