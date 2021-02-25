using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    #region Fields
    [Header("Health")]

    [SerializeField, Tooltip("The object's maximum health")]
    private float maxHealth = 100;

    [SerializeField, Tooltip("The object's initial health. Object cannot start higher than maxHealth")]
    private float initialHealth = 100;

    // The object's current health. Initialized based on max and initial healths.
    private float currentHealth;


    [Header("Events")]

    [SerializeField, Tooltip("Raised every time the object is Damaged")]
    private UnityEvent onDamage;
    [SerializeField, Tooltip("Raised every time the object is healed")]
    private UnityEvent onHeal;
    [SerializeField, Tooltip("Raised once when the object's health reaches 0")]
    private UnityEvent onDie;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the object's health.
        InitializeHealth();
    }
    #endregion Unity Methods


    #region Dev Methods
    // Called once during Start to initialize the object's health.
    private void InitializeHealth()
    {
        // Get the higher of the initialHealth and 1.0f.
        float value = Mathf.Max(initialHealth, 1.0f);

        // Set the currentHealth to the value.
        SetCurrentHealth(value);
    }

    // Called to damage the object by the specified amount.
    public void Damage(float damage)
    {
        // Damage must be positive.
        damage = Mathf.Max(damage, 0.0f);

        // Determine new health, ensuring currentHealth stays between 0.0f and maxHealth.
        float newHealth = Mathf.Clamp((currentHealth - damage), 0.0f, maxHealth);
        
        // Apply the new health.
        SetCurrentHealth(newHealth);

        // Send a message to all other components of this gameObject to call OnDamage, if that method exists.
        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);

        // Invoke OnDamage for this component.
        onDamage.Invoke();

        // If the object is dead,
        if (currentHealth == 0.0f)
        {
            // then send the OnDie message to other components.
            SendMessage("OnDie", SendMessageOptions.DontRequireReceiver);

            // Invoke OnDeath for this component.
            onDie.Invoke();
        }
    }

    // Convenience method to kill the object outright. Calls Damage() with maxHealth as the damage.
    public void Kill()
    {
        // Call Damage() with maxHealth as the damage to ensure death.
        Damage(maxHealth);
    }


    // Called to heal the object's health by the specified amount.
    public void Heal(float healing)
    {
        // healing must be positive.
        healing = Mathf.Max(healing, 0.0f);

        // Determine new health, ensuring currentHealth stays between 0.0f and maxHealth.
        float newHealth = Mathf.Clamp((currentHealth + healing), 0.0f, maxHealth);

        // Apply the new health.
        SetCurrentHealth(newHealth);

        // Send a message to all other components of this gameObject to call OnHeal, if that method exists.
        SendMessage("OnHeal", SendMessageOptions.DontRequireReceiver);

        // Invoke OnHeal for this component.
        onHeal.Invoke();
    }


        #region Getters
    // Get the currentHealth.
    public float GetHealth()
    {
        return currentHealth;
    }

    // Get the object's current health percentage.
    public float GetHealthPercentage()
    {
        return (currentHealth / maxHealth);
    }
    #endregion Getters


        #region Setters
    // Sets the currentHealth.
    private void SetCurrentHealth(float newHealth)
    {
        // Ensure currentHealth does not go above maxHealth.
        currentHealth = Mathf.Min(newHealth, maxHealth);
    }
        #endregion Setters


    #endregion Dev Methods
}
