using UnityEngine;

// Attach this script to a camera boom, and the camera to that boom at the desired height from the ground.
// The boom follow the character it is assigned to, along with the attached camera.

public class OverheadCamera : MonoBehaviour
{
    #region Fields
    // The Transform of the character that this camera should follow.
    [SerializeField] private Transform character;

    // The Transform of this camera.
    [SerializeField] private Transform tf;

    // The Transform of the actual Camera gameObject attached to this boom.
    [SerializeField] private Transform camera_tf;

    // The distance desired between the camera and the player (vertically).
    [SerializeField] private float verticalCameraDistance = 10.0f;

    // The maximum distance the camera will be from the character (on the XZ).
    [SerializeField] private float horizontalCameraDistance = 4.0f;
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

        if (camera_tf == null)
        {
            camera_tf = GetComponentInChildren<Transform>();
        }

        // Ensure the camera starts at the correct height from the ground.
        camera_tf.localPosition = new Vector3(0, verticalCameraDistance, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move with the character.
        FollowCharacter();
    }
    #endregion Unity Methods

    #region Dev Methods
    // Alter the camera boom's position appropriately to follow the character.
    private void FollowCharacter()
    {
        // Follow the character as they move, allowing for a bit of variation up to horizontalCameraDistance.
        tf.position = Vector3.MoveTowards(tf.position, character.position, horizontalCameraDistance);
    }
    #endregion Dev Methods
}
