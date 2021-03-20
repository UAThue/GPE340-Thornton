using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    #region Fields
    [Header("Health Bars")]

    [SerializeField, Tooltip("Prefab for enemy health bars.")]
    private HealthBar enemyHealthBarPrefab;

    [SerializeField, Tooltip("The health bar (script) on the main canvas to be assigned to the Player.")]
    private HealthBar playerHealthBar;


    [Header("Weapon Icon")]

    [SerializeField, Tooltip("The Image used to show the weapon icon sprite for the Player's HUD.")]
    private Image weaponIconImage;


    [Header("Other Object & Component References")]

    [SerializeField, Tooltip("The TextMeshProUGUI of the number of lives remaining on the Player's HUD.")]
    private TextMeshProUGUI livesRemaingText;
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
    // Called on Awake by PlayerData to register the primary health bar to them.
    public void RegisterPlayer(Health player)
    {
        // Set the primary health bar's target to the player.
        playerHealthBar.SetTarget(player);

        // Add listeners to the player's health to update the primary health bar when their health changes.
        player.onDamage.AddListener(playerHealthBar.UpdateHealthBar);
        player.onHeal.AddListener(playerHealthBar.UpdateHealthBar);

        // Force an update for the health bar immediately.
        playerHealthBar.UpdateHealthBar();
    }

    // Called on Awake by EnemyData to set up that healthBar.
    public void RegisterEnemy(Health enemy, HealthBar healthBar)
    {
        // Add listeners to this enemy's health to update their health bar when their health changes.
        enemy.onDamage.AddListener(healthBar.UpdateHealthBar);
        enemy.onHeal.AddListener(healthBar.UpdateHealthBar);
        // Add one more to destroy the healthBar when the enemy dies.
        enemy.onDie.AddListener(healthBar.RemoveHealthBar);
    }

    // Called when a player equips a weapon to update the weaponIconImage.
    public void UpdateWeaponIcon(Sprite newIcon)
    {
        weaponIconImage.sprite = newIcon;
    }

    // Called by the GM when the Player's number of remaining lives changes.
    public void UpdateLivesRemainingText(int numLives)
    {
        livesRemaingText.text = numLives.ToString();
    }
    #endregion Dev Methods
}
