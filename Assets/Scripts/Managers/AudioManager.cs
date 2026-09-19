using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    #region Singleton
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
    #endregion

    public void PlaySFX(AudioClip sfx)
    {
        sfxSource.PlayOneShot(sfx);
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }
}
