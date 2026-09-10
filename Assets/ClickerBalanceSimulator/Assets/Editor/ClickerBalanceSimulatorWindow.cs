#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ClickerBalanceSimulatorWindow : EditorWindow
{
    private BalanceConfig config = new BalanceConfig();
    private Vector2 scroll;
    private SimulationReport lastReport;
    private string outputFolder = "Assets/BalanceSimulation";

    [MenuItem("Tools/Clicker Balance Simulator")]
    public static void ShowWindow()
    {
        var window = GetWindow<ClickerBalanceSimulatorWindow>("Clicker Balance Simulator");
        window.minSize = new Vector2(900, 700);
    }

    private void OnGUI()
    {
        scroll = EditorGUILayout.BeginScrollView(scroll);

        EditorGUILayout.Space(8);
        EditorGUILayout.LabelField("Clicker Balance Simulator", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Monte Carlo симулятор экономики кликера. Он моделирует клики, обычные выпадения монет, шкалу джекпота, покупки улучшений и прохождение 9 этапов.",
            MessageType.Info);

        DrawGeneralSettings();
        DrawStageSettings();
        DrawUpgradeSettings();
        DrawSimulationSettings();
        DrawButtons();

        if (lastReport != null)
            DrawReport();

        EditorGUILayout.EndScrollView();
    }

    private void DrawGeneralSettings()
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("1. Стартовые параметры", EditorStyles.boldLabel);

        config.startPointsPerClick = EditorGUILayout.IntField("Очки за клик", config.startPointsPerClick);
        config.startCoinsPerDrop = EditorGUILayout.IntField("Монет за выпадение", config.startCoinsPerDrop);
        config.startCoinChance = EditorGUILayout.Slider("Шанс монеты", config.startCoinChance, 0f, 1f);
        config.startJackpotIncrement = EditorGUILayout.Slider("Прирост шкалы джекпота", config.startJackpotIncrement, 0.001f, 1f);
        config.startJackpotReward = EditorGUILayout.IntField("Награда джекпота", config.startJackpotReward);
    }

    private void DrawStageSettings()
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("2. Цели этапов", EditorStyles.boldLabel);

        if (config.stageGoals == null || config.stageGoals.Length != 9)
            config.stageGoals = new[] { 100, 250, 500, 900, 1600, 3000, 5500, 10000, 18000 };

        string[] labels =
        {
            "Уровень 1 — Этап 1", "Уровень 1 — Этап 2", "Уровень 1 — Этап 3",
            "Уровень 2 — Этап 1", "Уровень 2 — Этап 2", "Уровень 2 — Этап 3",
            "Уровень 3 — Этап 1", "Уровень 3 — Этап 2", "Уровень 3 — Этап 3"
        };

        for (int i = 0; i < config.stageGoals.Length; i++)
            config.stageGoals[i] = Mathf.Max(1, EditorGUILayout.IntField(labels[i], config.stageGoals[i]));
    }

    private void DrawUpgradeSettings()
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("3. Улучшения", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Очки за клик", EditorStyles.miniBoldLabel);
        config.pointsUpgradeInitialCost = Mathf.Max(0, EditorGUILayout.IntField("Начальная цена", config.pointsUpgradeInitialCost));
        config.pointsUpgradeCostMultiplier = Mathf.Max(1f, EditorGUILayout.FloatField("Множитель цены", config.pointsUpgradeCostMultiplier));
        config.pointsUpgradeValue = Mathf.Max(1, EditorGUILayout.IntField("+ очков за уровень", config.pointsUpgradeValue));

        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Количество монет за выпадение", EditorStyles.miniBoldLabel);
        config.coinsUpgradeInitialCost = Mathf.Max(0, EditorGUILayout.IntField("Начальная цена", config.coinsUpgradeInitialCost));
        config.coinsUpgradeCostMultiplier = Mathf.Max(1f, EditorGUILayout.FloatField("Множитель цены", config.coinsUpgradeCostMultiplier));
        config.coinsUpgradeValue = Mathf.Max(1, EditorGUILayout.IntField("+ монет за уровень", config.coinsUpgradeValue));

        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Шанс выпадения монеты", EditorStyles.miniBoldLabel);
        config.chanceUpgradeInitialCost = Mathf.Max(0, EditorGUILayout.IntField("Начальная цена", config.chanceUpgradeInitialCost));
        config.chanceUpgradeCostMultiplier = Mathf.Max(1f, EditorGUILayout.FloatField("Множитель цены", config.chanceUpgradeCostMultiplier));
        config.chanceUpgradeValue = Mathf.Clamp(EditorGUILayout.FloatField("+ шанс", config.chanceUpgradeValue), 0.0001f, 1f);
        config.maxCoinChance = Mathf.Clamp01(EditorGUILayout.FloatField("Максимальный шанс", config.maxCoinChance));

        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Джекпот", EditorStyles.miniBoldLabel);
        config.jackpotUpgradeInitialCost = Mathf.Max(0, EditorGUILayout.IntField("Начальная цена", config.jackpotUpgradeInitialCost));
        config.jackpotUpgradeCostMultiplier = Mathf.Max(1f, EditorGUILayout.FloatField("Множитель цены", config.jackpotUpgradeCostMultiplier));
        config.jackpotRewardMultiplier = Mathf.Max(1f, EditorGUILayout.FloatField("Множитель награды", config.jackpotRewardMultiplier));

        EditorGUILayout.LabelField("Прирост шкалы по уровням", EditorStyles.miniBoldLabel);
        config.jackpotIncrements = DrawFloatArray(config.jackpotIncrements);

        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Примечание: если уровней джекпота больше, чем значений прироста шкалы, используется последнее значение массива.", EditorStyles.helpBox);
    }

    private void DrawSimulationSettings()
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("4. Симуляция", EditorStyles.boldLabel);
        config.simulationsPerStrategy = Mathf.Max(1, EditorGUILayout.IntField("Симуляций на стратегию", config.simulationsPerStrategy));
        config.maxClicksPerSimulation = Mathf.Max(100, EditorGUILayout.IntField("Лимит кликов на симуляцию", config.maxClicksPerSimulation));
        config.randomSeed = EditorGUILayout.IntField("Seed (0 = случайный)", config.randomSeed);
        config.allowMultiplePurchasesPerStage = EditorGUILayout.Toggle("Покупать несколько улучшений за этап", config.allowMultiplePurchasesPerStage);

        EditorGUILayout.Space(4);
        outputFolder = EditorGUILayout.TextField("Папка CSV", outputFolder);
    }

    private float[] DrawFloatArray(float[] values)
    {
        values ??= Array.Empty<float>();

        int newSize = Mathf.Max(1, EditorGUILayout.IntField("Количество уровней", values.Length));
        if (newSize != values.Length)
        {
            var resized = new float[newSize];
            for (int i = 0; i < newSize; i++)
                resized[i] = i < values.Length ? values[i] : values.Length > 0 ? values[values.Length - 1] : 0.02f;
            values = resized;
        }

        for (int i = 0; i < values.Length; i++)
            values[i] = Mathf.Clamp(EditorGUILayout.FloatField($"Уровень {i + 1}", values[i]), 0.001f, 1f);

        return values;
    }

    private void DrawButtons()
    {
        EditorGUILayout.Space(12);

        if (GUILayout.Button("Запустить симуляцию", GUILayout.Height(36)))
        {
            config.ClampValues();
            lastReport = BalanceSimulator.Run(config);
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(lastReport == null);
        if (GUILayout.Button("Сохранить CSV", GUILayout.Height(28)))
            SaveCsv();
        EditorGUI.EndDisabledGroup();

        if (GUILayout.Button("Сбросить к базовому балансу", GUILayout.Height(28)))
        {
            config = new BalanceConfig();
            lastReport = null;
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawReport()
    {
        EditorGUILayout.Space(15);
        EditorGUILayout.LabelField("Результаты", EditorStyles.boldLabel);

        foreach (var strategy in lastReport.Strategies)
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField(strategy.StrategyName, EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Средний финальный баланс: {strategy.AverageFinalCoins:F1} монет");
            EditorGUILayout.LabelField($"Среднее количество кликов до конца: {strategy.AverageTotalClicks:F0}");

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Этап", "Средние клики | Монеты после этапа | Улучшения");
            foreach (var stage in strategy.Stages)
            {
                EditorGUILayout.LabelField(
                    $"L{stage.Level} / E{stage.Stage}",
                    $"{stage.AverageClicks:F0} | {stage.AverageCoinsAfterStage:F1} | {stage.AverageUpgradeSummary}");
            }
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space(8);
        EditorGUILayout.HelpBox(
            "Как читать результат: сравни стратегии. Если одна стратегия стабильно проходит этапы быстрее и при этом накапливает больше монет, у неё может быть слишком выгодный апгрейд. Большой разброс между min/median/max указывает на сильное влияние RNG.",
            MessageType.None);
    }

    private void SaveCsv()
    {
        if (lastReport == null) return;

        string folder = outputFolder.Replace('\\', '/').TrimEnd('/');
        if (!AssetDatabase.IsValidFolder(folder))
        {
            string parent = "Assets";
            string[] parts = folder.Split('/');
            foreach (string part in parts.Skip(1))
            {
                string next = parent + "/" + part;
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(parent, part);
                parent = next;
            }
        }

        string path = folder + "/clicker_balance_simulation.csv";
        var lines = new List<string>
        {
            "Strategy,Level,Stage,Goal,AverageClicks,MedianClicks,P10Clicks,P90Clicks,AverageCoinsAfterStage,MedianCoinsAfterStage,AveragePointsPerClick,AverageCoinChance,AverageCoinsPerDrop,AverageJackpotReward,AverageJackpotIncrement,AverageUpgradeSummary"
        };

        foreach (var strategy in lastReport.Strategies)
        {
            foreach (var stage in strategy.Stages)
            {
                lines.Add(string.Join(",", new[]
                {
                    Csv(strategy.StrategyName),
                    stage.Level.ToString(CultureInfo.InvariantCulture),
                    stage.Stage.ToString(CultureInfo.InvariantCulture),
                    stage.Goal.ToString(CultureInfo.InvariantCulture),
                    stage.AverageClicks.ToString("F2", CultureInfo.InvariantCulture),
                    stage.MedianClicks.ToString("F2", CultureInfo.InvariantCulture),
                    stage.P10Clicks.ToString("F2", CultureInfo.InvariantCulture),
                    stage.P90Clicks.ToString("F2", CultureInfo.InvariantCulture),
                    stage.AverageCoinsAfterStage.ToString("F2", CultureInfo.InvariantCulture),
                    stage.MedianCoinsAfterStage.ToString("F2", CultureInfo.InvariantCulture),
                    stage.AveragePointsPerClick.ToString("F2", CultureInfo.InvariantCulture),
                    stage.AverageCoinChance.ToString("P2", CultureInfo.InvariantCulture),
                    stage.AverageCoinsPerDrop.ToString("F2", CultureInfo.InvariantCulture),
                    stage.AverageJackpotReward.ToString("F2", CultureInfo.InvariantCulture),
                    stage.AverageJackpotIncrement.ToString("F4", CultureInfo.InvariantCulture),
                    Csv(stage.AverageUpgradeSummary)
                }));
            }
        }

        File.WriteAllLines(path, lines);
        AssetDatabase.Refresh();
        EditorUtility.RevealInFinder(Path.GetFullPath(path));
        Debug.Log("Clicker Balance Simulator: CSV сохранён в " + path);
    }

    private static string Csv(string value)
    {
        if (value == null) return "\"\"";
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}

[Serializable]
public class BalanceConfig
{
    public int startPointsPerClick = 1;
    public int startCoinsPerDrop = 1;
    public float startCoinChance = 0f;
    public float startJackpotIncrement = 0.10f;
    public int startJackpotReward = 5;

    public int[] stageGoals = { 100, 250, 500, 900, 1600, 3000, 5500, 10000, 18000 };

    public int pointsUpgradeInitialCost = 10;
    public float pointsUpgradeCostMultiplier = 1.55f;
    public int pointsUpgradeValue = 1;

    public int coinsUpgradeInitialCost = 20;
    public float coinsUpgradeCostMultiplier = 1.65f;
    public int coinsUpgradeValue = 1;

    public int chanceUpgradeInitialCost = 10;
    public float chanceUpgradeCostMultiplier = 1.60f;
    public float chanceUpgradeValue = 0.02f;
    public float maxCoinChance = 1f;

    public int jackpotUpgradeInitialCost = 40;
    public float jackpotUpgradeCostMultiplier = 1.80f;
    public float jackpotRewardMultiplier = 1.50f;
    public float[] jackpotIncrements = { 0.08f, 0.06f, 0.04f, 0.03f, 0.02f };

    public int simulationsPerStrategy = 1000;
    public int maxClicksPerSimulation = 250000;
    public int randomSeed = 12345;
    public bool allowMultiplePurchasesPerStage = true;

    public void ClampValues()
    {
        startPointsPerClick = Mathf.Max(1, startPointsPerClick);
        startCoinsPerDrop = Mathf.Max(1, startCoinsPerDrop);
        startCoinChance = Mathf.Clamp01(startCoinChance);
        startJackpotIncrement = Mathf.Clamp(startJackpotIncrement, 0.001f, 1f);
        startJackpotReward = Mathf.Max(1, startJackpotReward);

        pointsUpgradeInitialCost = Mathf.Max(0, pointsUpgradeInitialCost);
        pointsUpgradeCostMultiplier = Mathf.Max(1f, pointsUpgradeCostMultiplier);
        pointsUpgradeValue = Mathf.Max(1, pointsUpgradeValue);

        coinsUpgradeInitialCost = Mathf.Max(0, coinsUpgradeInitialCost);
        coinsUpgradeCostMultiplier = Mathf.Max(1f, coinsUpgradeCostMultiplier);
        coinsUpgradeValue = Mathf.Max(1, coinsUpgradeValue);

        chanceUpgradeInitialCost = Mathf.Max(0, chanceUpgradeInitialCost);
        chanceUpgradeCostMultiplier = Mathf.Max(1f, chanceUpgradeCostMultiplier);
        chanceUpgradeValue = Mathf.Clamp(chanceUpgradeValue, 0.0001f, 1f);
        maxCoinChance = Mathf.Clamp01(maxCoinChance);

        jackpotUpgradeInitialCost = Mathf.Max(0, jackpotUpgradeInitialCost);
        jackpotUpgradeCostMultiplier = Mathf.Max(1f, jackpotUpgradeCostMultiplier);
        jackpotRewardMultiplier = Mathf.Max(1f, jackpotRewardMultiplier);

        simulationsPerStrategy = Mathf.Max(1, simulationsPerStrategy);
        maxClicksPerSimulation = Mathf.Max(100, maxClicksPerSimulation);
        stageGoals ??= Array.Empty<int>();
        jackpotIncrements ??= Array.Empty<float>();

        for (int i = 0; i < stageGoals.Length; i++) stageGoals[i] = Mathf.Max(1, stageGoals[i]);
        for (int i = 0; i < jackpotIncrements.Length; i++) jackpotIncrements[i] = Mathf.Clamp(jackpotIncrements[i], 0.001f, 1f);
    }
}

public static class BalanceSimulator
{
    private enum StrategyType
    {
        Points,
        Economy,
        Jackpot,
        Balanced
    }

    private sealed class RunState
    {
        public int Score;
        public long Coins;
        public float JackpotProgress;

        public int PointsPerClick;
        public int CoinsPerDrop;
        public float CoinChance;
        public int JackpotReward;
        public float JackpotIncrement;

        public int PointsUpgradeLevel;
        public int CoinsUpgradeLevel;
        public int ChanceUpgradeLevel;
        public int JackpotUpgradeLevel;

        public int TotalClicks;
        public int JackpotCount;
    }

    public static SimulationReport Run(BalanceConfig config)
    {
        config.ClampValues();

        var report = new SimulationReport();
        var strategies = new[]
        {
            (StrategyType.Points, "Points-first"),
            (StrategyType.Economy, "Economy-first"),
            (StrategyType.Jackpot, "Jackpot-first"),
            (StrategyType.Balanced, "Balanced")
        };

        foreach (var (type, name) in strategies)
        {
            var strategyReport = new StrategyReport { StrategyName = name };
            var stageData = new List<List<StageRunData>>();

            for (int sim = 0; sim < config.simulationsPerStrategy; sim++)
            {
                int seed = config.randomSeed == 0 ? Environment.TickCount + sim * 7919 + (int)type * 104729 : config.randomSeed + sim * 1009 + (int)type * 7919;
                var rng = new System.Random(seed);
                var state = new RunState
                {
                    Score = 0,
                    Coins = 0,
                    JackpotProgress = 0f,
                    PointsPerClick = config.startPointsPerClick,
                    CoinsPerDrop = config.startCoinsPerDrop,
                    CoinChance = config.startCoinChance,
                    JackpotReward = config.startJackpotReward,
                    JackpotIncrement = config.startJackpotIncrement
                };

                for (int stageIndex = 0; stageIndex < config.stageGoals.Length; stageIndex++)
                {
                    int level = stageIndex / 3 + 1;
                    int stage = stageIndex % 3 + 1;
                    int goal = config.stageGoals[stageIndex];
                    int clicksBefore = state.TotalClicks;
                    long coinsBefore = state.Coins;
                    int purchasesThisStage = 0;

                    while (state.Score < goal && state.TotalClicks < config.maxClicksPerSimulation)
                    {
                        SimulateClick(config, state, rng);
                        TryPurchases(config, state, type, stageIndex, ref purchasesThisStage);
                    }

                    int clicks = state.TotalClicks - clicksBefore;
                    stageData.AddOrEnsure(stageIndex, new List<StageRunData>());
                    stageData[stageIndex].Add(new StageRunData
                    {
                        Clicks = clicks,
                        CoinsAfterStage = state.Coins,
                        PointsPerClick = state.PointsPerClick,
                        CoinChance = state.CoinChance,
                        CoinsPerDrop = state.CoinsPerDrop,
                        JackpotReward = state.JackpotReward,
                        JackpotIncrement = state.JackpotIncrement,
                        PointsUpgradeLevel = state.PointsUpgradeLevel,
                        CoinsUpgradeLevel = state.CoinsUpgradeLevel,
                        ChanceUpgradeLevel = state.ChanceUpgradeLevel,
                        JackpotUpgradeLevel = state.JackpotUpgradeLevel,
                        PurchasesThisStage = purchasesThisStage,
                        Goal = goal,
                        ReachedGoal = state.Score >= goal
                    });

                    // Stage completion: score is spent/reset, upgrades and coins remain.
                    state.Score = 0;
                }
            }

            for (int stageIndex = 0; stageIndex < config.stageGoals.Length; stageIndex++)
            {
                var runs = stageData[stageIndex];
                var summary = BuildStageSummary(runs, stageIndex);
                strategyReport.Stages.Add(summary);
            }

            strategyReport.AverageFinalCoins = strategyReport.Stages.Last().AverageCoinsAfterStage;
            strategyReport.AverageTotalClicks = strategyReport.Stages.Sum(s => s.AverageClicks);
            report.Strategies.Add(strategyReport);
        }

        return report;
    }

    private static void SimulateClick(BalanceConfig config, RunState state, System.Random rng)
    {
        state.TotalClicks++;
        state.Score += state.PointsPerClick;

        if (Next01(rng) < state.CoinChance)
            state.Coins += state.CoinsPerDrop;

        state.JackpotProgress += state.JackpotIncrement;

        while (state.JackpotProgress >= 1f)
        {
            state.JackpotProgress -= 1f;
            state.Coins += state.JackpotReward;
            state.JackpotCount++;
        }
    }

    private static void TryPurchases(BalanceConfig config, RunState state, StrategyType strategy, int stageIndex, ref int purchasesThisStage)
    {
        if (!config.allowMultiplePurchasesPerStage && purchasesThisStage > 0)
            return;

        int safety = 0;
        while (safety++ < 1000)
        {
            UpgradeChoice choice = ChooseUpgrade(config, state, strategy, stageIndex);
            if (!choice.Valid || state.Coins < choice.Cost)
                return;

            state.Coins -= choice.Cost;
            ApplyUpgrade(config, state, choice.Type);
            purchasesThisStage++;

            if (!config.allowMultiplePurchasesPerStage)
                return;
        }
    }

    private static UpgradeChoice ChooseUpgrade(BalanceConfig config, RunState state, StrategyType strategy, int stageIndex)
    {
        var choices = new List<UpgradeChoice>
        {
            BuildChoice(config, state, UpgradeType.Points),
            BuildChoice(config, state, UpgradeType.Coins),
            BuildChoice(config, state, UpgradeType.Chance),
            BuildChoice(config, state, UpgradeType.Jackpot)
        };

        choices = choices.Where(c => c.Valid).ToList();
        if (choices.Count == 0) return default;

        // The strategies use simple utility scores rather than an omniscient optimal solver.
        // This makes it easy to compare predictable playstyles.
        for (int i = 0; i < choices.Count; i++)
        {
            var choice = choices[i];
            float utility = 0f;
            switch (strategy)
            {
                case StrategyType.Points:
                    utility = choice.Type == UpgradeType.Points ? 100f : choice.Type == UpgradeType.Chance ? 6f : 2f;
                    break;
                case StrategyType.Economy:
                    utility = choice.Type == UpgradeType.Chance ? 100f : choice.Type == UpgradeType.Coins ? 70f : choice.Type == UpgradeType.Jackpot ? 40f : 5f;
                    break;
                case StrategyType.Jackpot:
                    utility = choice.Type == UpgradeType.Jackpot ? 100f : choice.Type == UpgradeType.Chance ? 30f : choice.Type == UpgradeType.Coins ? 15f : 2f;
                    break;
                case StrategyType.Balanced:
                    if (choice.Type == UpgradeType.Points) utility = 45f;
                    else if (choice.Type == UpgradeType.Chance) utility = 30f;
                    else if (choice.Type == UpgradeType.Coins) utility = 25f;
                    else if (choice.Type == UpgradeType.Jackpot) utility = 35f;
                    break;
            }

            // Prefer upgrades that materially improve the relevant quantity per coin spent.
            choice.Utility = utility * Math.Max(0.05f, choice.Efficiency);
            choices[i] = choice;
        }

        return choices
            .OrderByDescending(c => c.Utility)
            .ThenBy(c => c.Cost)
            .First();
    }

    private static UpgradeChoice BuildChoice(BalanceConfig config, RunState state, UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Points:
            {
                int cost = Cost(config.pointsUpgradeInitialCost, config.pointsUpgradeCostMultiplier, state.PointsUpgradeLevel);
                float efficiency = config.pointsUpgradeValue / (float)Math.Max(1, cost);
                return new UpgradeChoice { Type = type, Cost = cost, Efficiency = efficiency, Valid = cost >= 0 };
            }
            case UpgradeType.Coins:
            {
                int cost = Cost(config.coinsUpgradeInitialCost, config.coinsUpgradeCostMultiplier, state.CoinsUpgradeLevel);
                float efficiency = config.coinsUpgradeValue / (float)Math.Max(1, cost);
                return new UpgradeChoice { Type = type, Cost = cost, Efficiency = efficiency, Valid = cost >= 0 };
            }
            case UpgradeType.Chance:
            {
                if (state.CoinChance >= config.maxCoinChance - 0.000001f)
                    return default;
                int cost = Cost(config.chanceUpgradeInitialCost, config.chanceUpgradeCostMultiplier, state.ChanceUpgradeLevel);
                float actualIncrease = Math.Min(config.chanceUpgradeValue, config.maxCoinChance - state.CoinChance);
                float efficiency = actualIncrease * Math.Max(1, state.CoinsPerDrop) / Math.Max(1, cost);
                return new UpgradeChoice { Type = type, Cost = cost, Efficiency = efficiency, Valid = cost >= 0 };
            }
            case UpgradeType.Jackpot:
            {
                int cost = Cost(config.jackpotUpgradeInitialCost, config.jackpotUpgradeCostMultiplier, state.JackpotUpgradeLevel);
                // Approximate long-run expected coin gain per click from jackpot.
                float currentExpected = state.JackpotReward * state.JackpotIncrement;
                float nextIncrement = GetJackpotIncrement(config, state.JackpotUpgradeLevel + 1);
                int nextReward = Mathf.Max(1, Mathf.RoundToInt(state.JackpotReward * config.jackpotRewardMultiplier));
                float nextExpected = nextReward * nextIncrement;
                float gain = Math.Max(0.0001f, nextExpected - currentExpected);
                float efficiency = gain / Math.Max(1, cost);
                return new UpgradeChoice { Type = type, Cost = cost, Efficiency = efficiency, Valid = cost >= 0 };
            }
            default:
                return default;
        }
    }

    private static void ApplyUpgrade(BalanceConfig config, RunState state, UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Points:
                state.PointsUpgradeLevel++;
                state.PointsPerClick += config.pointsUpgradeValue;
                break;
            case UpgradeType.Coins:
                state.CoinsUpgradeLevel++;
                state.CoinsPerDrop += config.coinsUpgradeValue;
                break;
            case UpgradeType.Chance:
                state.ChanceUpgradeLevel++;
                state.CoinChance = Math.Min(config.maxCoinChance, state.CoinChance + config.chanceUpgradeValue);
                break;
            case UpgradeType.Jackpot:
                state.JackpotUpgradeLevel++;
                state.JackpotReward = Mathf.Max(state.JackpotReward + 1, Mathf.RoundToInt(state.JackpotReward * config.jackpotRewardMultiplier));
                state.JackpotIncrement = GetJackpotIncrement(config, state.JackpotUpgradeLevel);
                break;
        }
    }

    private static float GetJackpotIncrement(BalanceConfig config, int jackpotUpgradeLevel)
    {
        if (jackpotUpgradeLevel <= 0) return config.startJackpotIncrement;
        if (config.jackpotIncrements == null || config.jackpotIncrements.Length == 0)
            return config.startJackpotIncrement;
        int index = Mathf.Clamp(jackpotUpgradeLevel - 1, 0, config.jackpotIncrements.Length - 1);
        return config.jackpotIncrements[index];
    }

    private static int Cost(int initial, float multiplier, int level)
    {
        if (initial <= 0) return 0;
        double cost = initial * Math.Pow(multiplier, level);
        return Mathf.Max(initial, Mathf.RoundToInt((float)Math.Min(int.MaxValue, cost)));
    }

    private static float Next01(System.Random rng) => (float)rng.NextDouble();

    private static StageSummary BuildStageSummary(List<StageRunData> runs, int stageIndex)
    {
        var clicks = runs.Select(r => (float)r.Clicks).ToArray();
        var coins = runs.Select(r => (float)r.CoinsAfterStage).ToArray();
        var ppc = runs.Select(r => (float)r.PointsPerClick).ToArray();
        var chance = runs.Select(r => r.CoinChance).ToArray();
        var coinsPerDrop = runs.Select(r => (float)r.CoinsPerDrop).ToArray();
        var jackpotReward = runs.Select(r => (float)r.JackpotReward).ToArray();
        var jackpotIncrement = runs.Select(r => r.JackpotIncrement).ToArray();

        var level = stageIndex / 3 + 1;
        var stage = stageIndex % 3 + 1;

        var mostCommonUpgrade = runs
            .GroupBy(r => new { r.PointsUpgradeLevel, r.CoinsUpgradeLevel, r.ChanceUpgradeLevel, r.JackpotUpgradeLevel })
            .OrderByDescending(g => g.Count())
            .First().Key;

        return new StageSummary
        {
            Level = level,
            Stage = stage,
            Goal = runs.First().Goal,
            AverageClicks = Mean(clicks),
            MedianClicks = Percentile(clicks, 0.50f),
            P10Clicks = Percentile(clicks, 0.10f),
            P90Clicks = Percentile(clicks, 0.90f),
            AverageCoinsAfterStage = Mean(coins),
            MedianCoinsAfterStage = Percentile(coins, 0.50f),
            AveragePointsPerClick = Mean(ppc),
            AverageCoinChance = Mean(chance),
            AverageCoinsPerDrop = Mean(coinsPerDrop),
            AverageJackpotReward = Mean(jackpotReward),
            AverageJackpotIncrement = Mean(jackpotIncrement),
            AverageUpgradeSummary = $"P{mostCommonUpgrade.PointsUpgradeLevel} / C{mostCommonUpgrade.CoinsUpgradeLevel} / % {mostCommonUpgrade.ChanceUpgradeLevel} / J{mostCommonUpgrade.JackpotUpgradeLevel}"
        };
    }

    private static float Mean(float[] values) => values.Length == 0 ? 0f : values.Average();

    private static float Percentile(float[] values, float percentile)
    {
        if (values == null || values.Length == 0) return 0f;
        Array.Sort(values);
        float position = (values.Length - 1) * percentile;
        int lower = Mathf.FloorToInt(position);
        int upper = Mathf.CeilToInt(position);
        if (lower == upper) return values[lower];
        float t = position - lower;
        return Mathf.Lerp(values[lower], values[upper], t);
    }

    private enum UpgradeType
    {
        Points,
        Coins,
        Chance,
        Jackpot
    }

    private struct UpgradeChoice
    {
        public UpgradeType Type;
        public int Cost;
        public float Efficiency;
        public float Utility;
        public bool Valid;
    }

    private sealed class StageRunData
    {
        public int Clicks;
        public long CoinsAfterStage;
        public int PointsPerClick;
        public float CoinChance;
        public int CoinsPerDrop;
        public int JackpotReward;
        public float JackpotIncrement;
        public int PointsUpgradeLevel;
        public int CoinsUpgradeLevel;
        public int ChanceUpgradeLevel;
        public int JackpotUpgradeLevel;
        public int PurchasesThisStage;
        public int Goal;
        public bool ReachedGoal;
    }
}

[Serializable]
public class SimulationReport
{
    public List<StrategyReport> Strategies = new List<StrategyReport>();
}

[Serializable]
public class StrategyReport
{
    public string StrategyName;
    public List<StageSummary> Stages = new List<StageSummary>();
    public float AverageFinalCoins;
    public float AverageTotalClicks;
}

[Serializable]
public class StageSummary
{
    public int Level;
    public int Stage;
    public int Goal;
    public float AverageClicks;
    public float MedianClicks;
    public float P10Clicks;
    public float P90Clicks;
    public float AverageCoinsAfterStage;
    public float MedianCoinsAfterStage;
    public float AveragePointsPerClick;
    public float AverageCoinChance;
    public float AverageCoinsPerDrop;
    public float AverageJackpotReward;
    public float AverageJackpotIncrement;
    public string AverageUpgradeSummary;
}

internal static class ListExtensions
{
    public static void AddOrEnsure<T>(this List<List<T>> list, int index, List<T> value)
    {
        while (list.Count <= index)
            list.Add(new List<T>());
    }
}
#endif
