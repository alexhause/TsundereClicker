using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public event Action OnTotalClickChange;
    public event Action OnClick;
    public event Action OnAutoclikerMax;
    public event Action<int> OnClickAdded;

    public int TotalClick { get { return totalClick; } }
    public int ClickPower { get { return clickPower; } }
    public bool IsAutoclickMax { get; private set; }

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private bool autoClickerEnable;
    [SerializeField, Range(1,20)] private int autoclickInterval = 20;

    [SerializeField] private int clickPower = 1;
    private int totalClick = 0;
    private float timer;

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsAutoclickMax = false;
        UpgradeManager.Instance.OnClickUpgrade += UpgradeManager_OnUpgrade;
        UpgradeManager.Instance.OnAutoclickerUnlock += Instance_OnAutoclickerUnlock;
        UpgradeManager.Instance.OnAutoclickerUpgrade += Instance_OnAutoclickerUpgrade;
        levelManager.OnStageComplete += LevelManager_OnStageComplete;
    }

    private void Update()
    {
        if (autoClickerEnable)
        {
            timer += Time.deltaTime;
            if (timer >= (float)autoclickInterval/10)
            {
                timer -= (float)autoclickInterval/10; // так меньше накапливается погрешность
                Click();
            }
        }
    }

    private void OnDestroy()
    {
        UpgradeManager.Instance.OnClickUpgrade -= UpgradeManager_OnUpgrade;
        UpgradeManager.Instance.OnAutoclickerUnlock -= Instance_OnAutoclickerUnlock;
        UpgradeManager.Instance.OnAutoclickerUpgrade -= Instance_OnAutoclickerUpgrade;
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

    private void Instance_OnAutoclickerUpgrade()
    {
        if(autoclickInterval == 2)
        {
            IsAutoclickMax = true;
            autoclickInterval = 1;
            OnAutoclikerMax?.Invoke();
        }

        else
        {
            autoclickInterval -= 1;
        }
    }


    private void Instance_OnAutoclickerUnlock()
    {
        autoClickerEnable = true;
    }

    private void LevelManager_OnStageComplete()
    {
        totalClick = 0;
        OnTotalClickChange?.Invoke();
    }

}
