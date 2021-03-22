using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class SettingsWindow : MonoBehaviour
{
    #region Fields
    [Header("Volume Area")]

    [SerializeField, Tooltip("The Slider for the Master Volume setting.")]
    private Slider masterVolumeSlider;

    [SerializeField, Tooltip("The Slider for the Sound Volume setting.")]
    private Slider soundVolumeSlider;

    [SerializeField, Tooltip("The Slider for the Music Volume setting.")]
    private Slider musicVolumeSlider;


    [Header("Graphics Area")]

    [SerializeField, Tooltip("The TMP Dropdown for the Resolution setting.")]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField, Tooltip("The Toggle for the Fullscreen setting.")]
    private Toggle fullScreenToggle;

    [SerializeField, Tooltip("The TMP Dropdown for the Video Quality setting.")]
    private TMP_Dropdown videoQualityDropdown;


    [Header("Buttons")]

    [SerializeField, Tooltip("The Button script on the Apply button.")]
    private Button applyButton;


    [Header("Player Pref Names (NOTE: Changing these names means the user loses their settings.")]

    [SerializeField, Tooltip("The name of the PlayerPref for the Master Volume setting.")]
    private string masterVolume_PrefName = "MasterVolume_Pref";

    [SerializeField, Tooltip("The name of the PlayerPref for the Sound Volume setting.")]
    private string soundVolume_PrefName = "SoundVolume_Pref";

    [SerializeField, Tooltip("The name of the PlayerPref for the Music Volume setting.")]
    private string musicVolume_PrefName = "MusicVolume_Pref";


    [Header("Other Object & Component References")]

    [SerializeField, Tooltip("The gameObject holding the UI menu that should be opened when" +
        " the settings close.")]
    private GameObject previousMenu;
    #endregion Fields


    #region Unity Methods
    // Called immediately after being instantiated.
    private void Awake()
    {
        // Build the appropriate setting options for the resolution and video quality settings.
        BuildResolutions();
        BuildVideoQualities();
    }

    // Called every time the gameObject is enabled (SetActive to false, then true, also same as Start).
    private void OnEnable()
    {
        // Apply the currently saved preferences to the settings.
        ApplyAllPreferences();
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
    // Builds the Resolutions Dropdown menu with the appropriate options.
    private void BuildResolutions()
    {
        // Clear the resolution settings dropdown of all current options.
        resolutionDropdown.ClearOptions();
        // Create a list to hold the options we will get from Screen.
        List<string> resolutions = new List<string>();

        // Iterate through the resolution options from Screen.
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            // Add each resolution to the list, formated like this:
            // 1234 x 987
            resolutions.Add
                (string.Format("{0} x {1}", Screen.resolutions[i].width, Screen.resolutions[i].height));
        }

        // Finish by actually adding them to the dropdown as options.
        resolutionDropdown.AddOptions(resolutions);
    }

    // Builds the Video Quality Dropdown menu with the appropriate options.
    private void BuildVideoQualities()
    {
        // Clear the video quality settings dropdown of all current options.
        videoQualityDropdown.ClearOptions();
        // Add the appropriate settings.
        videoQualityDropdown.AddOptions(QualitySettings.names.ToList());
    }

    // Update all current settings to the values saved in PlayerPrefs and via Unity.
    private void ApplyAllPreferences()
    {
        // Apply Player Prefs to the volume sliders.
        masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolume_PrefName, masterVolumeSlider.maxValue);
        soundVolumeSlider.value = PlayerPrefs.GetFloat(soundVolume_PrefName, soundVolumeSlider.maxValue);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(musicVolume_PrefName, soundVolumeSlider.maxValue);
        // Apply Unity's auto-saved preferences to fullscreen and video quality.
        fullScreenToggle.isOn = Screen.fullScreen;
        videoQualityDropdown.value = QualitySettings.GetQualityLevel();
        // Resolution settings are saved & applied automatically.

        // Apply those changes.
        Apply(false);
    }

    // Applies the current setting values. Can also save the values to preferences.
    private void Apply(bool saveToPrefs)
    {
        // No changes have been made anymore, so ensure the Apply button is not interactable.
        applyButton.interactable = false;

        // TODO: Apply changes once sound is built.

        if (saveToPrefs)
        {
            // Save the current values as Player Preferences (the ones that Unity doesn't save automatically).
            SavePrefs();
        }
    }

    // Save the current values as Player Preferences (the ones that Unity doesn't save automatically).
    private void SavePrefs()
    {
        PlayerPrefs.SetFloat(masterVolume_PrefName, masterVolumeSlider.value);
        PlayerPrefs.SetFloat(soundVolume_PrefName, soundVolumeSlider.value);
        PlayerPrefs.SetFloat(musicVolume_PrefName, musicVolumeSlider.value);
    }

    // Closes the Settings menu, opening up the appropriate previous menu.
    private void CloseSettingsMenu()
    {
        previousMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    #endregion Dev Methods

    #region UI Callback Methods
    // Called when the Player clicks on the Back button of the Settings Menu.
    public void SettingsMenu_OnBackButtonClicked()
    {
        // Close the Settings menu, going back to the previous menu.
        CloseSettingsMenu();
    }

    // Called when the Player clicks on the Back button of the Settings Menu.
    public void SettingsMenu_OnApplyButtonClicked()
    {
        // Apply.
        Apply(true);
    }
    #endregion UI Callback Methods
}
