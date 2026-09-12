using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickHendler : MonoBehaviour, IPointerClickHandler
{
    public event Action OnTotalClickChange;
    public event Action OnClick;

    public event Action<int> OnClickAdded;

    public int TotalClick { get { return totalClick; } }
    public int ClickPower { get { return clickPower; } }

    [SerializeField] private LevelManager levelManager;

    private int clickPower = 1;
    private int totalClick = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        totalClick += clickPower;
        OnClick?.Invoke();
        OnClickAdded?.Invoke(clickPower);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpgradeManager.Instance.OnClickUpgrade += UpgradeManager_OnUpgrade;
        levelManager.OnStageComplete += LevelManager_OnStageComplete;
    }

    private void OnDestroy()
    {
        UpgradeManager.Instance.OnClickUpgrade -= UpgradeManager_OnUpgrade;
        levelManager.OnStageComplete -= LevelManager_OnStageComplete;
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
