using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
