using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public event Action OnClickUpgrade;
    public event Action OnChanceCoinDropUpgrade;
    public event Action OnCoutCoinForDropUpgrade;
    public event Action OnJackpotUpgrade;

    private int clickUpgradeCost = 10;
    private int chanceDropUpgradeCost = 10;
    private int countCoinForDropUpgradeCost = 10;
    private int jackpotUpgradeCost = 10;

    public int ClickUpgradeCost { get { return clickUpgradeCost; } }
    public int ChanceCoinDropUpgradeCost { get { return chanceDropUpgradeCost; } }
    public int CountCoinForDroupgradeCost { get { return countCoinForDropUpgradeCost; } }
    public int JackpotUpgradeCost {  get { return jackpotUpgradeCost; } }

    public static UpgradeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BuyClickUpgrade()
    {
        if(CoinManager.Instance.TrySpendCoin(clickUpgradeCost))
        {
            clickUpgradeCost *= 2;
            OnClickUpgrade?.Invoke();
        }
        else
        {
            Debug.Log("NO COIN!");
        }
    }

    public void BuyChanceCoinDropUpgrade()
    {
        if (CoinManager.Instance.TrySpendCoin(chanceDropUpgradeCost))
        {
            chanceDropUpgradeCost *= 2;
            OnChanceCoinDropUpgrade?.Invoke();
        }
        else
        {
            Debug.Log("NO COIN!");
        }
    }

    public void BuyCountCoinForDropUpgrade()
    {
        if (CoinManager.Instance.TrySpendCoin(countCoinForDropUpgradeCost))
        {
            countCoinForDropUpgradeCost *= 2;
            OnCoutCoinForDropUpgrade?.Invoke();
        }
        else
        {
            Debug.Log("NO COIN!");
        }
    }

    public void BuyJackpotUpgrade()
    {
        if (CoinManager.Instance.TrySpendCoin(jackpotUpgradeCost))
        {
            jackpotUpgradeCost *= 2;
            OnJackpotUpgrade?.Invoke();
        }
        else
        {
            Debug.Log("NO COIN!");
        }
    }
}
