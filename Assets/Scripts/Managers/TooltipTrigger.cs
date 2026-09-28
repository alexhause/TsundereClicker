using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string tooltipText;
    public void OnPointerEnter(PointerEventData eventData)
    {       
        TooltipManager.Instance.Show();
        TooltipManager.Instance.SetText(tooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}
