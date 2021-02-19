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
    #endregion Fields

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        // If the animator is not set up,
        if (animator == null)
        {
            // then set up the animator.
            animator = GetComponent<Animator>();
        }

        // If the transform is not set up,
        if (tf == null)
        {
            // then set up the transform.
            tf = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion Unity Methods

    #region Dev Methods
    // Called by a controller to move the character. Moves the character in a direction at a speed.
    public void Move(Vector3 direction, float speed)
    {
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
