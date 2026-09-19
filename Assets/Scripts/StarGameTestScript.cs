using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarGameTestScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
