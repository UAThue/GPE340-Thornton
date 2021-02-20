using UnityEngine;

// Input controllers take input from the user. AI controllers "take input" from
// the FSM. Both send the results to the pawn, which actually controls movement.
// Think "motor" from the UATanks game.

public class Pawn : MonoBehaviour
{
    #region Fields
    // The animator attached to this player.
    [SerializeField] private Animator animator;

    // The transform component on this player.
    [SerializeField] private Transform tf;

    // The CharacterData on this character.
    [SerializeField] private CharacterData data;
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

        if (data == null)
        {
            data = GetComponent<CharacterData>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion Unity Methods

    #region Dev Methods
    // Called by a controller to move the character. Moves the character in a direction at a speed.
    // Takes a bool for if the player is trying to sprint, and another for if trying to walk.
    public void Move(Vector3 direction, float speed, bool shiftDown, bool controlDown)
    {
        // If the player is trying to sprint, and if the player can sprint,
        if (shiftDown && data.CanSprint())
        {
            // then the current speed is fine. Do nothing to the speed.
        }
        // Else, can't sprint, or is not trying.
        else
        {
            // Tell the CharacterData that the player is not sprinting.
            data.isSprinting = false;

            // If player wants to walk,
            if (controlDown)
            {
                // then adjust the speed one quarter normal.
                speed /= 4;
            }
            // Else, character must move at normal run speed (half of the max speed that is passed in).
            else
            {
                // Adjust speed to half.
                speed /= 2;
            }
        }

        // Set the appropriate value on the animator to move.
        animator.SetFloat("Right", (direction.x * speed));
        animator.SetFloat("Forward", (direction.z * speed));
    }

    // Called by a controller to turn the character toward a point instantaneously.
    public void Turn(Vector3 targetPosition)
    {
        // Determine the angle necessary and immediately set this character's rotation to match.
        tf.rotation = Quaternion.LookRotation(targetPosition - tf.position);
    }

    // Called by a controller to turn the character toward a point over time.
    public void TurnOverTime(Vector3 targetPosition, float turnSpeed)
    {
        // Determine the angle to which the character is trying to turn.
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - tf.position);
        // Rotate towards that angle over time.
        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, (turnSpeed * Time.deltaTime));
    }
    #endregion Dev Methods
}
