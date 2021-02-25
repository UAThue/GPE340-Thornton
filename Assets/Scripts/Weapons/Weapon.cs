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

    [Tooltip("The Transform of the right hand of the character wielding this weapon.")]
    public Transform rightHandPoint;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
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
    #endregion Dev Methods
}
