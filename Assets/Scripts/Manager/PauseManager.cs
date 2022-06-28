using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public static bool isGamePaused = false;
    public static bool isSettings = false;
    [SerializeField] GameObject pauseMenuUI;

    [SerializeField] GameObject pauseOption;
    void Update()
    {
        this.PlayerPause();
    }

    void PlayerPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isSettings){

            if(isGamePaused){
            
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void OnClickMenuOption()
    {
        pauseMenuUI.SetActive(false);
        pauseOption.SetActive(true);
        isSettings = true;
    }

    public void OnClickBack()
    {
        pauseOption.SetActive(false);
        pauseMenuUI.SetActive(true);
        isSettings = false;
    }
}
