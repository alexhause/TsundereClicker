using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    public event Action OnBarFilled;

    [SerializeField, Range(1, 10)] private int barfillPercentage;

    [SerializeField] private ClickHandler clickHendler;
    [SerializeField] private Image progressBarFillImage;

    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private GameplayManager gameplayManager;

    public bool FillSpeedUpgradeMax { get; private set; }
    public int BarFillPercent { get {  return barfillPercentage; }  }
    

    private void Awake()
    {
        upgradeManager.OnJackpotFillSpeedUpgrade += Instance_OnJackpotFillSpeedUpgrade;
        clickHendler.OnClick += ClickHendler_OnClick;
        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
        FillSpeedUpgradeMax = false;
        barfillPercentage = 1;
    }

    void Start()
    {
       progressBarFillImage.fillAmount = 0;
    }

    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        upgradeManager.OnJackpotUpgrade -= Instance_OnJackpotFillSpeedUpgrade;
        gameplayManager.OnGameLoad -= GameplayManager_OnGameLoad;
    }

    private void GameplayManager_OnGameLoad(SaveData save)
    {
        barfillPercentage = save.jackpotProgressBarFillSpeed;
        if(barfillPercentage == 10)
        {
            FillSpeedUpgradeMax = true;
        }
    }

    private void ClickHendler_OnClick()
    {
        progressBarFillImage.fillAmount += (float)(barfillPercentage)/100;
        if (progressBarFillImage.fillAmount >= 0.99f)
        {
            OnBarFilled?.Invoke();
            progressBarFillImage.fillAmount = 0;
        }
    }
    private void Instance_OnJackpotFillSpeedUpgrade()
    {
        if(barfillPercentage < 9)
        {
            barfillPercentage += 1;
        }
        else
        {
            barfillPercentage = 10;
            FillSpeedUpgradeMax = true;
        }
    }
}
