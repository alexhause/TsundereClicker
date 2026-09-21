using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private RectTransform tooltipRectTransform;
    [SerializeField] private TextMeshProUGUI tooltipText;
    private void Awake()
    {
        SetText("Подсказка номер 10");
    }

    private void SetText(string tooltipTxt)
    {
        tooltipText.SetText(tooltipTxt);
        tooltipText.ForceMeshUpdate();

        Vector2 textSize = tooltipText.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);
        tooltipRectTransform.sizeDelta = textSize + paddingSize;

    }
}
