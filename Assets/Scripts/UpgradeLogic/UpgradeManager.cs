using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public event Action OnClickUpgrade;
    public event Action OnChanceCoinDropUpgrade;
    public event Action OnCoutCoinForDropUpgrade;
    public event Action OnJackpotUpgrade;
    public event Action OnJackpotFillSpeedUpgrade;
    public event Action OnAutoclickerUnlock;
    public event Action OnAutoclickerUpgrade;
    public event Action OnNoCoinForUpgrade;
    public event Action OnNoCoinForUnlock;
    public event Action OnByuAnyUpgrade;

   [SerializeField] private int clickUpgradeCost = 10;
   [SerializeField] private int chanceDropUpgradeCost = 10;
   [SerializeField] private int countCoinForDropUpgradeCost = 10;
   [SerializeField] private int jackpotUpgradeCost = 10;
   [SerializeField] private int _jackpotFillSpeedUpgradeCost = 10;
   [SerializeField] private int autoclickerUnlockCost = 10;
   [SerializeField] private int autoclickerUpgradeCost = 10;

    [SerializeField] private float clickUpgradeCostFactor = 1.5f;
    [SerializeField] private float chanceDropUpgradeCostFactor = 1.5f;
    [SerializeField] private float countCoinForDropUpgradeCostFactor = 1.5f;
    [SerializeField] private float jackpotUpgradeCostFactor = 1.5f;
    [SerializeField] private float _jackpotFillSpeedUpgradeCostFactor = 1.5f;
    [SerializeField] private float autoclickerUpgradeCostFactor = 1.5f;

    [SerializeField] private CoinManager coinManager;
    [SerializeField] private GameplayManager gameplayManager;

    public int ClickUpgradeCost { get { return clickUpgradeCost; } }
    public int ChanceCoinDropUpgradeCost { get { return chanceDropUpgradeCost; } }
    public int CountCoinForDroupgradeCost { get { return countCoinForDropUpgradeCost; } }
    public int JackpotUpgradeCost {  get { return jackpotUpgradeCost; } }
    public int JackpotFillSpeedUpgradeCost { get { return _jackpotFillSpeedUpgradeCost; } }
    public int AutoclickerUnlockCost { get { return autoclickerUnlockCost; }  }
    public int AutoclickerUpgradeCost { get { return autoclickerUpgradeCost; } }


    private void Awake()
    {
        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
    }

    public void BuyClickUpgrade()
    {
        if(coinManager.TrySpendCoin((int)clickUpgradeCost))
        {
            clickUpgradeCost = CalculateNewPrice(clickUpgradeCost, clickUpgradeCostFactor);
            OnClickUpgrade?.Invoke();
            OnByuAnyUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyChanceCoinDropUpgrade()
    {
        if (coinManager.TrySpendCoin(chanceDropUpgradeCost))
        {
            chanceDropUpgradeCost = CalculateNewPrice(chanceDropUpgradeCost, chanceDropUpgradeCostFactor);
            OnChanceCoinDropUpgrade?.Invoke();
            OnByuAnyUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyCountCoinForDropUpgrade()
    {
        if (coinManager.TrySpendCoin(countCoinForDropUpgradeCost))
        {
            countCoinForDropUpgradeCost = CalculateNewPrice(countCoinForDropUpgradeCost, countCoinForDropUpgradeCostFactor);
            OnCoutCoinForDropUpgrade?.Invoke();
            OnByuAnyUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyJackpotUpgrade()
    {
        if (coinManager.TrySpendCoin(jackpotUpgradeCost))
        {
            jackpotUpgradeCost = CalculateNewPrice(jackpotUpgradeCost, jackpotUpgradeCostFactor);
            OnJackpotUpgrade?.Invoke();
            OnByuAnyUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyJackpotFillSpeedUpgrade()
    {
        if (coinManager.TrySpendCoin(_jackpotFillSpeedUpgradeCost))
        {
            _jackpotFillSpeedUpgradeCost = CalculateNewPrice(_jackpotFillSpeedUpgradeCost, _jackpotFillSpeedUpgradeCostFactor);
            OnJackpotFillSpeedUpgrade?.Invoke();
            OnByuAnyUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyAutoclikerUnlock()
    {
        if (coinManager.TrySpendCoin(autoclickerUnlockCost))
        {
            OnAutoclickerUnlock?.Invoke();
            OnByuAnyUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
            OnNoCoinForUnlock?.Invoke();
        }
    }

    public void BuyAutoclikerUpgrade()
    {
        if (coinManager.TrySpendCoin(autoclickerUpgradeCost))
        {
            autoclickerUpgradeCost = CalculateNewPrice(autoclickerUpgradeCost, autoclickerUpgradeCostFactor);
            OnAutoclickerUpgrade?.Invoke();
            OnByuAnyUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    private void GameplayManager_OnGameLoad(SaveData save)
    {
        clickUpgradeCost = save.clickUpgradeCost;
        OnClickUpgrade?.Invoke();
    }

    private int CalculateNewPrice(int currentPrice, float factor)
    {
        return (int)(currentPrice * factor);
    }
}
