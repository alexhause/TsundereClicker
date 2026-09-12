using System;
using UnityEngine;
using UnityEngine.UI;


public class LevelProgressBarManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Image progressBarFillImage;


    void Start()
    {
        progressBarFillImage.fillAmount = 0;
        levelManager.OnStageComplete += LevelManager_OnStageComplete;
    }

    private void LevelManager_OnStageComplete()
    {
        progressBarFillImage.fillAmount = 0;
    }

    public void AddProgress(float amount)
    {
        progressBarFillImage.fillAmount += amount;
    }
}
