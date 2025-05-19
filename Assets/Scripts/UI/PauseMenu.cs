using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject generalPauseMenu;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject firstScreen;
    [SerializeField] GameObject HUD;

    private bool isPaused = false;

    private void Update()
    {
        HandlerPause();
    }
    private void HandlerPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                isPaused = false;
            }
            else
            {
                PauseGame();
                isPaused = true;
            }
        }

    }
    public void PauseGame()
    {
        generalPauseMenu.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        generalPauseMenu.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Back()
    {
        optionsScreen.SetActive(false);
        firstScreen.SetActive(true);
    }

    public void Options()
    {
        optionsScreen.SetActive(true);
        firstScreen.SetActive(false);

    }

    public void Volumen()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


