using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource;
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        clickHandler.OnClick += ClickHandler_OnClick;
    }

    private void ClickHandler_OnClick()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
