using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI pointCountText;
    [SerializeField] private TextMeshProUGUI currentCoinCountText;
    [SerializeField] private TextMeshProUGUI chanceCoinDropText;
    [SerializeField] private TextMeshProUGUI countCoinForDropText;

    [SerializeField] private TextMeshProUGUI clickUpgradeCostText;
    [SerializeField] private TextMeshProUGUI chanceCoinDropUpgradeCost;
    [SerializeField] private TextMeshProUGUI countCoinForDropUgradeCostText;
    [SerializeField] private TextMeshProUGUI jackpotUpgradeCostText;
    [SerializeField] private TextMeshProUGUI autoclickerUpgradeCostText;

    [SerializeField] private Button autoClickerUpgradeBtn;

    [SerializeField] private TextMeshProUGUI jackpotAmountText;

    [SerializeField] private TextMeshProUGUI targetScoreTxt;

    [SerializeField] private Button coinChanceUpgradeBtn;


    [SerializeField] private ClickHandler clickHendler;
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        clickHendler.OnClick += ClickHendler_OnClick;
        clickHendler.OnTotalClickChange += ClickHendler_OnTotalClickChange;
        clickHendler.OnAutoclikerMax += ClickHendler_OnAutoclikerMax;

        CoinManager.Instance.OnCoinDrop += CoinManager_OnCoinDrop;
        CoinManager.Instance.OnCoinChange += CoinManager_OnCoinChange;
        CoinManager.Instance.OnMaxDropChance += CoinManager_OnMaxDropChance;
        CoinManager.Instance.OnCoinDropChanceChange += CoinManager_OnCoinDropChanceChange;
        CoinManager.Instance.OnCountCoinForDropChange += CoinManager_OnCountCoinForDropChange;
        CoinManager.Instance.OnJackpotAmountChange += CoinManager_OnJackpotAmountChange;

        UpgradeManager.Instance.OnClickUpgrade += UpgradeManager_OnClickUpgrade;
        UpgradeManager.Instance.OnChanceCoinDropUpgrade += UpgradeManager_OnChanceCoinDropUpgrade;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade += UpgradeManager_OnCoutCoinForDropUpgrade;
        UpgradeManager.Instance.OnJackpotUpgrade += UpgradeManager_OnJackpotUpgrade;
        UpgradeManager.Instance.OnAutoclickerUnlock += Instance_OnAutoclickerUnlock;
        UpgradeManager.Instance.OnAutoclickerUpgrade += Instance_OnAutoclickerUpgrade;

        levelManager.OnStageChanged += LevelManager_OnStageChanged;
        levelManager.OnNewLevelStart += LevelManager_OnNewLevelStart;


        clickUpgradeCostText.text = UpgradeManager.Instance.ClickUpgradeCost.ToString();
        chanceCoinDropText.text = CoinManager.Instance.CoinDropChance.ToString() + "%";
        chanceCoinDropUpgradeCost.text = UpgradeManager.Instance.ChanceCoinDropUpgradeCost.ToString();
        countCoinForDropUgradeCostText.text = UpgradeManager.Instance.CountCoinForDroupgradeCost.ToString();
        jackpotUpgradeCostText.text = UpgradeManager.Instance.JackpotUpgradeCost.ToString();
        countCoinForDropText.text = "+" + CoinManager.Instance.CountCoinForDrop.ToString();
        jackpotAmountText.text = "+" + CoinManager.Instance.JackpotAmount.ToString();
        targetScoreTxt.text = levelManager.StageTargetScore.ToString();
        autoclickerUpgradeCostText.text = UpgradeManager.Instance.AutoclickerUnlockCost.ToString();
    }

    private void OnDestroy()
    {
        clickHendler.OnClick -= ClickHendler_OnClick;
        clickHendler.OnTotalClickChange -= ClickHendler_OnTotalClickChange;
        clickHendler.OnAutoclikerMax -= ClickHendler_OnAutoclikerMax;

        CoinManager.Instance.OnCoinDrop -= CoinManager_OnCoinDrop;
        CoinManager.Instance.OnCoinChange -= CoinManager_OnCoinChange;
        CoinManager.Instance.OnJackpotAmountChange -= CoinManager_OnJackpotAmountChange;

        UpgradeManager.Instance.OnClickUpgrade -= UpgradeManager_OnClickUpgrade;
        UpgradeManager.Instance.OnChanceCoinDropUpgrade -= UpgradeManager_OnChanceCoinDropUpgrade;
        UpgradeManager.Instance.OnCoutCoinForDropUpgrade -= UpgradeManager_OnCoutCoinForDropUpgrade;
        UpgradeManager.Instance.OnAutoclickerUnlock -= Instance_OnAutoclickerUnlock;
        UpgradeManager.Instance.OnAutoclickerUpgrade -= Instance_OnAutoclickerUpgrade;

        levelManager.OnStageChanged -= LevelManager_OnStageChanged;
        levelManager.OnNewLevelStart -= LevelManager_OnNewLevelStart;
    }

    #region Методы обработки событий  
    private void CoinManager_OnMaxDropChance()
    {
        coinChanceUpgradeBtn.enabled = false;
        chanceCoinDropUpgradeCost.text = "MAX";
    }

    private void CoinManager_OnJackpotAmountChange()
    {
        jackpotAmountText.text = "+" + CoinManager.Instance.JackpotAmount.ToString();
    }

    private void CoinManager_OnCoinDropChanceChange(float obj)
    {
        chanceCoinDropText.text = CoinManager.Instance.CoinDropChance.ToString() + "%";
    }

    private void CoinManager_OnCoinDrop(int currentCoinCount)
    {
        currentCoinCountText.text = currentCoinCount.ToString();
    }

    private void CoinManager_OnCountCoinForDropChange()
    {
        countCoinForDropText.text = "+" + CoinManager.Instance.CountCoinForDrop.ToString();
    }
    private void CoinManager_OnCoinChange(int newCoinCount)
    {
        currentCoinCountText.text = newCoinCount.ToString();
    }

    private void UpgradeManager_OnCoutCoinForDropUpgrade()
    {
        countCoinForDropUgradeCostText.text = UpgradeManager.Instance.CountCoinForDroupgradeCost.ToString();
    }

    private void UpgradeManager_OnChanceCoinDropUpgrade()
    {
        chanceCoinDropUpgradeCost.text = UpgradeManager.Instance.ChanceCoinDropUpgradeCost.ToString();
    }

    private void UpgradeManager_OnClickUpgrade()
    {
        clickUpgradeCostText.text = UpgradeManager.Instance.ClickUpgradeCost.ToString();
    }

    private void UpgradeManager_OnJackpotUpgrade()
    {
        jackpotUpgradeCostText.text = UpgradeManager.Instance.JackpotUpgradeCost.ToString();
    }

    private void Instance_OnAutoclickerUpgrade()
    {
        autoclickerUpgradeCostText.text = UpgradeManager.Instance.AutoclickerUpgradeCost.ToString();
    }

    private void Instance_OnAutoclickerUnlock()
    {
        autoClickerUpgradeBtn.interactable = true;
        autoclickerUpgradeCostText.text = UpgradeManager.Instance.AutoclickerUpgradeCost.ToString();
    }

    private void ClickHendler_OnClick()
    {
        pointCountText.text = clickHendler.TotalClick.ToString();
    }
    private void ClickHendler_OnTotalClickChange()
    {
        pointCountText.text = clickHendler.TotalClick.ToString();
    }

    private void ClickHendler_OnAutoclikerMax()
    {
        autoClickerUpgradeBtn.interactable = false;
        autoclickerUpgradeCostText.text = "MAX";
    }


    private void LevelManager_OnStageChanged(StageData newStage)
    {
        targetScoreTxt.text = newStage.targetScore.ToString();
    }

    private void LevelManager_OnNewLevelStart(LevelData newLevel)
    {
        targetScoreTxt.text = newLevel.stages[0].targetScore.ToString();
    }

    #endregion


}
