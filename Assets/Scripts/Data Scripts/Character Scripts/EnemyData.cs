using UnityEngine;

// Theese scripts are required.
[RequireComponent(typeof(AI_Controller))]

public class EnemyData : WeaponAgent
{
    #region Fields
    [Header("Object & Component References")]

    [SerializeField, Tooltip("The AI Controller for this enemy.")]
    AI_Controller ai;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    public override void Start()
    {
        // If any of these are null, try to set them up.
        if (ai == null)
        {
            ai = GetComponent<AI_Controller>();
        }

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {


        base.Update();
    }
    #endregion Unity Methods


    #region Dev Methods

    #endregion Dev Methods
}
