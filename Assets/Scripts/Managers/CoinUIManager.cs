using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currentCoinCountText;
    [SerializeField] private TextMeshProUGUI chanceCoinDropText;
    [SerializeField] private TextMeshProUGUI countCoinForDropText;
    [SerializeField] private TextMeshProUGUI jackpotAmountText;
    [SerializeField] private TextMeshProUGUI chanceCoinDropUpgradeCostText;

    [SerializeField] private Button coinChanceUpgradeBtn;

    private void Start()
    {
        CoinManager.Instance.OnCoinDrop += CoinManager_OnCoinDrop;
        CoinManager.Instance.OnCoinChange += CoinManager_OnCoinChange;
        CoinManager.Instance.OnMaxDropChance += CoinManager_OnMaxDropChance;
        CoinManager.Instance.OnCoinDropChanceChange += CoinManager_OnCoinDropChanceChange;
        CoinManager.Instance.OnCountCoinForDropChange += CoinManager_OnCountCoinForDropChange;
        CoinManager.Instance.OnJackpotAmountChange += CoinManager_OnJackpotAmountChange;

        jackpotAmountText.text = "+" + CoinManager.Instance.JackpotAmount.ToString();
        chanceCoinDropText.text = CoinManager.Instance.CoinDropChance.ToString() + "%";
        countCoinForDropText.text = "+" + CoinManager.Instance.CountCoinForDrop.ToString();
    }

    private void OnDestroy()
    {
        CoinManager.Instance.OnCoinDrop -= CoinManager_OnCoinDrop;
        CoinManager.Instance.OnCoinChange -= CoinManager_OnCoinChange;
        CoinManager.Instance.OnMaxDropChance -= CoinManager_OnMaxDropChance;
        CoinManager.Instance.OnCoinDropChanceChange -= CoinManager_OnCoinDropChanceChange;
        CoinManager.Instance.OnCountCoinForDropChange -= CoinManager_OnCountCoinForDropChange;
        CoinManager.Instance.OnJackpotAmountChange -= CoinManager_OnJackpotAmountChange;
    }

    private void CoinManager_OnJackpotAmountChange()
    {
        jackpotAmountText.text = "+" + CoinManager.Instance.JackpotAmount.ToString();
    }

    private void CoinManager_OnCountCoinForDropChange()
    {
        countCoinForDropText.text = "+" + CoinManager.Instance.CountCoinForDrop.ToString();
    }

    private void CoinManager_OnCoinDropChanceChange(float obj)
    {
        chanceCoinDropText.text = CoinManager.Instance.CoinDropChance.ToString() + "%";
    }

    private void CoinManager_OnMaxDropChance()
    {
        coinChanceUpgradeBtn.enabled = false;
        chanceCoinDropUpgradeCostText.text = "MAX";
    }

    private void CoinManager_OnCoinChange(int newCoinCount)
    {
        currentCoinCountText.text = newCoinCount.ToString();
    }

    private void CoinManager_OnCoinDrop(int currentCoinCount)
    {
        currentCoinCountText.text = currentCoinCount.ToString();
    }
}
