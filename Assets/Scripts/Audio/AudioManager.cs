using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Fields
    // SINGLETON
    public static AudioManager Instance { get; private set; }


    [Header("Audio Mixer")]

    [Tooltip("The Master Audio mixer (Find it in the Project window).")]
    public AudioMixer masterAudioMixer;

    [Tooltip("The EXACT name of the Master bus in the audio mixer.")]
    public string masterBusName = "masterVol";

    [Tooltip("The EXACT name of the Sound bus in the audio mixer.")]
    public string soundBusName = "soundVol";

    [Tooltip("The EXACT name of the Music bus in the audio mixer.")]
    public string musicBusName = "musicVol";

    [SerializeField, Tooltip("")]
    private AnimationCurve volumeVsDecibels;


    [Header("Player Pref Names (NOTE: Changing these names means the user loses their settings.")]

    [Tooltip("The name of the PlayerPref for the Master Volume setting.")]
    public string masterVolume_PrefName = "MasterVolume_Pref";

    [Tooltip("The name of the PlayerPref for the Sound Volume setting.")]
    public string soundVolume_PrefName = "SoundVolume_Pref";

    [Tooltip("The name of the PlayerPref for the Music Volume setting.")]
    public string musicVolume_PrefName = "MusicVolume_Pref";


    [Header("Sounds for use")]

    [Tooltip("The sound for when a characcter gets hurt.")]
    public AudioClip hurtSound;
    #endregion Fields


    #region Unity Methods
    // Called immediately after being instantiated.
    private void Awake()
    {
        // SINGLETON
        // If there isn't already an instance of this class,
        if (Instance == null)
        {
            // then this will the the only allows instance.
            Instance = this;
        }
        // Else, a new GameManager is trying to instantiate, but there can only be one.
        else
        {
            // Destroy this GameManager and end the Awake method.
            Destroy(this);
            return;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        // Ensure that when the MainMenu and/or Level 1 starts, the preferences are applied.
        ApplyPreferencesToGame();
    }

    // Update is called once per frame
    public void Update()
    {

    }
    #endregion Unity Methods


    #region Dev Methods
    // Changes the volumes on all three audio buses. Provide the volume in decibels.
    public void ChangeBusVolumes(float masterVol, float soundVol, float musicVol)
    {
        masterAudioMixer.SetFloat
            (masterBusName, masterVol);
        masterAudioMixer.SetFloat
            (soundBusName, soundVol);
        masterAudioMixer.SetFloat
            (musicBusName, musicVol);
    }

    // Converts a volume (float) into decibels using the AudioManager's animation curve.
    public float VolumeToDecibel(float volume)
    {
        return volumeVsDecibels.Evaluate(volume);
    }

    // Apply the already-saved preferences to the game (for audio).
    private void ApplyPreferencesToGame()
    {
        ChangeBusVolumes
            (VolumeToDecibel(PlayerPrefs.GetFloat(masterVolume_PrefName)),
            VolumeToDecibel(PlayerPrefs.GetFloat(soundVolume_PrefName)),
            VolumeToDecibel(PlayerPrefs.GetFloat(musicVolume_PrefName)));
    }
    #endregion Dev Methods
}
