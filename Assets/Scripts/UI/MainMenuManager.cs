using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Fields
    [Header("Events")]

    [Tooltip("Invoked when the game is started from the MainMenu.")]
    public UnityEvent onStart;

    [Tooltip("Invoked when the game is quit from the MainMenu.")]
    public UnityEvent onQuit;


    [Header("Scene Info")]

    [SerializeField, Tooltip("The EXACT name of the scene to load when the game starts.")]
    private string gameStartSceneName = "Level1";


    [Header("Object & Component References")]

    [SerializeField, Tooltip("The GameObject holding the Settings Screen UI.")]
    private GameObject settingsScreen;
    #endregion Fields


    #region Unity Methods
    // Called immediately after being instantiated.
    private void Awake()
    {
        // If any of these are null, try to set them up.
    }

    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {

    }
    #endregion Unity Methods


    #region Dev Methods
    // Starts the game.
    public void StartGame()
    {
        // Loads the appripriate scene.
        SceneManager.LoadScene(gameStartSceneName);
    }

    // Ends the program.
    public void EndProgram()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Activates the Settings Menu, which deactivates the Main Menu
    // until the Player finished with the settings.
    private void ActivateSettingsScreen()
    {
        settingsScreen.SetActive(true);
        gameObject.SetActive(false);
    }
    #endregion Dev Methods


    #region UI Callback Methods
    // Main Menu -->

    // Called when the Player clicks on the Start button on the Main Menu.
    public void MainMenu_OnStartButtonClicked()
    {
        // Invoke the OnStart event.
        onStart.Invoke();
    }

    // Called when the Player clicks on the Quit button on the Main Menu.
    public void MainMenu_OnQuitButtonClicked()
    {
        // Invoke the OnQuit event.
        onQuit.Invoke();
    }

    // Called when the Player clicks on the Settings button on the MainMenu.
    public void MainMenu_OnSettingsButtonClicked()
    {
        // Open the settings menu.
        ActivateSettingsScreen();
    }
    #endregion UI Callback Methods
}
