using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public event Action<int> OnCoinDrop;
    public event Action<int> OnHeartCountChange;
    public event Action<float> OnCoinDropChanceChange;
    public event Action OnCountCoinForDropChange;
    public event Action OnJackpotAmountChange;
    public event Action OnMaxDropChance;
    public event Action OnJackpotPayout;

    private int currentHeartCount = 0;

    [SerializeField] private int jackpotAmount = 1; //размер выплаты джекпота
    [SerializeField] private int countCoinForDrop = 0; //сколько монет дают за один дроп
    [SerializeField, Range(2, 100)] private int coinDropChance = 5; //вероятсность дропа
    [SerializeField] private float jackpotAmountUppgradeFactor = 2.5f;

    [SerializeField] ClickHandler clickHendler;
    [SerializeField] GameplayManager gameplayManager;
    [SerializeField] ProgressBarManager progressBarManager;

    public int CurrentCoinCount { get { return currentHeartCount; }  }
    public int CoinDropChance { get { return coinDropChance; } }
    public int CountCoinForDrop { get { return countCoinForDrop; } }
    public int JackpotAmount { get { return jackpotAmount; } }

    private void Awake()
    {
        UpgradeManager.Instance.OnJackpotUpgrade += UpgradeManager_OnJackpotUpgrade;
    }

    private void Start()
    {
        clickHendler.OnClick += ClickHendler_OnClick;
        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
        UpgradeManager.Instance.OnChanceCoinDropUpgrade += UpgradeManager_OnChanceCoinDropUpgrade;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade += UpgradeManager_OnCoutCoinForDropUpgrade;
        
        progressBarManager.OnBarFilled += ProgressBarManager_OnBarFilled;
    }

    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        UpgradeManager.Instance.OnChanceCoinDropUpgrade -= UpgradeManager_OnChanceCoinDropUpgrade;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade -= UpgradeManager_OnCoutCoinForDropUpgrade;
        UpgradeManager.Instance.OnJackpotUpgrade -= UpgradeManager_OnJackpotUpgrade;
        progressBarManager.OnBarFilled -= ProgressBarManager_OnBarFilled;
        gameplayManager.OnGameLoad -= GameplayManager_OnGameLoad;
    }

    private void UpgradeManager_OnJackpotUpgrade()
    {
        float newJackpotAmount = jackpotAmount * jackpotAmountUppgradeFactor;
        jackpotAmount = (int)newJackpotAmount;
        OnJackpotAmountChange?.Invoke();
    }

    private void UpgradeManager_OnChanceCoinDropUpgrade()
    {
        if(coinDropChance < 95)
        {
            coinDropChance += 5;
            OnHeartCountChange?.Invoke(currentHeartCount);
            OnCoinDropChanceChange?.Invoke(coinDropChance);
        }
        else
        {
            coinDropChance = 100;
            OnCoinDropChanceChange?.Invoke(coinDropChance);
            OnHeartCountChange?.Invoke(currentHeartCount);
            OnMaxDropChance?.Invoke();
        }
    }

    private void UpgradeManager_OnCoutCoinForDropUpgrade()
    {
        countCoinForDrop += 1;
        OnCountCoinForDropChange?.Invoke();
    }

    private void GameplayManager_OnGameLoad(SaveData saveData)
    {
        currentHeartCount = saveData.hearts;
    }

    private void ClickHendler_OnClick()
    {
        if(UnityEngine.Random.value * 100 <= coinDropChance)
        {
            currentHeartCount += countCoinForDrop;
            OnCoinDrop?.Invoke(currentHeartCount);
        }
    }

    private void ProgressBarManager_OnBarFilled()
    {
        currentHeartCount += jackpotAmount;
        OnJackpotPayout?.Invoke();
        OnHeartCountChange?.Invoke(currentHeartCount);
    }

    public bool TrySpendCoin(int amount)
    {
        if(currentHeartCount >= amount)
        {
            currentHeartCount -= amount;
            OnHeartCountChange?.Invoke(currentHeartCount);
            return true;
        }
        else
        {
            return false;
        }
    }
}
