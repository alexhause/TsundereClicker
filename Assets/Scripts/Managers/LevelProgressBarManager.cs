using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class LevelProgressBarManager : MonoBehaviour
{

    public float ProgressBarFill { get { return progressBarFill; } }

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Image progressBarFillImage;
    [SerializeField] private GameplayManager gameplayManager;

    private float progressBarFill;


    private void Awake()
    {
        levelManager.OnStageComplete += LevelManager_OnStageComplete;
        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
    }

    private void LevelManager_OnStageComplete()
    {
        progressBarFillImage.fillAmount = 0;
        progressBarFill = 0;
    }

    private void GameplayManager_OnGameLoad(SaveData save)
    {
        progressBarFill = save.levelProgressBarFill;
        progressBarFillImage.fillAmount = progressBarFill;
    }


    public void AddProgress(float amount)
    {
        progressBarFill += amount;
        progressBarFillImage.fillAmount = progressBarFill;
    }
}
