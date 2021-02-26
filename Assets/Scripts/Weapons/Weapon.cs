using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    #region Fields
    [Header("Unity Events")]

    // The event to be called when the weapon starts its attack.
    public UnityEvent onAttackStart;

    // The event to be called when the weapon starts its attack.
    public UnityEvent onAttackEnd;


    [Header("Inverse Kinematic Points")]

    [Tooltip("The Transform of the left hand of the character wielding this weapon.")]
    public Transform leftHandPoint;

    [Tooltip("The Transform of the left elbow of the character.")]
    public Transform leftElbowPoint;

    [Tooltip("The Transform of the right hand of the character wielding this weapon.")]
    public Transform rightHandPoint;

    [Tooltip("The Transform of the right elbow of the character.")]
    public Transform rightElbowPoint;


    [Header("Animation Assistance")]

    [Tooltip("The WeaponStance that is appropriate for holding this weapon.")]
    public WeaponAgent.WeaponStance weaponStance = WeaponAgent.WeaponStance.Unarmed;


    [Header("Match Rotation To Target Transform")]

    [SerializeField, Tooltip("The Transform to match rotations with. Leave empty for to rotation.")]
    private Transform rotationTarget;

    [SerializeField, Tooltip("The speed at which the rotation will occur.")]
    private float rotationSpeed = 5.0f;


    [Header("Object & Component References")]

    [SerializeField, Tooltip("The Transform on this gameObject.")]
    private Transform tf;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // If the rotationTarget isn't null, ensure the weapon is matching the rotation.
        if (rotationTarget != null)
        {
            MatchTargetRotation();
        }
    }
    #endregion Unity Methods


    #region Dev Methods
    // Called at the beginning of the attack with this weapon.
    public virtual void AttackStart()
    {

    }

    // Called at the end of the attack with this weapon.
    public virtual void AttackEnd()
    {

    }

    //Rotate the weapon to match the rotation of the target.
    private void MatchTargetRotation()
    {
        tf.rotation = Quaternion.Slerp
            (
                tf.rotation,
                rotationTarget.rotation,
                rotationSpeed * Time.deltaTime
            );
    }
    #endregion Dev Methods
}
