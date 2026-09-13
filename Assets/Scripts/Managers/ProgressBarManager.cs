using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    public event Action OnBarFilled;

    [SerializeField, Range(0, 1f)] private float barfillPercentage;

    [SerializeField] private ClickHendler clickHendler;
    [SerializeField] private Image progressBarFillImage;
    

    void Start()
    {
       progressBarFillImage.fillAmount = 0;

       clickHendler.OnClick += ClickHendler_OnClick;
        UpgradeManager.Instance.OnJackpotUpgrade += UpgradeManager_OnJackpotUpgrade;
    }

    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        UpgradeManager.Instance.OnJackpotUpgrade -= UpgradeManager_OnJackpotUpgrade;

    }

    // Update is called once per frame
    void Update()
    {
        if (progressBarFillImage.fillAmount > 0)
        {
            progressBarFillImage.fillAmount -= 0.1f * Time.deltaTime;
        }
        if(progressBarFillImage.fillAmount >= 0.99f)
        {
            OnBarFilled?.Invoke();
            progressBarFillImage.fillAmount = 0;
        }
    }

    private void ClickHendler_OnClick()
    {
        progressBarFillImage.fillAmount += barfillPercentage;
    }

    private void UpgradeManager_OnJackpotUpgrade()
    {
        if(barfillPercentage > 0.02f)
        {
            barfillPercentage -= 0.001f;
        }
    }
}
