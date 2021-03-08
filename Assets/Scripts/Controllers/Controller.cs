using UnityEngine;

// This script is required.
[RequireComponent(typeof(Pawn))]

public abstract class Controller : MonoBehaviour
{
    #region Fields
    [Header("Object & Component References")]

    [Tooltip("The Pawn on the Pawn's gameObject.")]
    protected Pawn pawn;

    [SerializeField, Tooltip("The Animator on this gameObject.")]
    protected Animator anim;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    public virtual void Start()
    {
        // If any of these are null, try to set them up.
        if (pawn == null)
        {
            pawn = GetComponent<Pawn>();
        }

        if (anim == null)
        {
            // Get the animator from the pawn.
            anim = pawn.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    #endregion Unity Methods


    #region Dev Methods

    #endregion Dev Methods
}
