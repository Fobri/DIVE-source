using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject newGamePanel;


    private void Start()
    {
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        settingsPanel.SetActive(false);
        newGamePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void LoadSettingsMenu()
    {
        settingsPanel.SetActive(true);
        newGamePanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }
    public void LoadNewGameMenu()
    {
        settingsPanel.SetActive(false);
        newGamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        SceneManager.LoadScene("Surface");
    }
}
