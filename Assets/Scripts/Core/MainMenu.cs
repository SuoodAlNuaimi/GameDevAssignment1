using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayHardGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayEasyGame()
    {
        SceneManager.LoadScene("EasyMode");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game ....");
        Application.Quit();
    }
}
