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

    [SerializeField] private CoinManager coinManager;
    [SerializeField] private GameplayManager gameplayManager;

    private void Awake()
    {
        coinManager.OnHeartCountChange += Instance_OnHeartCountChange;
        coinManager.OnCoinDrop += CoinManager_OnCoinDrop;
        coinManager.OnCoinDropChanceChange += CoinManager_OnCoinDropChanceChange;
        coinManager.OnCountCoinForDropChange += CoinManager_OnCountCoinForDropChange;
        coinManager.OnJackpotAmountChange += CoinManager_OnJackpotAmountChange;
    }

    private void Start()
    {        
        jackpotAmountText.text = "+" + coinManager.JackpotAmount.ToString();
        chanceCoinDropText.text = coinManager.CoinDropChance.ToString() + "%";
        countCoinForDropText.text = "+" + coinManager.CountCoinForDrop.ToString();
    }

    private void OnDestroy()
    {
        coinManager.OnCoinDrop -= CoinManager_OnCoinDrop;

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



    private void CoinManager_OnCoinDrop(int currentCoinCount)
    {
        currentCoinCountText.text = currentCoinCount.ToString();
    }
}
