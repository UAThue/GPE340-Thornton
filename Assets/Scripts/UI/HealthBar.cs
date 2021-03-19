using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Fields
    [Header("Object & Component References")]

    [SerializeField, Tooltip("The Health (on the Player or Enemy) that this HealthBar represents.")]
    private Health target;

    [SerializeField, Tooltip("The slider of this health bar.")]
    private Slider slider;


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
        // Update the health bar.
        UpdateHealthBar();
    }

    // Update is called once per frame
    public void Update()
    {

    }
    #endregion Unity Methods


    #region Dev Methods
    // Sets this health bar's target.
    public void SetTarget(Health newTarget)
    {
        target = newTarget;
    }

    // Update the health bar's percentage.
    public void UpdateHealthBar()
    {
        slider.value = target.GetHealthPercentage();
    }

    /* Destroys the healthBar. This is to prevent the healthBar hanging around when the enemy
     * dies and goes Ragdoll. If not destroyed, it looks odd and blocks view of the dropped loot. */
    public void RemoveHealthBar()
    {
        Destroy(gameObject);
    }
    #endregion Dev Methods
}
