using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private LevelManager levelManager;

    public void SaveGame()
    {
        SaveData data = new SaveData
        {
            hearts = CoinManager.Instance.CurrentCoinCount,
            clickPower = clickHandler.ClickPower,
            currentLevel = levelManager.CurrentLevel,
            currentStage = levelManager.CurrentStage,
            currentScore = clickHandler.TotalClick,
            autoClicker = clickHandler.AutoClickerEnable
        };
        SaveManager.Instance.SaveGame(data);
    }
}
