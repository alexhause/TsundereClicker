using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    public event Action OnBarFilled;

    [SerializeField, Range(0, 0.01f)] private float barfillPercentage;

    [SerializeField] private ClickHandler clickHendler;
    [SerializeField] private Image progressBarFillImage;

    [SerializeField] private UpgradeManager upgradeManager;


    private void Awake()
    {
        upgradeManager.OnJackpotFillSpeedUpgrade += Instance_OnJackpotFillSpeedUpgrade;
        clickHendler.OnClick += ClickHendler_OnClick;
    }

    void Start()
    {
       progressBarFillImage.fillAmount = 0;
    }
    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        upgradeManager.OnJackpotUpgrade -= Instance_OnJackpotFillSpeedUpgrade;
    }

    private void ClickHendler_OnClick()
    {
        progressBarFillImage.fillAmount += barfillPercentage;
        if (progressBarFillImage.fillAmount >= 0.99f)
        {
            OnBarFilled?.Invoke();
            progressBarFillImage.fillAmount = 0;
        }
    }

    private void Instance_OnJackpotFillSpeedUpgrade()
    {
        if(barfillPercentage < 0.01f)
        {
            barfillPercentage += 0.001f;
        }
    }
}
