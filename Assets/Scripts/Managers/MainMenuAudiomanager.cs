using Unity.VisualScripting;
using UnityEngine;

public class MainMenuAudiomanager : MonoBehaviour
{
    [SerializeField] AudioClip mainMenuMusic;
    private void Start()
    {
        AudioManager.Instance.PlayMusic(mainMenuMusic);
    }

}
