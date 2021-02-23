using System.Collections;
using UnityEngine;

// The input controller just takes input from the player, and tells the Pawn what that input was.
// The Pawn is what actually controls the character using root motion.

// The CharacterData and Pawn scripts are required.
[RequireComponent(typeof(CharacterData))]
[RequireComponent(typeof(Pawn))]

public class Player_InputController : MonoBehaviour
{
    #region Fields
    [Header("Controls")]
    // The key used to sprint with.
    [SerializeField, Tooltip("The key used to sprint")] private KeyCode sprintKey = KeyCode.LeftShift;

    // The key used to walk with.
    [SerializeField, Tooltip("The key used to walk")] private KeyCode walkKey = KeyCode.LeftAlt;

    [Header("Object/Component references")]
    // References the CharacterData on this character, which holds all character-specific data.
    [SerializeField] private CharacterData data;

    // The transform component on this player.
    [SerializeField] private Transform tf;

    // The Pawn that actually controls motion for this character via the animator and root motion.
    [SerializeField] private Pawn pawn;

    // The camera that will follow this player.
    [SerializeField] private Camera cam;
    #endregion Fields

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        // If any of these variables are not yet set up, set them up.

        if (data == null)
        {
            data = GetComponent<CharacterData>();
        }

        if (tf == null)
        {
            tf = transform;
        }

        if (pawn == null)
        {
            pawn = GetComponent<Pawn>();
        }

        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get whether or not the player is trying to sprint.
        bool sprint = Input.GetKey(sprintKey);

        // If not trying to sprint,
        if (!sprint)
        {
            // Tell the CharacterData that the player is not sprinting.
            data.isSprinting = false;
        }

        // Take the user's input and tell the Pawn to move the player, accounting for speed/sprint/walk.
        pawn.Move(TakeInput(), data.maxMoveSpeed, sprint, Input.GetKey(walkKey));

        // Get the point that the mouse is aiming at and tell the Pawn to turn the player
        // towards that point over time.
        pawn.TurnOverTime(GetMouseTarget(), data.turnSpeed);
    }
    #endregion Unity Methods

    #region Dev Methods
    // Called every frame to move the player based on their input.
    private Vector3 TakeInput()
    {
        // Create a new Vector3 to hold the user's current input.
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // Normalize the input to maximum of 1.
        input = Vector3.ClampMagnitude(input, 1f);
        // Translate the input to local from world for the animator.
        input = tf.InverseTransformDirection(input);

        // Return the input.
        return input;
    }

    // Gets the location that the mouse is pointing at (on a place at the character's feet).
    private Vector3 GetMouseTarget()
    {
        // Create a mathmatical plane perpendicular to the player at their feet (true 0 of their position).
        Plane plane = new Plane(Vector3.up, tf.position);
        // Create a ray from the mouse's position toward that plane, angled as if from the camera.
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        // Check how far the ray travelled to interset with the plane.
        if (plane.Raycast(ray, out float distance))
        {
            // Return the location where they intersected.
            return ray.GetPoint(distance);
        }
        // Else, something went wrong.
        else
        {
            // Log the problem.
            Debug.LogError("The raycast failed to find the plane for " + gameObject.name);

            // Return a point directly in front of where the player is currently facing.
            return tf.rotation.eulerAngles + tf.forward;
        }
    }
    #endregion Dev Methods
}
