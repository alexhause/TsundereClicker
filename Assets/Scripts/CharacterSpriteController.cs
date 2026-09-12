using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    private SpriteRenderer characterSpriteRenderer;

    private void Awake()
    {
        characterSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        levelManager.OnStageChanged += LevelManager_OnStageChanged;
        levelManager.OnNewLevelStart += LevelManager_OnNewLevelStart;
    }

    private void OnDestroy()
    {
        levelManager.OnStageChanged -= LevelManager_OnStageChanged;
        levelManager.OnNewLevelStart -= LevelManager_OnNewLevelStart;
    }

    private void LevelManager_OnStageChanged(StageData nextStage)
    {
        characterSpriteRenderer.sprite = nextStage.characterSprite;
    }

    private void LevelManager_OnNewLevelStart(LevelData newLevel)
    {
        characterSpriteRenderer.sprite = newLevel.stages[0].characterSprite;
    }
}
