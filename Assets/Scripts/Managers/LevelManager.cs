using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public event Action OnStageComplete;
    public event Action<StageData> OnStageChanged;
    public event Action<LevelData> OnNewLevelStart;
    public int StageTargetScore {  get; private set; }

    [SerializeField] private LevelData[] levels;
    [SerializeField] private ClickHendler clickHendler;
    [SerializeField] private GameObject levelProgressBar;

    private LevelProgressBarManager levelProgressBarManager;
    private int currentLevel = 0;
    private int currentStage = 0;

    private void Awake()
    {
        StageTargetScore = levels[currentLevel].stages[currentStage].targetScore;
    }

    private void Start()
    {     
        levelProgressBarManager = levelProgressBar.GetComponent<LevelProgressBarManager>();
        clickHendler.OnClick += ClickHendler_OnClick;
    }

    private void ClickHendler_OnClick()
    {
        if (!IsStageCompleted())
        {
            levelProgressBarManager.AddProgress((float)clickHendler.ClickPower / levels[currentLevel].stages[currentStage].targetScore);
            return;
        }
            
        CompleteStage();
    }

    private bool IsStageCompleted()
    {
        int targetScore = levels[currentLevel]
            .stages[currentStage]
            .targetScore;

        return clickHendler.TotalClick >= targetScore;
    }

    private void CompleteStage()
    {
        Debug.Log("STAGE COMPLETE!");
        OnStageComplete?.Invoke();
        if (IsLastStage())
        {
            CompleteLevel();
        }
        else
        {
            currentStage++;
            StageData nextStage = levels[currentLevel].stages[currentStage];
            OnStageChanged?.Invoke(nextStage);
        }
    }

    private bool IsLastStage()
    {
        return currentStage >= levels[currentLevel].stages.Length - 1;
    }

    private void CompleteLevel()
    {
        Debug.Log("LEVEL COMPLETE!");
        if (IsLastLevel())
        {
            CompleteGame();
        }
        else
        {
            currentLevel++;
            currentStage = 0;
            OnNewLevelStart?.Invoke(levels[currentLevel]);
        }

    }

    private bool IsLastLevel()
    {
        return currentLevel >= levels.Length - 1;
    }

    private void CompleteGame()
    {
        Debug.Log("GAME COMPLETE!");
        currentLevel = 0;
        currentStage = 0;
    }

}
