using System;
using UnityEngine;
using UnityEngine.UI;


public class LevelProgressBarManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Image progressBarFillImage;


    private void Awake()
    {
        levelManager.OnStageComplete += LevelManager_OnStageComplete;
    }

    void Start()
    {
        progressBarFillImage.fillAmount = 0;   
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
