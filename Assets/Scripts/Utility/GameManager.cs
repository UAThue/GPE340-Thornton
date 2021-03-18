using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    [Header("The Player")]

    [SerializeField, Tooltip("The PREFAB for the Player.")]
    private PlayerData playerPrefab;

    private static PlayerData m_Player;


    [Header("The HUD")]

    [Tooltip("The Health Bar slider.")]
    public Slider healthBar;

    [Tooltip("The Stamina Bar slider.")]
    public Slider staminaBar;


    [Header("Spawning")]

    [SerializeField, Tooltip("Where the Player should start this level.")]
    private Transform playerStartLocation;
    
    [SerializeField, Tooltip("How long between the Player dieing and being respawned.")]
    private float playerRespawnDelay = 4.0f;


    [Header("Lives")]

    [SerializeField, Tooltip("The number of lives that the Player starts with. Cannot respawn if < 1.")]
    private int initialLives = 2;

    // The actual number of lives left.
    private int livesLeft;


    [Header("Other Object & Component References")]

    [Tooltip("The OverheadCamera script on the main camera that should follow the player.")]
    public OverheadCamera overheadCamera;
    #endregion Fields


    #region Unity Methods
    // Called when instantiated.
    private void Awake()
    {
        // Spawn the player on the start point and save a reference to their data.
        SpawnPlayer();

        // Set initial lives.
        SetLivesLeft(initialLives);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {

    }
    #endregion Unity Methods


    #region Dev Methods
    // Spawn the player.
    private void SpawnPlayer()
    {
        // Spawn the Player.
        m_Player = Instantiate(playerPrefab, playerStartLocation.position, playerStartLocation.rotation).
            GetComponent<PlayerData>();
        // Set up the references needed for the Player.
        m_Player.SetUpReferences();
        // Add a listener for when the Player dies.
        m_Player.health.onDie.AddListener(HandlePlayerDeath);
    }

    // Called when the player dies via OnDie event.
    private void HandlePlayerDeath()
    {
        // Remove this listener from the OnDie event.
        m_Player.health.onDie.RemoveListener(HandlePlayerDeath);

        // If the Player has a life to consume,
        if (livesLeft > 0)
        {
            // then consume a list and invoke the SpawnPlayer method after the appropriate respawn delay.
            SetLivesLeft(livesLeft - 1);
            Invoke("SpawnPlayer", playerRespawnDelay);
        }
        // Else, the Player did not have any more lives left.
        else
        {
            // TODO: Game Over!
            print("Game Over!");
        }
    }


        #region Getters
    public static PlayerData GetPlayer()
    {
        // Return reference to the player.
        return m_Player;
    }

    public int GetLivesLeft()
    {
        return livesLeft;
    }
    #endregion Getters

    #region Setters
    private void SetLivesLeft(int newVal)
    {
        livesLeft = newVal;
    }
    #endregion Setters
    #endregion Dev Methods
}
