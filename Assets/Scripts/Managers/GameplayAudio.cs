using Unity.VisualScripting;
using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioClip upgradeSFX;
    [SerializeField] private AudioClip coinDropSFX;
    [SerializeField] private AudioClip orgasmSFX;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip _noCoinSFX;
    [SerializeField] private AudioClip _autoclicekrUnlocSFX;

    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private ProgressBarManager progressBarManager;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private UpgradeManager upgradeManager;

    private void Awake()
    {
        clickHandler.OnClick += ClickSFXPlay;
        upgradeManager.OnByuAnyUpgrade += UpgradeSFXPlay;
        upgradeManager.OnNoCoinForUpgrade += NoCoinSFXPlay;
        upgradeManager.OnAutoclickerUnlock += AutoclickerUnlockSFXPlay;
        progressBarManager.OnBarFilled += OrgasmSFXPlay;
        coinManager.OnCoinDrop += CoinDropSFXPlay;
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(music);      
    }

    private void OnDestroy()
    {
        clickHandler.OnClick -= ClickSFXPlay;
        upgradeManager.OnByuAnyUpgrade -= UpgradeSFXPlay;
        upgradeManager.OnNoCoinForUpgrade -= NoCoinSFXPlay;
        upgradeManager.OnAutoclickerUnlock -= AutoclickerUnlockSFXPlay;
        progressBarManager.OnBarFilled -= OrgasmSFXPlay;
        coinManager.OnCoinDrop -= CoinDropSFXPlay;
    }

    private void UpgradeSFXPlay()
    {
        AudioManager.Instance.PlaySFX(upgradeSFX);
    }

    private void AutoclickerUnlockSFXPlay()
    {
        AudioManager.Instance.PlaySFX(_autoclicekrUnlocSFX);
    }

    private void ClickSFXPlay()
    {
        AudioManager.Instance.PlaySFX(clickSFX);
    }

    private void OrgasmSFXPlay()
    {
        AudioManager.Instance.PlaySFX(orgasmSFX);
    }

    private void CoinDropSFXPlay(int obj)
    {
        AudioManager.Instance.PlaySFX(coinDropSFX);
    }

    private void NoCoinSFXPlay()
    {
        AudioManager.Instance.PlaySFX(_noCoinSFX);
    }
}
