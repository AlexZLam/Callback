
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public enum GameState
    {
        Playing,
        Paused,
        Won,
        Restarting
    }

    [Header("Game State")]
    public GameState currentState = GameState.Playing;

    [Header("Level Settings")]
    public string nextLevelName;
    public string mainMenuName = "Main Menu";
    public string gameCompleteSceneName = "GameComplete";

    [Header("UI Panels")]
    public GameObject levelCompletePanel;
    public GameObject pausePanel;

    [Header("Gameplay References")]
    public PlayerMovement playerMovement;
    public Callback_Manager callbackManager;

    private bool restartRequested = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    private void Start()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        SetGameplayEnabled(true);
    }

    private void Update()
    {
        
        if(restartRequested)
        {
            restartRequested = false;
            RestartLevel();
            return;
        }
      

        if (currentState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartLevel();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        else if (currentState == GameState.Paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
    }

    // Called when the player reaches the level goal.
    public void WinLevel()
    {
        if (currentState != GameState.Playing)
            return;

        currentState = GameState.Won;

        SetGameplayEnabled(false);

        Time.timeScale = 0f;

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }
        else
        {
            NextLevel();
        }
    }

    // Called when the player touches a hazard.
    public void Die()
    {
        if (currentState != GameState.Playing)
            return;

        RestartLevel();
    }

    // Reloads the current level and resets all objects.
    public void RestartLevel()
    {
        if (currentState == GameState.Restarting)
            return;

        currentState = GameState.Restarting;

        SetGameplayEnabled(false);

        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }

    // Loads the next level or the game completion scene.
    public void NextLevel()
    {
        if (currentState != GameState.Won)
            return;

        if (!string.IsNullOrEmpty(nextLevelName))
        {
            LoadScene(nextLevelName);
        }
        else
        {
            LoadScene(gameCompleteSceneName);
        }
    }

    // Pauses gameplay and displays the pause menu.
    public void PauseGame()
    {
        if (currentState != GameState.Playing)
            return;

        currentState = GameState.Paused;

        SetGameplayEnabled(false);

        Time.timeScale = 0f;

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    // Resumes gameplay and hides the pause menu.
    public void ResumeGame()
    {
        if (currentState != GameState.Paused)
            return;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        currentState = GameState.Playing;

        Time.timeScale = 1f;

        SetGameplayEnabled(true);
    }

    // Returns to the main menu.
    public void ReturnToMenu()
    {
        LoadScene(mainMenuName);
    }

    // Enables or disables player and callback input.
    private void SetGameplayEnabled(bool enabled)
    {
        if (playerMovement != null)
            playerMovement.enabled = enabled;

        if (callbackManager != null)
            callbackManager.enabled = enabled;
    }

    // Returns whether gameplay is currently active.
    public bool IsPlaying()
    {
        return currentState == GameState.Playing;
    }

    // Loads a scene after verifying that it is available.
    private void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("No scene name was provided!");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError("Scene could not be loaded: " + sceneName);
            return;
        }

        currentState = GameState.Restarting;

        SetGameplayEnabled(false);

        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void RequestRestart()
    {
        restartRequested = true;
    }
}
