using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointCountText;
    [SerializeField] private TextMeshProUGUI targetScoreTxt;

    [SerializeField] private TextMeshProUGUI autoclickerUpgradeCostText;
    [SerializeField] private Button autoClickerUpgradeBtn;

  
    [SerializeField] private ClickHandler clickHendler;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameplayManager gameplayManager;


    private void Awake()
    {
        clickHendler.OnClick += ClickHendler_OnClick;
        clickHendler.OnTotalClickChange += ClickHendler_OnTotalClickChange;

        levelManager.OnStageChanged += LevelManager_OnStageChanged;
        levelManager.OnNewLevelStart += LevelManager_OnNewLevelStart;

        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
    }


    private void Start()
    {
        targetScoreTxt.text = levelManager.StageTargetScore.ToString();
    }

    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        clickHendler.OnTotalClickChange -= ClickHendler_OnTotalClickChange;
        
        levelManager.OnStageChanged -= LevelManager_OnStageChanged;
        levelManager.OnNewLevelStart -= LevelManager_OnNewLevelStart;

        gameplayManager.OnGameLoad -= GameplayManager_OnGameLoad;
    }

    #region Методы обработки событий  
    private void ClickHendler_OnClick()
    {
        pointCountText.text = clickHendler.TotalClick.ToString();
    }

    private void GameplayManager_OnGameLoad(SaveData save)
    {
        pointCountText.text = save.currentScore.ToString();
    }

    private void ClickHendler_OnTotalClickChange()
    {
        pointCountText.text = clickHendler.TotalClick.ToString();
    }

    private void LevelManager_OnStageChanged(StageData newStage)
    {
        targetScoreTxt.text = newStage.targetScore.ToString();
    }

    private void LevelManager_OnNewLevelStart(LevelData newLevel)
    {
        targetScoreTxt.text = newLevel.stages[0].targetScore.ToString();
    }
    #endregion
}
