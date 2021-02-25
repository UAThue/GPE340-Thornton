using UnityEngine;

public class HumanoidPawn : Pawn
{
    #region Fields
    [Header("Object & Component references")]

    // The CharacterData on this character.
    [SerializeField] private CharacterData data;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        // If any of these are not set up, set them up.
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
    public override void Move(Vector3 direction, float speed, bool sprintKeyDown, bool walkKeyDown)
    {
        // If the player is trying to sprint, and if the player can sprint,
        if (sprintKeyDown && data.CanSprint())
        {
            // then the current speed is fine. Do nothing to the speed.
        }
        // Else, can't sprint, or is not trying.
        else
        {
            // Tell the CharacterData that the player is not sprinting.
            data.isSprinting = false;

            // If player wants to walk,
            if (walkKeyDown)
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

        // Tell the camera for this character to follow the character.
        data.overheadCam.FollowCharacter(data.isSprinting);
    }

    // Called by a controller to turn the character toward a point instantaneously.
    public override void Turn(Vector3 targetPosition)
    {
        // Determine the angle necessary and immediately set this character's rotation to match.
        tf.rotation = Quaternion.LookRotation(targetPosition - tf.position);
    }

    // Called by a controller to turn the character toward a point over time.
    public override void TurnOverTime(Vector3 targetPosition, float turnSpeed)
    {
        // Determine the angle to which the character is trying to turn.
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - tf.position);
        // Rotate towards that angle over time.
        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, (turnSpeed * Time.deltaTime));
    }

    //
    public void OnAnimatorIK(int layerIndex)
    {
        // TODO: Ifs from convas

        // Set position for the left and right hands.
        animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.leftHandPoint.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.rightHandPoint.position);

        // Set rotation for the hands.
        animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.leftHandPoint.rotation);
        animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.rightHandPoint.rotation);
    }
    #endregion Dev Methods
}
