using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clickUpgradeCostText;
    [SerializeField] private TextMeshProUGUI chanceCoinDropUpgradeCost;
    [SerializeField] private TextMeshProUGUI countCoinForDropUgradeCostText;
    [SerializeField] private TextMeshProUGUI jackpotUpgradeCostText;
    [SerializeField] private TextMeshProUGUI jackpotFillSpeedUpgradeCostText;
    [SerializeField] private TextMeshProUGUI autoclickerUpgradeCostText;
    [SerializeField] private Button autoClickerUpgradeBtn;
    [SerializeField] private Button jackpotFillSpeedUpgradeBtn;
    [SerializeField] private TextMeshProUGUI chanceCoinDropUpgradeCostText;
    [SerializeField] private Button coinChanceUpgradeBtn;
    [SerializeField] private Button jackpotUpgradeBtn;
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private ProgressBarManager progressBarManager;
    [SerializeField] private CoinManager coinManager;

    private void Awake()
    {
        clickHandler.OnAutoclikerMax += ClickHendler_OnAutoclikerMax;
        upgradeManager.OnClickUpgrade += UpgradeManager_OnClickUpgrade;
        upgradeManager.OnChanceCoinDropUpgrade += UpgradeManager_OnChanceCoinDropUpgrade;
        upgradeManager.OnCoutCoinForDropUpgrade += UpgradeManager_OnCoutCoinForDropUpgrade;
        upgradeManager.OnJackpotUpgrade += UpgradeManager_OnJackpotUpgrade;
        upgradeManager.OnJackpotFillSpeedUpgrade += Instance_OnJackpotFillSpeedUpgrade;
        upgradeManager.OnAutoclickerUnlock += Instance_OnAutoclickerUnlock;
        upgradeManager.OnAutoclickerUpgrade += Instance_OnAutoclickerUpgrade;
        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
        coinManager.OnMaxDropChance += CoinManager_OnMaxDropChance;
        coinManager.OnJackpotRewardMax += CoinManager_OnJackpotRewardMax;


        autoclickerUpgradeCostText.text = upgradeManager.AutoclickerUnlockCost.ToString();
        countCoinForDropUgradeCostText.text = upgradeManager.CountCoinForDroupgradeCost.ToString();
        jackpotUpgradeCostText.text = upgradeManager.JackpotUpgradeCost.ToString();
        jackpotFillSpeedUpgradeCostText.text = upgradeManager.JackpotFillSpeedUpgradeCost.ToString();
        clickUpgradeCostText.text = upgradeManager.ClickUpgradeCost.ToString();
        chanceCoinDropUpgradeCost.text = upgradeManager.ChanceCoinDropUpgradeCost.ToString();
    }

    private void OnDestroy()
    {
        upgradeManager.OnClickUpgrade -= UpgradeManager_OnClickUpgrade;
        upgradeManager.OnChanceCoinDropUpgrade -= UpgradeManager_OnChanceCoinDropUpgrade;
        upgradeManager.OnCoutCoinForDropUpgrade -= UpgradeManager_OnCoutCoinForDropUpgrade;
        upgradeManager.OnJackpotUpgrade -= UpgradeManager_OnJackpotUpgrade;
        upgradeManager.OnJackpotFillSpeedUpgrade -= Instance_OnJackpotFillSpeedUpgrade;
        upgradeManager.OnAutoclickerUnlock -= Instance_OnAutoclickerUnlock;
        upgradeManager.OnAutoclickerUpgrade -= Instance_OnAutoclickerUpgrade;
        gameplayManager.OnGameLoad -= GameplayManager_OnGameLoad;
        coinManager.OnMaxDropChance -= CoinManager_OnMaxDropChance;
        coinManager.OnJackpotRewardMax -= CoinManager_OnJackpotRewardMax;

        clickHandler.OnAutoclikerMax -= ClickHendler_OnAutoclikerMax;
    }

    private void GameplayManager_OnGameLoad(SaveData save)
    {
        if (save.autoClickerUnlock)
        {
            if(save.autoclikerPower == clickHandler.AutoClickerMinInterval)
            {
                autoClickerUpgradeBtn.enabled = false;
                autoclickerUpgradeCostText.text = "MAX";
            }
            else
            {
                autoClickerUpgradeBtn.image.raycastTarget = true;
                autoClickerUpgradeBtn.interactable = true;
                autoclickerUpgradeCostText.text = save.autoclikerUpgradeCost.ToString();
            }
        }
        if(save.jackpotProgressBarFillSpeed == 10)
        {
            jackpotFillSpeedUpgradeBtn.enabled = false;
            jackpotFillSpeedUpgradeCostText.text = "MAX";
        }
        else
        {
            jackpotFillSpeedUpgradeCostText.text = save.jackpotFillSpeedUpgradeCost.ToString();
        }

        if(save.heartDropChance == 100)
        {
            coinChanceUpgradeBtn.enabled = false;
            chanceCoinDropUpgradeCostText.text = "MAX";
        }
        else
        {
            chanceCoinDropUpgradeCostText.text = save.chanceHeartDropUpgradeCost.ToString();
        }

        if(save.jackpotReward >= coinManager.JackpotMaxAmount)
        {
            jackpotUpgradeBtn.enabled = false;
            jackpotUpgradeCostText.text = "MAX";
        }
        else
        {
            jackpotUpgradeCostText.text = save.jackpotUpgradeCost.ToString();
        }

        clickUpgradeCostText.text = save.clickUpgradeCost.ToString();
        countCoinForDropUgradeCostText.text = save.countHeartForDropUpgradeCost.ToString();
    }

    private void CoinManager_OnMaxDropChance()
    {
        coinChanceUpgradeBtn.enabled = false;
        chanceCoinDropUpgradeCostText.text = "MAX";
    }

    private void CoinManager_OnJackpotRewardMax()
    {
        jackpotUpgradeBtn.enabled = false;
        jackpotUpgradeCostText.text = "MAX";
    }

    private void Instance_OnAutoclickerUpgrade()
    {
        if(!clickHandler.IsAutoclickMax)
        autoclickerUpgradeCostText.text = upgradeManager.AutoclickerUpgradeCost.ToString();
    }

    private void ClickHendler_OnAutoclikerMax()
    {
        autoClickerUpgradeBtn.enabled = false;
        autoclickerUpgradeCostText.text = "MAX";
    }

    private void Instance_OnAutoclickerUnlock()
    {
        autoClickerUpgradeBtn.image.raycastTarget = true;
        autoClickerUpgradeBtn.interactable = true;
        autoclickerUpgradeCostText.text = upgradeManager.AutoclickerUpgradeCost.ToString();
    }

    private void UpgradeManager_OnJackpotUpgrade()
    {
        jackpotUpgradeCostText.text = upgradeManager.JackpotUpgradeCost.ToString();
    }

    private void Instance_OnJackpotFillSpeedUpgrade()
    {
        if (!progressBarManager.FillSpeedUpgradeMax)
        {
            jackpotFillSpeedUpgradeCostText.text = upgradeManager.JackpotFillSpeedUpgradeCost.ToString();
        }
        else
        {
            jackpotFillSpeedUpgradeBtn.enabled = false;
            jackpotFillSpeedUpgradeCostText.text = "MAX";
        }
       
    }

    private void UpgradeManager_OnCoutCoinForDropUpgrade()
    {
        countCoinForDropUgradeCostText.text = upgradeManager.CountCoinForDroupgradeCost.ToString();
    }

    private void UpgradeManager_OnChanceCoinDropUpgrade()
    {
        chanceCoinDropUpgradeCost.text = upgradeManager.ChanceCoinDropUpgradeCost.ToString();
    }

    private void UpgradeManager_OnClickUpgrade()
    {
        clickUpgradeCostText.text = upgradeManager.ClickUpgradeCost.ToString();
    }
}
