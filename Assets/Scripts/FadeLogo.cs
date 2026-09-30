using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeLogo : MonoBehaviour
{
    // Ссылка на CanvasGroup нашей картинки
    [SerializeField] private CanvasGroup logoCanvasGroup;

    // Длительность проявления в секундах
    [SerializeField] private float fadeDuration = 2f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float counter = 0f;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            logoCanvasGroup.alpha = Mathf.Lerp(0f, 1f, counter / fadeDuration);
            yield return null; 
        }

        logoCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(1f);
    }
}
