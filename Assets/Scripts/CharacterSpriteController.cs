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
    }

    private void OnDestroy()
    {
        levelManager.OnStageChanged -= LevelManager_OnStageChanged;
    }

    private void LevelManager_OnStageChanged(StageData nextStage)
    {
        characterSpriteRenderer.sprite = nextStage.characterSprites;
    }
}
