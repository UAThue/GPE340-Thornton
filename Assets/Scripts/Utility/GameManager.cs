using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    [Header("The Player")]

    [SerializeField, Tooltip("The PREFAB for the Player.")]
    private PlayerData playerPrefab;

    [SerializeField, Tooltip("Where the Player should start this level.")]
    private Transform playerStartLocation;

    private static PlayerData m_Player;


    [Header("The HUD")]

    [Tooltip("The Health Bar slider.")]
    public Slider healthBar;

    [Tooltip("The Stamina Bar slider.")]
    public Slider staminaBar;


    [Header("Other Object & Component References")]

    [Tooltip("The OverheadCamera script on the main camera that should follow the player.")]
    public OverheadCamera overheadCamera;
    #endregion Fields


    #region Unity Methods
    // Called when instantiated.
    private void Awake()
    {
        // Spawn the player on the start point and save a reference to their data.
        SpawnPlayer(playerStartLocation.position, playerStartLocation.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        // If any of these are null, try to set them up.
        if (m_Player == null)
        {
            FindPlayer();
        }
    }

    // Update is called once per frame
    public void Update()
    {

    }
    #endregion Unity Methods


    #region Dev Methods
    // In the case where we lose reference to the player, try to find the player.
    private static void FindPlayer()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }

    // Spawn the player at the specified location / rotation.
    private void SpawnPlayer(Vector3 position, Quaternion rotation)
    {
        m_Player = Instantiate(playerPrefab, position, rotation);
    }


        #region Getters
    public static PlayerData GetPlayer()
    {
        // If the player is null,
        if (m_Player == null)
        {
            // Find the player.
            FindPlayer();
        }

        // Return reference to the player.
        return m_Player;
    }
        #endregion Getters
    #endregion Dev Methods
}
