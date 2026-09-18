using TMPro;
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

    private void Start()
    {
        clickHendler.OnClick += ClickHendler_OnClick;
        clickHendler.OnTotalClickChange += ClickHendler_OnTotalClickChange;
        
        levelManager.OnStageChanged += LevelManager_OnStageChanged;
        levelManager.OnNewLevelStart += LevelManager_OnNewLevelStart;
        
        targetScoreTxt.text = levelManager.StageTargetScore.ToString();
    }

    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        clickHendler.OnTotalClickChange -= ClickHendler_OnTotalClickChange;
        


        levelManager.OnStageChanged -= LevelManager_OnStageChanged;
        levelManager.OnNewLevelStart -= LevelManager_OnNewLevelStart;
    }

    #region Методы обработки событий  
    private void ClickHendler_OnClick()
    {
        pointCountText.text = clickHendler.TotalClick.ToString();
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
