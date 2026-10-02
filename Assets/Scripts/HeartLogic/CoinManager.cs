using System;
using Unity.VisualScripting;
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
    public event Action OnJackpotRewardMax;

    private int currentHeartCount = 0;

    [SerializeField] private int jackpotAmount = 1; //размер выплаты джекпота
    [SerializeField] private int countCoinForDrop = 0; //сколько монет дают за один дроп
    [SerializeField, Range(2, 100)] private int coinDropChance = 5; //вероятсность дропа
    [SerializeField] private float jackpotAmountUppgradeFactor = 2.5f;
    [SerializeField] private int _jackpotMaxAmount;
    [SerializeField] private int upgradeCountCoinForDrop;

    [SerializeField] private ClickHandler clickHendler;
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] ProgressBarManager progressBarManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private LevelManager levelManager;

    public int CurrentCoinCount { get { return currentHeartCount; }  }
    public int CoinDropChance { get { return coinDropChance; } }
    public int CountCoinForDrop { get { return countCoinForDrop; } }
    public int JackpotAmount { get { return jackpotAmount; } }
    public int JackpotMaxAmount { get { return _jackpotMaxAmount; } }


    private void Awake()
    {
        upgradeManager.OnJackpotUpgrade += UpgradeManager_OnJackpotUpgrade;
        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
        clickHendler.OnClick += ClickHendler_OnClick;
        upgradeManager.OnChanceCoinDropUpgrade += UpgradeManager_OnChanceCoinDropUpgrade;
        upgradeManager.OnCoutCoinForDropUpgrade += UpgradeManager_OnCoutCoinForDropUpgrade;
        levelManager.OnStageComplete += LevelManager_OnStageComplete;

        progressBarManager.OnBarFilled += ProgressBarManager_OnBarFilled;
    }

    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        upgradeManager.OnChanceCoinDropUpgrade -= UpgradeManager_OnChanceCoinDropUpgrade;
        upgradeManager.OnCoutCoinForDropUpgrade -= UpgradeManager_OnCoutCoinForDropUpgrade;
        upgradeManager.OnJackpotUpgrade -= UpgradeManager_OnJackpotUpgrade;
        progressBarManager.OnBarFilled -= ProgressBarManager_OnBarFilled;
        gameplayManager.OnGameLoad -= GameplayManager_OnGameLoad;
        levelManager.OnStageComplete -= LevelManager_OnStageComplete;
    }

    private void UpgradeManager_OnJackpotUpgrade()
    {
        float newJackpotAmount = jackpotAmount * jackpotAmountUppgradeFactor;
        if(newJackpotAmount >= _jackpotMaxAmount)
        {
            jackpotAmount = _jackpotMaxAmount;
            OnJackpotAmountChange?.Invoke();
            OnJackpotRewardMax?.Invoke();
            return;
        }
        else
        {
            jackpotAmount = (int)newJackpotAmount;
            OnJackpotAmountChange?.Invoke();
        }
    }

    private void LevelManager_OnStageComplete(StageData stage)
    {
        currentHeartCount += stage.stageCompletionReward;
        OnHeartCountChange?.Invoke(currentHeartCount);
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
        countCoinForDrop += upgradeCountCoinForDrop;
        OnCountCoinForDropChange?.Invoke();
    }

    private void GameplayManager_OnGameLoad(SaveData saveData)
    {
        currentHeartCount = saveData.hearts;
        coinDropChance = saveData.heartDropChance;
        countCoinForDrop = saveData.heartsForDrop;
        jackpotAmount = saveData.jackpotReward;
        OnHeartCountChange?.Invoke(currentHeartCount);
        OnCountCoinForDropChange?.Invoke();
        OnCoinDropChanceChange?.Invoke(coinDropChance);
        OnJackpotAmountChange?.Invoke();
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
