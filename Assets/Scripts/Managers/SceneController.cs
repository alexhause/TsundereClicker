using UnityEngine;

public class SceneController : MonoBehaviour
{
    #region Singleton
    public static SceneController Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion


}
