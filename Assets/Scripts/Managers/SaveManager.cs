using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    public static SaveManager Instance { get; private set; }
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
    #endregion Singleton
    private const string HEARTS_KEY = "Hearts";
    private const string CLICK_POWER_KEY = "ClickPower";
    private const string CURRENT_LEVEL_KEY = "CurrentLevel";
    private const string CURRENT_STAGE_KEY = "CurrentStage";
    private const string CURRENT_SCORE_KEY = "CurrentScore";
    private const string HEART_DROP_CHANCE_KEY = "HeartDropChance";
    private const string HEART_FOR_DROP_KEY = "HeartForDrop";
    private const string JACKPOT_REWARD_KEY = "JackpotReward";
    private const string JACKPOT_FILL_SPEED_KEY = "JaclpotFillSpeed";
    private const string AUTOCKICKER_KEY = "AutoClicker";
    private const string AUTOCLICKER_POWER_KEY = "AutoClickerPower";

    private const string CLICK_UPGRADE_COST_KEY = "ClickUpgradeCost";
    private const string CHANCE_HEART_DROP_UPGRADE_COST_KEY = "ChanceHeartDropUpgradeCost";
    private const string COUNT_HEART_FOR_DROP_KEY = "CountHeartForDrop";
    private const string JACKPOT_UPGRADE_COST_KEY = "JackpotUpgradeCost";
    private const string JACKPOT_FILL_SPEED_UPGRADE_COST_KEY = "JackpotFillSpeedUpgradeCost";
    private const string AUTOCLICKER_UPGRADE_COST_KEY = "AutoclickerUpgradeCost";

    private const string LEVEL_PROGRESSBER_FILL = "LevelProgressBarFill";

    public void SaveGame(SaveData data)
    {
        PlayerPrefs.SetInt(HEARTS_KEY, data.hearts);
        PlayerPrefs.SetInt(CLICK_POWER_KEY, data.clickPower);
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, data.currentLevel);
        PlayerPrefs.SetInt(CURRENT_STAGE_KEY, data.currentStage);
        PlayerPrefs.SetInt(CURRENT_SCORE_KEY, data.currentScore);
        PlayerPrefs.SetInt(HEART_FOR_DROP_KEY, data.heartsForDrop);

        PlayerPrefs.SetInt(HEART_DROP_CHANCE_KEY, data.heartDropChance);
        PlayerPrefs.SetInt(JACKPOT_REWARD_KEY, data.jackpotReward);
        PlayerPrefs.SetInt(JACKPOT_FILL_SPEED_KEY, data.jackpotProgressBarFillSpeed);

        PlayerPrefs.SetInt(AUTOCKICKER_KEY, data.autoClickerUnlock ? 1 : 0);
        PlayerPrefs.SetInt(AUTOCLICKER_POWER_KEY, data.autoclikerPower);

        PlayerPrefs.SetInt(CLICK_UPGRADE_COST_KEY, data.clickUpgradeCost);
        PlayerPrefs.SetInt(CHANCE_HEART_DROP_UPGRADE_COST_KEY, data.chanceHeartDropUpgradeCost);
        PlayerPrefs.SetInt(COUNT_HEART_FOR_DROP_KEY, data.countHeartForDrop);
        PlayerPrefs.SetInt(JACKPOT_UPGRADE_COST_KEY, data.jackpotUpgradeCost);
        PlayerPrefs.SetInt(JACKPOT_FILL_SPEED_UPGRADE_COST_KEY, data.jackpotFillSpeedUpgradeCost);
        PlayerPrefs.SetInt(AUTOCLICKER_UPGRADE_COST_KEY, data.autoclikerUpgradeCost);
        PlayerPrefs.SetFloat(LEVEL_PROGRESSBER_FILL, data.levelProgressBarFill);


        PlayerPrefs.Save();

        Debug.Log("Игра сохранена!");

        Debug.Log("Сердец: " + PlayerPrefs.GetInt(HEARTS_KEY));
    }

    public SaveData LoadGame()
    {
        return new SaveData
        {
            hearts = PlayerPrefs.GetInt(HEARTS_KEY),
            clickPower = PlayerPrefs.GetInt(CLICK_POWER_KEY),
            currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY),
            currentStage = PlayerPrefs.GetInt(CURRENT_STAGE_KEY),
            currentScore = PlayerPrefs.GetInt(CURRENT_SCORE_KEY),
            heartDropChance = PlayerPrefs.GetInt(HEART_DROP_CHANCE_KEY),
            heartsForDrop = PlayerPrefs.GetInt(HEART_FOR_DROP_KEY),
            jackpotReward = PlayerPrefs.GetInt(JACKPOT_REWARD_KEY),
            autoClickerUnlock = (PlayerPrefs.GetInt(AUTOCKICKER_KEY) == 1),
            autoclikerPower = PlayerPrefs.GetInt(AUTOCLICKER_POWER_KEY),
            clickUpgradeCost = PlayerPrefs.GetInt(CLICK_UPGRADE_COST_KEY),
            chanceHeartDropUpgradeCost = PlayerPrefs.GetInt(CHANCE_HEART_DROP_UPGRADE_COST_KEY),
            countHeartForDrop = PlayerPrefs.GetInt(COUNT_HEART_FOR_DROP_KEY),
            jackpotUpgradeCost = PlayerPrefs.GetInt(JACKPOT_UPGRADE_COST_KEY),
            jackpotFillSpeedUpgradeCost = PlayerPrefs.GetInt(JACKPOT_FILL_SPEED_UPGRADE_COST_KEY),
            autoclikerUpgradeCost = PlayerPrefs.GetInt(AUTOCLICKER_UPGRADE_COST_KEY),
            levelProgressBarFill = PlayerPrefs.GetFloat(LEVEL_PROGRESSBER_FILL),
            jackpotProgressBarFillSpeed = PlayerPrefs.GetInt(JACKPOT_FILL_SPEED_KEY)
        };
    }

    public bool HasSave()
    {
        return PlayerPrefs.HasKey(HEARTS_KEY);
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
