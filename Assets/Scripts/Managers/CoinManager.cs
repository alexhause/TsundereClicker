using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public event Action<int> OnCoinDrop;
    public event Action<int> OnCoinChange;
    public event Action<float> OnCoinDropChanceChange;
    public event Action OnCountCoinForDropChange;
    public event Action OnJackpotAmountChange;
    public event Action OnMaxDropChance;
    public event Action OnJackpotPayout;

    private int currentCoinCount = 0;

    [SerializeField] private int jackpotAmount = 1; //размер выплаты джекпота
    [SerializeField] private int countCoinForDrop = 0; //сколько монет дают за один дроп
    [SerializeField, Range(2, 100)] private int coinDropChance = 5; //вероятсность дропа

    [SerializeField] ClickHendler clickHendler;
    [SerializeField] ProgressBarManager progressBarManager;

    public int CurrentCoinCount { get { return currentCoinCount; }  }
    public int CoinDropChance { get { return coinDropChance; } }
    public int CountCoinForDrop { get { return countCoinForDrop; } }
    public int JackpotAmount { get { return jackpotAmount; } }

    public static CoinManager Instance { get; private set; }

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

    private void Start()
    {
        clickHendler.OnClick += ClickHendler_OnClick;

        UpgradeManager.Instance.OnChanceCoinDropUpgrade += UpgradeManager_OnChanceCoinDropUpgrade;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade += UpgradeManager_OnCoutCoinForDropUpgrade;
        UpgradeManager.Instance.OnJackpotUpgrade += UpgradeManager_OnJackpotUpgrade;

        progressBarManager.OnBarFilled += ProgressBarManager_OnBarFilled;
    }

    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        UpgradeManager.Instance.OnChanceCoinDropUpgrade -= UpgradeManager_OnChanceCoinDropUpgrade;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade -= UpgradeManager_OnCoutCoinForDropUpgrade;
        UpgradeManager.Instance.OnJackpotUpgrade -= UpgradeManager_OnJackpotUpgrade;
        progressBarManager.OnBarFilled -= ProgressBarManager_OnBarFilled;
    }

    private void UpgradeManager_OnJackpotUpgrade()
    {
        jackpotAmount += 1;
        OnJackpotAmountChange?.Invoke();
    }

    private void UpgradeManager_OnChanceCoinDropUpgrade()
    {
        if(coinDropChance < 95)
        {
            coinDropChance += 5;
            OnCoinChange?.Invoke(currentCoinCount);
            OnCoinDropChanceChange?.Invoke(coinDropChance);
        }
        else
        {
            coinDropChance = 100;
            OnCoinDropChanceChange?.Invoke(coinDropChance);
            OnCoinChange?.Invoke(currentCoinCount);
            OnMaxDropChance?.Invoke();
        }
    }

    private void UpgradeManager_OnCoutCoinForDropUpgrade()
    {
        countCoinForDrop += 1;
        OnCountCoinForDropChange?.Invoke();
    }

    private void ClickHendler_OnClick()
    {
        if(UnityEngine.Random.value * 100 <= coinDropChance)
        {
            currentCoinCount += countCoinForDrop;
            OnCoinDrop?.Invoke(currentCoinCount);
        }
    }

    private void ProgressBarManager_OnBarFilled()
    {
        currentCoinCount += jackpotAmount;
        OnJackpotPayout?.Invoke();
        OnCoinChange?.Invoke(currentCoinCount);
    }

    public bool TrySpendCoin(int amount)
    {
        if(currentCoinCount >= amount)
        {
            currentCoinCount -= amount;
            OnCoinChange?.Invoke(currentCoinCount);
            return true;
        }
        else
        {
            return false;
        }
    }
}
