using UnityEngine;

// The camera this is attached to will follow the character it is assigned to.

public class OverheadCamera : MonoBehaviour
{
    #region Fields
    // The Transform of the character that this camera should follow.
    [SerializeField] private Transform character;

    // The Transform of this camera.
    [SerializeField] private Transform tf;

    // The distance desired between the camera and the player.
    [SerializeField] private float cameraDistance = 10.0f;
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

        // Ensure the camera starts in the correct spot.
        FollowCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        // Move with the character.
        FollowCharacter();
    }
    #endregion Unity Methods

    #region Dev Methods
    // Alter the camera's position appropriately to follow the character.
    private void FollowCharacter()
    {
        // Set the camera's position to above the character the appropriate amount.
        // First, determine the correct Vector3, then change the values as necessary.
        Vector3 newPosition = character.position;
        newPosition.y += cameraDistance;
        tf.position = newPosition;
    }
    #endregion Dev Methods
}
