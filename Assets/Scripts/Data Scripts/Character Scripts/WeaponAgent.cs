using UnityEngine;

// WeaponAgent is the parent for both PlayerData and EnemyData, so both can use weapons.

public abstract class WeaponAgent : MonoBehaviour
{
    #region Fields
    [Header("Weapon")]

    [Tooltip("The weapon that this character has equipped.")]
    public Weapon equippedWeapon;

    // TODO: Do I need this?
    //// The current weapon stance this character should be using.
    //private WeaponStance weaponStance = WeaponStance.Unarmed;


    [Header("Animation Assistance")]

    [SerializeField, Tooltip("The name (as a string) of the anim int used for weapon stances.")]
    private string stanceParameter = "WeaponStance";


    [Header("Object & Component References")]

    [SerializeField, Tooltip("The tf of the WeaponContainer, where the weapon should be held.")]
    private Transform attachPoint;

    [SerializeField, Tooltip("The animator on this character.")]
    private Animator animator;

        #region Enum Definitions
    // Enum for weapon types, to help tell animator which weapon stance to use.
    public enum WeaponStance
    {
        Unarmed = 0,
        Rifle = 1,
        Handgun = 2
    }
        #endregion Enum Definitions
    #endregion Fields





    #region Unity Methods
    // Start is called before the first frame update
    public virtual void Start()
    {
        // If any of these are null, try to set them up.
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }
    #endregion Unity Methods


    #region Dev Methods
    // Creates a weapon from the Weapon prefab passed in and equips it.
    public void EquipWeapon(Weapon weapon)
    {
        // Create the weapon as our equipped weapon.
        equippedWeapon = Instantiate(weapon) as Weapon;

        // Get the Transforms, then set its parent and location/rotation.
        Transform weapon_tf = equippedWeapon.transform;
        Transform prefab_tf = weapon.transform;
        weapon_tf.parent = attachPoint.transform;
        weapon_tf.localPosition = prefab_tf.localPosition;
        weapon_tf.localRotation = prefab_tf.localRotation;

        // Tell the animator which weapon stance to use.
        animator.SetInteger(stanceParameter, (int)equippedWeapon.weaponStance);
    }

    // Unequips the weapon, destroying it.
    public void UnequipWeapon()
    {
        // If there is a weapon equipped,
        if (equippedWeapon != null)
        {
            // Then destroy the equipped weapon. Ensure the variable is now null.
            Destroy(equippedWeapon.gameObject);
            equippedWeapon = null;

            // Tell the animator to show characcter as unarmed.
            animator.SetInteger(stanceParameter, (int)WeaponStance.Unarmed);
        }
    }
    #endregion Dev Methods
}
