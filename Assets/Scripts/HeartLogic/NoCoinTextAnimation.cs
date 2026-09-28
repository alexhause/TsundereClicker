using UnityEngine;
using TMPro;
using DG.Tweening;

public class NoCoinTextAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinCountText;

    private Color _defaultColor;
    private void Awake()
    {
        _defaultColor = _coinCountText.color;
    }

    private void Start()
    {
        UpgradeManager.Instance.OnNoCoinForUpgrade += PlayAnimation;
    }

    private void PlayAnimation()
    {
        _coinCountText.transform.DOKill();
        _coinCountText.DOKill();
        _coinCountText.transform.localScale = new Vector3(1f, 1f, 1f);

        _coinCountText.transform.DOShakeScale(
            duration: 0.1f);

        Sequence colorSequence = DOTween.Sequence();

        colorSequence.Append(
            _coinCountText.DOColor(Color.red, 0.1f));

        colorSequence.Append(
           _coinCountText.DOColor(_defaultColor, 0.2f)
        );
    }
}
