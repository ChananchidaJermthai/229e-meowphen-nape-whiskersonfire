using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("InGame");
    }
}
