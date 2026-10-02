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
    public bool AutoClickerEnable { get { return autoClickerEnable; } }
    public int AutoClickerInterval { get { return autoclickInterval; } }
    public int AutoClickerMinInterval { get { return autoClickerMinInterval; } }

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private bool autoClickerEnable = false;
    [SerializeField, Range(5,20)] private int autoclickInterval = 20;
    [SerializeField] private int autoClickerMinInterval = 5;

    [SerializeField] private int clickPower = 1;
    private int totalClick = 0;
    private float timer;
 

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }


    private void Awake()
    {
        gameplayManager.OnGameLoad += GameplayManager_OnGameLoad;
        upgradeManager.OnClickUpgrade += UpgradeManager_OnUpgrade;
        upgradeManager.OnAutoclickerUnlock += Instance_OnAutoclickerUnlock;
        upgradeManager.OnAutoclickerUpgrade += Instance_OnAutoclickerUpgrade;
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
        upgradeManager.OnClickUpgrade -= UpgradeManager_OnUpgrade;
        upgradeManager.OnAutoclickerUnlock -= Instance_OnAutoclickerUnlock;
        upgradeManager.OnAutoclickerUpgrade -= Instance_OnAutoclickerUpgrade;
        gameplayManager.OnGameLoad -= GameplayManager_OnGameLoad;
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
        if(autoclickInterval == (autoClickerMinInterval + 1))
        {
            IsAutoclickMax = true;
            autoclickInterval = autoClickerMinInterval;
            OnAutoclikerMax?.Invoke();
        }

        else
        {
            autoclickInterval -= 1;
        }
    }
    private void GameplayManager_OnGameLoad(SaveData saveData)
    {
        clickPower = saveData.clickPower;
        totalClick = saveData.currentScore;
        autoClickerEnable = saveData.autoClickerUnlock;
        autoclickInterval = saveData.autoclikerPower;
    }

    private void Instance_OnAutoclickerUnlock()
    {
        autoClickerEnable = true;
    }

    private void LevelManager_OnStageComplete(StageData stage)
    {
        totalClick = 0;
        OnTotalClickChange?.Invoke();
    }

}
