using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private LevelManager levelManager;


    private void Start()
    {
        if (!SaveManager.Instance.HasSave())
            return;
        SaveData save = SaveManager.Instance.LoadGame();
        Debug.Log("Игра была загружена! Сохраненное значение сердец: " + save.hearts);
    }

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
