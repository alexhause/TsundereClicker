using UnityEngine;

public class TsundereAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ClickHandler clickHandler;

    private void OnEnable()
    {
        clickHandler.OnClick += PlayClickAnimation;
    }

    private void OnDisable()
    {
        clickHandler.OnClick -= PlayClickAnimation;
    }

    private void PlayClickAnimation()
    {
        animator.SetTrigger("Click");
    }
}
