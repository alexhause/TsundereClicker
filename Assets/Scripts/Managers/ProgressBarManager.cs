using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    public event Action OnBarFilled;

    [SerializeField, Range(0, 0.01f)] private float barfillPercentage;

    [SerializeField] private ClickHandler clickHendler;
    [SerializeField] private Image progressBarFillImage;
    

    void Start()
    {
       progressBarFillImage.fillAmount = 0;

       clickHendler.OnClick += ClickHendler_OnClick;
        UpgradeManager.Instance.OnJackpotFillSpeedUpgrade += Instance_OnJackpotFillSpeedUpgrade;
    }
    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        UpgradeManager.Instance.OnJackpotUpgrade -= Instance_OnJackpotFillSpeedUpgrade;
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
