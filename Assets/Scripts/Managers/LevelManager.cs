using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public event Action OnStageComplete;
    public event Action<StageData> OnStageChanged;
    public event Action<LevelData> OnNewLevelStart;
    public int StageTargetScore {  get; private set; }
    public int CurrentScore { get { return currentScore; } }
    public int CurrentLevel { get {  return currentLevel; }  }
    public int CurrentStage {  get { return currentStage; } }

    [SerializeField] private LevelData[] levels;
    [SerializeField] private ClickHandler clickHendler;
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private GameObject levelProgressBar;

    private LevelProgressBarManager levelProgressBarManager;
    private int currentLevel = 0;
    private int currentStage = 0;
    private int currentScore;

    private void Awake()
    {
        StageTargetScore = levels[currentLevel].stages[currentStage].targetScore;
        clickHendler.OnClick += ClickHendler_OnClick;
        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
    }

    private void Start()
    {     
        levelProgressBarManager = levelProgressBar.GetComponent<LevelProgressBarManager>();
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
    private void GameplayManager_OnGameLoad(SaveData save)
    {
        currentLevel = save.currentLevel;
        currentStage = save.currentStage;
        currentScore = save.currentScore;
        OnStageChanged?.Invoke(levels[currentLevel].stages[currentStage]);
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
