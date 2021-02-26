using UnityEngine;

// Input controllers take input from the user. AI controllers "take input" from
// the FSM. Both send the results to the pawn, which actually controls movement.
// Think "motor" from the UATanks game.

public abstract class Pawn : MonoBehaviour
{
    #region Fields
    [Header("Object & Component references")]

    // The animator attached to this character.
    [SerializeField] protected Animator animator;

    // The transform component on this character.
    [SerializeField] protected Transform tf;
    #endregion Fields

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        // If any of these are not set up, set them up.
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (tf == null)
        {
            tf = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion Unity Methods

    #region Dev Methods
    public virtual void Move(Vector3 direction, float speed, bool sprintKeyDown, bool walkKeyDown)
    {

    }

    // Called by a controller to turn the character toward a point instantaneously.
    public virtual void Turn(Vector3 targetPosition)
    {
        
    }

    // Called by a controller to turn the character toward a point over time.
    public virtual void TurnOverTime(Vector3 targetPosition, float turnSpeed)
    {
        
    }
    #endregion Dev Methods
}
