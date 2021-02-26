using UnityEngine;

public class HumanoidPawn : Pawn
{
    #region Fields
    [Header("IK Weights, Left Arm")]

    [SerializeField, Range(0, 1), Tooltip("The weight used for setting the left hand position for IK.")]
    private float leftHandPosition_Weight = 1.0f;

    [SerializeField, Range(0, 1), Tooltip("The weight used for setting the left hand rotation for IK.")]
    private float leftHandRotation_Weight = 1.0f;

    [SerializeField, Range(0, 1), Tooltip("The weight used for setting the left elbow hint for IK.")]
    private float leftElbowHint_Weight = 1.0f;


    [Header("IK Weights, Right Arm")]

    [SerializeField, Range(0, 1), Tooltip("The weight used for setting the right hand position for IK.")]
    private float rightHandPosition_Weight = 1.0f;

    [SerializeField, Range(0, 1), Tooltip("The weight used for setting the right hand rotation for IK.")]
    private float rightHandRotation_Weight = 1.0f;

    [SerializeField, Range(0, 1), Tooltip("The weight used for setting the right elbow hint for IK.")]
    private float rightElbowHint_Weight = 1.0f;


    [Header("Object & Component references")]

    // The CharacterData on this character.
    [SerializeField] private PlayerData data;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    public void Start()
    {
        // If any of these are not set up, set them up.
        if (data == null)
        {
            data = GetComponent<PlayerData>();
        }
    }

    // Moves the character's hands toward the weapon's hand IK points.
    public void OnAnimatorIK(int layerIndex)
    {
        // Try to save a reference to the equipped weapon.
        Weapon weapon = data.equippedWeapon;
        // If no weapon is equipped,
        if (weapon == null)
        {
            // then do nothing. Return.
            return;
        }
        // Else, there is a weapon.

        // If this weapon needs IK for the left hand, then do so.
        if (weapon.leftHandPoint)
        {
            // Get a reference to Transform to save on finding it more than once.
            Transform leftHand = weapon.leftHandPoint;

            // Set the IK position and rotation for left hand, with maximum weights.
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandPosition_Weight);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandRotation_Weight);

            // Set the hint for the left elbow and the weight.
            animator.SetIKHintPosition(AvatarIKHint.LeftElbow, weapon.leftElbowPoint.position);
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, leftElbowHint_Weight);
        }
        // Else, no left hand needed.
        else
        {
            // Set the weights to 0.
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }

        // If this weapon needs IK for the right hand, then do so.
        if (weapon.rightHandPoint)
        {
            // Get a reference to Transform to save on finding it more than once.
            Transform rightHand = weapon.rightHandPoint;

            // Set the IK position and rotation for right hand, with maximum weights.
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandPosition_Weight);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandRotation_Weight);

            // Set the hint for the right elbow and the weight.
            animator.SetIKHintPosition(AvatarIKHint.RightElbow, weapon.rightElbowPoint.position);
            animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rightElbowHint_Weight);
        }
        // Else, no right hand needed.
        else
        {
            // Set the weights to 0.
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        }
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
    #endregion Dev Methods
}
