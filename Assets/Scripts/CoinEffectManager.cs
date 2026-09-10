using UnityEngine;

public class CoinEffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem coinDropEffect;

    private void Start()
    {
        CoinManager.Instance.OnCoinDrop += CoinManager_OnCoinDrop;
    }

    private void OnDestroy()
    {
        CoinManager.Instance.OnCoinDrop -= CoinManager_OnCoinDrop;
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