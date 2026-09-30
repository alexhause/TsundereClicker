using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootstrap : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DelayedLoad("MainMenu"));
    }

    // Корутина для ожидания
    IEnumerator DelayedLoad(string sceneName)
    {
        // Ждем 3 секунды (можно указать любое число)
        yield return new WaitForSeconds(3f);

        // Переключаем сцену
        SceneManager.LoadScene(sceneName);
    }
}
