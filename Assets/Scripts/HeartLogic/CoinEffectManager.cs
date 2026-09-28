using Unity.VisualScripting;
using UnityEngine;

public class CoinEffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem coinDropEffect;
    [SerializeField] private CoinManager coinManager;
    private void Start()
    {
        coinManager.OnCoinDrop += CoinManager_OnCoinDrop;
    }

    private void OnDestroy()
    {
        coinManager.OnCoinDrop -= CoinManager_OnCoinDrop;
    }

    private void CoinManager_OnCoinDrop(int obj)
    {
        PlayCoinEffect();
    }

    private void PlayCoinEffect()
    {
        coinDropEffect.Play();
    }
}