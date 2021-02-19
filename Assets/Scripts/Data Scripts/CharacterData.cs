using UnityEngine;

// The CharacterData holds data about the character that can be accessed from other scripts.
// This way, all data specific to this character is in one spot.

public class CharacterData : MonoBehaviour
{
    #region Fields
    // The maximum movement speed of this character.
    [Tooltip("Max movement speed of this character.")]
    public float maxMoveSpeed = 4.0f;

    // The speed at which this character can turn their body.
    [Tooltip("Speed that this character can turn.")]
    public float turnSpeed = 90.0f;
    #endregion Fields

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion Unity Methods
}
