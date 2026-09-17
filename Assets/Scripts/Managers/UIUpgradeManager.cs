using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clickUpgradeCostText;
    [SerializeField] private TextMeshProUGUI chanceCoinDropUpgradeCost;
    [SerializeField] private TextMeshProUGUI countCoinForDropUgradeCostText;
    [SerializeField] private TextMeshProUGUI jackpotUpgradeCostText;
    [SerializeField] private TextMeshProUGUI autoclickerUpgradeCostText;
    [SerializeField] private Button autoClickerUpgradeBtn;

    private void Start()
    {
        UpgradeManager.Instance.OnClickUpgrade += UpgradeManager_OnClickUpgrade;
        UpgradeManager.Instance.OnChanceCoinDropUpgrade += UpgradeManager_OnChanceCoinDropUpgrade;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade += UpgradeManager_OnCoutCoinForDropUpgrade;
        UpgradeManager.Instance.OnJackpotUpgrade += UpgradeManager_OnJackpotUpgrade;
        UpgradeManager.Instance.OnAutoclickerUnlock += Instance_OnAutoclickerUnlock;
        UpgradeManager.Instance.OnAutoclickerUpgrade += Instance_OnAutoclickerUpgrade;

        countCoinForDropUgradeCostText.text = UpgradeManager.Instance.CountCoinForDroupgradeCost.ToString();
        jackpotUpgradeCostText.text = UpgradeManager.Instance.JackpotUpgradeCost.ToString();
        clickUpgradeCostText.text = UpgradeManager.Instance.ClickUpgradeCost.ToString();
        chanceCoinDropUpgradeCost.text = UpgradeManager.Instance.ChanceCoinDropUpgradeCost.ToString();
        autoclickerUpgradeCostText.text = UpgradeManager.Instance.AutoclickerUnlockCost.ToString();
    }

    private void OnDestroy()
    {
        UpgradeManager.Instance.OnClickUpgrade += UpgradeManager_OnClickUpgrade;
        UpgradeManager.Instance.OnChanceCoinDropUpgrade += UpgradeManager_OnChanceCoinDropUpgrade;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade += UpgradeManager_OnCoutCoinForDropUpgrade;
        UpgradeManager.Instance.OnJackpotUpgrade += UpgradeManager_OnJackpotUpgrade;
        UpgradeManager.Instance.OnAutoclickerUnlock += Instance_OnAutoclickerUnlock;
        UpgradeManager.Instance.OnAutoclickerUpgrade += Instance_OnAutoclickerUpgrade;
    }

    private void Instance_OnAutoclickerUpgrade()
    {
        autoclickerUpgradeCostText.text = UpgradeManager.Instance.AutoclickerUpgradeCost.ToString();
    }

    private void Instance_OnAutoclickerUnlock()
    {
        autoClickerUpgradeBtn.interactable = true;
        autoclickerUpgradeCostText.text = UpgradeManager.Instance.AutoclickerUpgradeCost.ToString();
    }

    private void UpgradeManager_OnJackpotUpgrade()
    {
        jackpotUpgradeCostText.text = UpgradeManager.Instance.JackpotUpgradeCost.ToString();
    }

    private void UpgradeManager_OnCoutCoinForDropUpgrade()
    {
        countCoinForDropUgradeCostText.text = UpgradeManager.Instance.CountCoinForDroupgradeCost.ToString();
    }

    private void UpgradeManager_OnChanceCoinDropUpgrade()
    {
        chanceCoinDropUpgradeCost.text = UpgradeManager.Instance.ChanceCoinDropUpgradeCost.ToString();
    }

    private void UpgradeManager_OnClickUpgrade()
    {
        clickUpgradeCostText.text = UpgradeManager.Instance.ClickUpgradeCost.ToString();
    }
}
