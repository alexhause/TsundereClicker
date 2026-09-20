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

    public int ClickUpgradeCost { get { return clickUpgradeCost; } }
    public int ChanceCoinDropUpgradeCost { get { return chanceDropUpgradeCost; } }
    public int CountCoinForDroupgradeCost { get { return countCoinForDropUpgradeCost; } }
    public int JackpotUpgradeCost {  get { return jackpotUpgradeCost; } }
    public int JackpotFillSpeedUpgradeCost { get { return _jackpotFillSpeedUpgradeCost; } }
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
        if(CoinManager.Instance.TrySpendCoin((int)clickUpgradeCost))
        {
            clickUpgradeCost = CalculateNewPrice(clickUpgradeCost, clickUpgradeCostFactor);
            OnClickUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyChanceCoinDropUpgrade()
    {
        if (CoinManager.Instance.TrySpendCoin(chanceDropUpgradeCost))
        {
            chanceDropUpgradeCost = CalculateNewPrice(chanceDropUpgradeCost, chanceDropUpgradeCostFactor);
            OnChanceCoinDropUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyCountCoinForDropUpgrade()
    {
        if (CoinManager.Instance.TrySpendCoin(countCoinForDropUpgradeCost))
        {
            countCoinForDropUpgradeCost = CalculateNewPrice(countCoinForDropUpgradeCost, countCoinForDropUpgradeCostFactor);
            OnCoutCoinForDropUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyJackpotUpgrade()
    {
        if (CoinManager.Instance.TrySpendCoin(jackpotUpgradeCost))
        {
            jackpotUpgradeCost = CalculateNewPrice(jackpotUpgradeCost, jackpotUpgradeCostFactor);
            OnJackpotUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyJackpotFillSpeedUpgrade()
    {
        if (CoinManager.Instance.TrySpendCoin(_jackpotFillSpeedUpgradeCost))
        {
            _jackpotFillSpeedUpgradeCost = CalculateNewPrice(_jackpotFillSpeedUpgradeCost, _jackpotFillSpeedUpgradeCostFactor);
            OnJackpotFillSpeedUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
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
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    public void BuyAutoclikerUpgrade()
    {
        if (CoinManager.Instance.TrySpendCoin(autoclickerUpgradeCost))
        {
            autoclickerUpgradeCost = CalculateNewPrice(autoclickerUpgradeCost, autoclickerUpgradeCostFactor);
            OnAutoclickerUpgrade?.Invoke();
        }
        else
        {
            OnNoCoinForUpgrade?.Invoke();
        }
    }

    private int CalculateNewPrice(int currentPrice, float factor)
    {
        return (int)(currentPrice * factor);
    }
}
