using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject levelPanel;

    public string firstLevelName = "Level 1";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;
        ShowMainMenu();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevelName);

    }
    public void ShowLevelSelect()
    {
        mainPanel.SetActive(false);
        levelPanel.SetActive(true);

    }

    public void ShowMainMenu()
    {
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(levelName);
        Debug.Log("Loaded" + levelName);
    }
    
    public void QuitGame()
    {
       Application.Quit();
    }

}
