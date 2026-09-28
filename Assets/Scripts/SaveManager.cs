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
    private const string JACKPOT_REWARD_KEY = "JackpotReward";
    private const string AUTOCKICKER_KEY = "AutoClicker";
    private const string AUTOCLICKER_POWER_KEY = "AutoClickerPower";

    public void SaveGame(SaveData data)
    {
        PlayerPrefs.SetInt(HEARTS_KEY, data.hearts);
        PlayerPrefs.SetInt(CLICK_POWER_KEY, data.clickPower);
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, data.currentLevel);
        PlayerPrefs.SetInt(CURRENT_STAGE_KEY, data.currentStage);
        PlayerPrefs.SetInt(CURRENT_SCORE_KEY, data.currentScore);

        PlayerPrefs.SetFloat(HEART_DROP_CHANCE_KEY, data.heartDropChance);
        PlayerPrefs.SetInt(JACKPOT_REWARD_KEY, data.jackpotReward);

        PlayerPrefs.SetInt(AUTOCKICKER_KEY, data.autoClicker ? 1 : 0);
        PlayerPrefs.SetInt(AUTOCLICKER_POWER_KEY, data.autoclikerPower);
        
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
            jackpotReward = PlayerPrefs.GetInt(JACKPOT_REWARD_KEY),
            autoClicker = (PlayerPrefs.GetInt(AUTOCKICKER_KEY) == 1),
            autoclikerPower = PlayerPrefs.GetInt(AUTOCLICKER_POWER_KEY)
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
