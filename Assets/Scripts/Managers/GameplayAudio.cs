using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioClip upgradeSFX;
    [SerializeField] private AudioClip coinDropSFX;
    [SerializeField] private AudioClip orgasmSFX;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip _noCoinSFX;
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private ProgressBarManager progressBarManager;


    private void Start()
    {
        AudioManager.Instance.PlayMusic(music);
        clickHandler.OnClick += ClickSFXPlay;
        UpgradeManager.Instance.OnAutoclickerUpgrade += UpgradeSFXPlay;
        UpgradeManager.Instance.OnChanceCoinDropUpgrade += UpgradeSFXPlay;
        UpgradeManager.Instance.OnClickUpgrade += UpgradeSFXPlay;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade += UpgradeSFXPlay;
        UpgradeManager.Instance.OnJackpotUpgrade += UpgradeSFXPlay;
        UpgradeManager.Instance.OnJackpotFillSpeedUpgrade += UpgradeSFXPlay;
        UpgradeManager.Instance.OnNoCoinForUpgrade += NoCoinSFXPlay;
        progressBarManager.OnBarFilled += OrgasmSFXPlay;
        CoinManager.Instance.OnCoinDrop += CoinDropSFXPlay;         
    }

    private void OnDestroy()
    {
        clickHandler.OnClick -= ClickSFXPlay;
        UpgradeManager.Instance.OnAutoclickerUpgrade -= UpgradeSFXPlay;
        UpgradeManager.Instance.OnChanceCoinDropUpgrade -= UpgradeSFXPlay;
        UpgradeManager.Instance.OnClickUpgrade -= UpgradeSFXPlay;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade -= UpgradeSFXPlay;
        UpgradeManager.Instance.OnJackpotUpgrade -= UpgradeSFXPlay;
        UpgradeManager.Instance.OnJackpotFillSpeedUpgrade -= UpgradeSFXPlay;
        UpgradeManager.Instance.OnNoCoinForUpgrade -= NoCoinSFXPlay;
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

    private void NoCoinSFXPlay()
    {
        AudioManager.Instance.PlaySFX(_noCoinSFX);
    }
}
