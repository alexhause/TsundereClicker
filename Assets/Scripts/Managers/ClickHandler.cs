using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public event Action OnTotalClickChange;
    public event Action OnClick;

    public event Action<int> OnClickAdded;

    public int TotalClick { get { return totalClick; } }
    public int ClickPower { get { return clickPower; } }

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private bool autoClickerEnable;
    [SerializeField] private float autoclickInterval = 1f;

    private int clickPower = 1;
    private int totalClick = 0;
    private float timer;

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpgradeManager.Instance.OnClickUpgrade += UpgradeManager_OnUpgrade;
        levelManager.OnStageComplete += LevelManager_OnStageComplete;
    }

    private void Update()
    {
        if (autoClickerEnable)
        {
            timer += Time.deltaTime;
            if (timer >= autoclickInterval)
            {
                timer -= autoclickInterval; // так меньше накапливается погрешность
                Click();
            }
        }
    }

    private void OnDestroy()
    {
        UpgradeManager.Instance.OnClickUpgrade -= UpgradeManager_OnUpgrade;
        levelManager.OnStageComplete -= LevelManager_OnStageComplete;
    }

    private void Click()
    {
        totalClick += clickPower;
        OnClick?.Invoke();
        OnClickAdded?.Invoke(clickPower);
    }

    private void UpgradeManager_OnUpgrade()
    {
        clickPower += 1;
    }

    private void LevelManager_OnStageComplete()
    {
        totalClick = 0;
        OnTotalClickChange?.Invoke();
    }

}
