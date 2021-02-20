using UnityEngine;
using UnityEngine.UI;

// The CharacterData holds data about the character that can be accessed from other scripts.
// This way, all data specific to this character is in one spot.

public class CharacterData : MonoBehaviour
{
    #region Fields
    [Header("Speeds")]
    // The maximum movement speed of this character.
    [Tooltip("Max movement speed of this character.")]
    public float maxMoveSpeed = 8.0f;

    // The speed at which this character can turn their body.
    [Tooltip("Speed that this character can turn.")]
    public float turnSpeed = 90.0f;

    [Header("Stamina")]
    // The player's maximum stamina.
    [SerializeField] private float maxStamina = 100.0f;

    // The player's curent stamina.
    private float currentStamina;

    // The rate at which stamina drops when sprinting (per second).
    [SerializeField] private float staminaUseRate = 10.0f;

    // The number of seconds that player must wait to start regaini9ng stamina once they stop sprinting.
    [SerializeField] private float staminaRecoveryDelay = 1.0f;

    // The number of seconds since the player stopped sprinting (until staminaRecoverDelay time is reached).
    private float timeStaminaRecoveryDelayed = 0.0f;

    // The amount of stamina to be recovered (per second) when recovering stamina.
    [SerializeField] private float staminaRecoveryRate = 20.0f;

    // Whether or not the character is currently sprinting.
    public bool isSprinting = false;

    // The Stamins bar (slider) on the HUD.
    [SerializeField] private Slider staminaBar;
    #endregion Fields

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        // Initialize currentStamina to equal maxStamina.
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        // If sprinting,
        if (isSprinting)
        {
            // then use stamina.
            UseStamina();
        }

        // Recover stamina (as appropriate).
        RecoverStamina();
    }
    #endregion Unity Methods

    #region Dev Methods
    // Returns bool for whether or not the character can sprint at the moment.
    public bool CanSprint()
    {
        // If there is stamina left,
        if (currentStamina > 0)
        {
            // and if the player is not already sprinting,
            if (!isSprinting)
            {
                // then the player can sprint. Player is now sprinting.
                isSprinting = true;
                // Reset the time since the player started the stamina recovery delay.
                timeStaminaRecoveryDelayed = 0.0f;
            }

            // Player will not be sprinting. Use stamina.
            UseStamina();
            // Return true. The player can sprint.
            return true;
        }
        // Else, they cannot sprint.
        else
        {
            isSprinting = false;
            return false;
        }
    }

    // Called each frame that the character is sprinting to lower the stamina.
    public void UseStamina()
    {
        // Set the current stamina equal to itself myself staminaUseRate / second, minimum of 0.
        currentStamina -= staminaUseRate * Time.deltaTime;

        if (currentStamina < 0)
        {
            currentStamina = 0;
        }

        // Update the stamina bar.
        UpdateStaminaBar();
    }

    // Called every frame. Determines if stamina should be recovered, and does so.
    private void RecoverStamina()
    {
        // If the player is not sprinting, and stamina is not at maximum,
        if (!isSprinting && currentStamina < maxStamina)
        {
            // and if the player's stamina recovery has been delayed enough,
            if (timeStaminaRecoveryDelayed >= staminaRecoveryDelay)
            {
                // then recover stamina, with a max of maxStamina.
                currentStamina += staminaRecoveryRate * Time.deltaTime;

                if (currentStamina > maxStamina)
                {
                    currentStamina = maxStamina;
                }

                // Update the stamina bar.
                UpdateStaminaBar();
            }
            // Else, the player's stamina recovery has not yet been delayed enough.
            else
            {
                // Increase the time since 
                timeStaminaRecoveryDelayed += Time.deltaTime;
            }
        }
    }

    // Updates the stamina bar on the HUD.
    private void UpdateStaminaBar()
    {
        staminaBar.value = currentStamina / maxStamina;
    }

    // Called to get the current stamina.
    public float GetCurrentStamina()
    {
        return currentStamina;
    }
    #endregion Dev Methods
}
