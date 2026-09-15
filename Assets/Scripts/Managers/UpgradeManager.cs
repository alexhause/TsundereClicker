using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public event Action OnClickUpgrade;
    public event Action OnChanceCoinDropUpgrade;
    public event Action OnCoutCoinForDropUpgrade;
    public event Action OnJackpotUpgrade;
    public event Action OnAutoclickerUnlock;
    public event Action OnAutoclickerUpgrade;

    private int clickUpgradeCost = 10;
    private int chanceDropUpgradeCost = 10;
    private int countCoinForDropUpgradeCost = 10;
    private int jackpotUpgradeCost = 10;
    private int autoclickerUnlockCost = 1000;
    private int autoclickerUpgradeCost = 10000;

    public int ClickUpgradeCost { get { return clickUpgradeCost; } }
    public int ChanceCoinDropUpgradeCost { get { return chanceDropUpgradeCost; } }
    public int CountCoinForDroupgradeCost { get { return countCoinForDropUpgradeCost; } }
    public int JackpotUpgradeCost {  get { return jackpotUpgradeCost; } }
    public int AutoclickerUnlockCost { get { return autoclickerUnlockCost; }  }
    public int AutoclickerUpgradeCost { get { return autoclickerUpgradeCost; } }

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

    public void BuyAutoclikerUnlock()
    {
        if (CoinManager.Instance.TrySpendCoin(autoclickerUnlockCost))
        {
            OnAutoclickerUnlock?.Invoke();
        }
        else
        {
            Debug.Log("NO COIN!");
        }
    }

    public void BuyAutoclikerUpgrade()
    {
        if (CoinManager.Instance.TrySpendCoin(autoclickerUpgradeCost))
        {
            autoclickerUpgradeCost *= 2;
            OnAutoclickerUpgrade?.Invoke();
        }
        else
        {
            Debug.Log("NO COIN!");
        }
    }
}
