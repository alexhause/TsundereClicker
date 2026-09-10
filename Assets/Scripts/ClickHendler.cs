using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickHendler : MonoBehaviour, IPointerClickHandler
{
    private int clickPower = 1;
    private int totalClick = 0;
    public event Action OnClick;

    public int TotalClick { get { return totalClick; } }


    public void OnPointerClick(PointerEventData eventData)
    {
        totalClick += clickPower;
        OnClick?.Invoke();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpgradeManager.Instance.OnClickUpgrade += UpgradeManager_OnUpgrade;
    }

    private void OnDestroy()
    {
        UpgradeManager.Instance.OnClickUpgrade -= UpgradeManager_OnUpgrade;
    }

    private void UpgradeManager_OnUpgrade()
    {
        clickPower += 1;
    }
}
