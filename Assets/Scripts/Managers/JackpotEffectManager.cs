using UnityEngine;

public class JackpotEffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _jackpotDropEffect;
    [SerializeField] private ProgressBarManager _progressBarManager;
    private void Start()
    {
        _progressBarManager.OnBarFilled += _progressBarManager_OnBarFilled;
    }

    private void _progressBarManager_OnBarFilled()
    {
        PlayJackpotEffect();
    }

    private void OnDestroy()
    {
        _progressBarManager.OnBarFilled -= _progressBarManager_OnBarFilled;
    }

    private void PlayJackpotEffect()
    {
        _jackpotDropEffect.Play();
    }
}
