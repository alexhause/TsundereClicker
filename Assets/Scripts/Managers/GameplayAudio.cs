using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioClip upgradeSFX;
    [SerializeField] private AudioClip coinDropSFX;
    [SerializeField] private AudioClip orgasmSFX;
    [SerializeField] private AudioClip music;
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private ProgressBarManager progressBarManager;


    private void Start()
    {
        AudioManager.Instance.PlayMusic(music);
        clickHandler.OnClick += ClickSFXPlay;
        upgradeManager.OnAutoclickerUpgrade += UpgradeSFXPlay;
        upgradeManager.OnChanceCoinDropUpgrade += UpgradeSFXPlay;
        upgradeManager.OnClickUpgrade += UpgradeSFXPlay;
        upgradeManager.OnCoutCoinForDropUpgrade += UpgradeSFXPlay;
        upgradeManager.OnJackpotUpgrade += UpgradeSFXPlay;
        progressBarManager.OnBarFilled += OrgasmSFXPlay;
        CoinManager.Instance.OnCoinDrop += CoinDropSFXPlay;
        
    }

    private void OnDestroy()
    {
        clickHandler.OnClick -= ClickSFXPlay;
        upgradeManager.OnAutoclickerUpgrade -= UpgradeSFXPlay;
        upgradeManager.OnChanceCoinDropUpgrade -= UpgradeSFXPlay;
        upgradeManager.OnClickUpgrade -= UpgradeSFXPlay;
        upgradeManager.OnCoutCoinForDropUpgrade -= UpgradeSFXPlay;
        upgradeManager.OnJackpotUpgrade -= UpgradeSFXPlay;
        progressBarManager.OnBarFilled -= OrgasmSFXPlay;
        CoinManager.Instance.OnCoinDrop -= CoinDropSFXPlay;
    }

    private void UpgradeSFXPlay()
    {
        AudioManager.Instance.PlaySFX(upgradeSFX);
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
}
