using UnityEngine;

public class HumanoidPawn : Pawn
{
    #region Fields
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
            // Set the IK position and rotation for left hand, with maximum weights.
            animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.leftHandPoint.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.leftHandPoint.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
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
            // Set the IK position and rotation for right hand, with maximum weights.
            animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.rightHandPoint.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.rightHandPoint.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
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
