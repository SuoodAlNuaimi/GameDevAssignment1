using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private InputAction interactAction;
    public GameObject pauseMenuUI;
    


    void Start()
    {
        interactAction = UserInput.instance.controls.Player.Menu;
        interactAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

   public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void quitGame()
    {
        Debug.Log("Quitting game ....");
        Application.Quit();
    }
    
}
