using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    [Header("Object & Component References")]

    [SerializeField, Tooltip("References the player's data.")]
    private PlayerData m_Player;
    #endregion Fields


    #region Unity Methods
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
    private void FindPlayer()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }


        #region Getters
    public PlayerData GetPlayer()
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
