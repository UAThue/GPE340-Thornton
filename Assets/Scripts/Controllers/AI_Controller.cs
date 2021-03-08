using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : Controller
{
    #region Fields
    [Header("Navigation")]

    [SerializeField, Tooltip("The GameObject that this AI should be following.")]
    private GameObject target;

    [SerializeField, Tooltip("The NavMeshAgent on this gameObject.")]
    private NavMeshAgent agent;


    [Header("Object & Component References")]

    [SerializeField, Tooltip("The EnemyData on this AI.")]
    private EnemyData data;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    public override void Start()
    {
        // If any of these are null, try to set them up.
        if (target == null)
        {
            // Assume that we want to target the player if the target was not set up by designer.
            target = GameManager.Instance.GetPlayer().gameObject;
        }

        if (agent == null)
        {
            // Grab the NavMeshAgent component off of this gameObject.
            agent = GetComponent<NavMeshAgent>();
        }

        if (data == null)
        {
            data = GetComponent<EnemyData>();
        }

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Create a path to the target.
        agent.SetDestination(target.transform.position);
        // Get the direction that the navMeshAgent wants to move.
        Vector3 moveDirection = agent.desiredVelocity;
        // Tell the pawn to move. It will change from world directions to local directions automatically.
        pawn.Move(moveDirection, data.maxMoveSpeed / 4);

        /* NOTE: AT this point, both animator AND navMeshAgent are moving the enemy!
         * Within OnAnimatorMove(), this is resolved. */

        base.Update();
    }

    // Called after the animator has finished determining its changes.
    public void OnAnimatorMove()
    {
        // The animator determines how much to move, and we pass that velocity into the agent.
        agent.velocity = anim.velocity;
    }
    #endregion Unity Methods


    #region Dev Methods

    #endregion Dev Methods
}
