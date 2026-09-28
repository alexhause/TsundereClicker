using System;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] private CoinManager coinManager;

    private void Awake()
    {
        coinManager.OnHeartCountChange += Instance_OnHeartCountChange;
    }

    private void Start()
    {
        coinManager.OnCoinDrop += CoinManager_OnCoinDrop;
        coinManager.OnHeartCountChange += CoinManager_OnCoinChange;
        coinManager.OnMaxDropChance += CoinManager_OnMaxDropChance;
        coinManager.OnCoinDropChanceChange += CoinManager_OnCoinDropChanceChange;
        coinManager.OnCountCoinForDropChange += CoinManager_OnCountCoinForDropChange;
        coinManager.OnJackpotAmountChange += CoinManager_OnJackpotAmountChange;
        

        jackpotAmountText.text = "+" + coinManager.JackpotAmount.ToString();
        chanceCoinDropText.text = coinManager.CoinDropChance.ToString() + "%";
        countCoinForDropText.text = "+" + coinManager.CountCoinForDrop.ToString();
    }

    private void OnDestroy()
    {
        coinManager.OnCoinDrop -= CoinManager_OnCoinDrop;
        coinManager.OnHeartCountChange -= CoinManager_OnCoinChange;
        coinManager.OnMaxDropChance -= CoinManager_OnMaxDropChance;
        coinManager.OnCoinDropChanceChange -= CoinManager_OnCoinDropChanceChange;
        coinManager.OnCountCoinForDropChange -= CoinManager_OnCountCoinForDropChange;
        coinManager.OnJackpotAmountChange -= CoinManager_OnJackpotAmountChange;
        coinManager.OnHeartCountChange -= Instance_OnHeartCountChange;
    }

    private void CoinManager_OnJackpotAmountChange()
    {
        jackpotAmountText.text = "+" + coinManager.JackpotAmount.ToString();
    }

    private void CoinManager_OnCountCoinForDropChange()
    {
        countCoinForDropText.text = "+" + coinManager.CountCoinForDrop.ToString();
    }

    private void CoinManager_OnCoinDropChanceChange(float obj)
    {
        chanceCoinDropText.text = coinManager.CoinDropChance.ToString() + "%";
    }

    private void Instance_OnHeartCountChange(int newCount)
    {
        currentCoinCountText.text = newCount.ToString();
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
