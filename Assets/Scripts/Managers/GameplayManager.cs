using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public event Action<SaveData> OnGameLoad;

    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private UpgradeManager upgradeManager;

    private void Start()
    {
        if (!SaveManager.Instance.HasSave())
            return;
        SaveData save = SaveManager.Instance.LoadGame();
        OnGameLoad?.Invoke(save);
        Debug.Log("Игра была загружена! Сохраненное значение клика: " + save.clickPower);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData
        {
            hearts = coinManager.CurrentCoinCount,
            clickPower = clickHandler.ClickPower,
            currentLevel = levelManager.CurrentLevel,
            currentStage = levelManager.CurrentStage,
            currentScore = clickHandler.TotalClick,
            heartDropChance = coinManager.CoinDropChance,
            heartsForDrop = coinManager.CountCoinForDrop,
            autoClickerUnlock = clickHandler.AutoClickerEnable,
            clickUpgradeCost = upgradeManager.ClickUpgradeCost,
            chanceHeartDropUpgradeCost = upgradeManager.ChanceCoinDropUpgradeCost,
            countHeartForDrop = coinManager.CountCoinForDrop,
            jackpotFillSpeedUpgradeCost = upgradeManager.JackpotFillSpeedUpgradeCost,
            jackpotUpgradeCost = upgradeManager.JackpotUpgradeCost,
            jackpotReward = coinManager.JackpotAmount,
            autoclikerUpgradeCost = upgradeManager.AutoclickerUpgradeCost,
            autoclikerPower = clickHandler.AutoClickerInterval
        };
        SaveManager.Instance.SaveGame(data);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
