using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
   public void NewGame()
   {
        SaveManager.Instance.DeleteSave();
        SceneManager.LoadScene("GameplayScene");
    }

    public void ContinueGame()
    {
        if (!SaveManager.Instance.HasSave())
        {
            Debug.LogWarning("Сохранение не найдено!");
            return;
        }
        SceneManager.LoadScene("GameplayScene");
    }
}
