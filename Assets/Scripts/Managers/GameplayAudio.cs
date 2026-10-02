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
    [SerializeField] private AudioClip _stageComplitedSFX;

    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private ProgressBarManager progressBarManager;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private LevelManager levelManager;

    private void Awake()
    {
        clickHandler.OnClick += ClickSFXPlay;
        upgradeManager.OnByuAnyUpgrade += UpgradeSFXPlay;
        upgradeManager.OnNoCoinForUpgrade += NoCoinSFXPlay;
        upgradeManager.OnAutoclickerUnlock += AutoclickerUnlockSFXPlay;
        progressBarManager.OnBarFilled += OrgasmSFXPlay;
        coinManager.OnCoinDrop += CoinDropSFXPlay;
        levelManager.OnStageComplete += StageCompliteSFXPlay;
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
        levelManager.OnStageComplete -= StageCompliteSFXPlay;
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

    private void StageCompliteSFXPlay(StageData save)
    {
        AudioManager.Instance.PlaySFX(_stageComplitedSFX);
    }
}
