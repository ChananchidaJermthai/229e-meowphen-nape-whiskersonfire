using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("💡 QuitGame() called!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // หยุด Play Mode ใน Unity Editor
#else
        Application.Quit();  // ปิดเกมเมื่อรันจริง
#endif
    }
}