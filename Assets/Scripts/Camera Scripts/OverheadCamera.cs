using UnityEngine;

// Attach this script to the Main Camera and it will follow the character pulled into the character variable.

public class OverheadCamera : MonoBehaviour
{
    #region Fields
    // The Transform of the character that this camera should follow.
    [SerializeField] private Transform character;

    // The Transform of this camera.
    [SerializeField] private Transform tf;

    // The distance desired between the camera and the player.
    [SerializeField] private Vector3 offset = new Vector3(0, 10, 0);

    // The  slower, default speed that the camera follows the character.
    [SerializeField] private float moveSpeed_Default = 2.0f;

    // The speed that the camera follows the character while the character is sprinting.
    [SerializeField] private float moveSpeed_Sprinting = 4.5f;
    #endregion Fields

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        // If any of these variables are not set up, set them up.

        if (tf == null)
        {
            tf = transform;
        }

        if (character == null)
        {
            character = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    #endregion Unity Methods

    #region Dev Methods
    // Alter the camera boom's position appropriately to follow the character.
    public void FollowCharacter(bool isSprinting)
    {
        // The speed that the camera will follow the character.
        float moveSpeed;
        // Determine the speed by whether or not the character is sprinting.
        if (isSprinting)
        {
            moveSpeed = moveSpeed_Sprinting;
        }
        else
        {
            moveSpeed = moveSpeed_Default;
        }

        // Follow the character as they move, staying 10.0f units above, at moveSpeed/second.
        tf.position = Vector3.MoveTowards
            (
                tf.position,
                character.position + offset,
                moveSpeed * Time.deltaTime
            );
    }
    #endregion Dev Methods
}
