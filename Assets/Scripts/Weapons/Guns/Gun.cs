using UnityEngine;
using System.Collections;

public class Gun : Weapon
{
    #region Fields
    [Header("Fire Rate")]

    [SerializeField, Tooltip("If false, bullet shoots as fast as can pull the trigger.")]
    private bool limitedFireRate = false;

    [SerializeField, Tooltip("How often the gun can fire / second.")]
    private float fireRate = 0.3f;

    // How long it has been (in seconds) since the last time the gun was fired.
    private float timeSinceFired = 0.0f;

    // Whether or not the gun can currently shoot.
    private bool canShoot = true;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    public override void Start()
    {


        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {


        base.Update();
    }
    #endregion Unity Methods


    #region Dev Methods
    // Called at the beginning of the attack with this weapon.
    public override void AttackStart()
    {
        // Always invoke the onAttackStart event.
        // Other methods called from the event, as decided by designers.
        onAttackStart.Invoke();

        base.AttackStart();
    }

    // Called at the end of the attack with this weapon.
    public override void AttackEnd()
    {
        // Always invoke the onAttackEnd event.
        // Other methods called from the event, as decided by designers.
        onAttackEnd.Invoke();

        base.AttackEnd();
    }

    // Fires a bullet from the gun, projecting it out from the muzzle.
    // Potentially affected by fireRate.
    public void FireBullet()
    {
        // If the gun is ready to shoot,
        if (canShoot)
        {
            // then fire a bullet.
            InstantiateBullet();

            // If the fire rate is limited,
            if (limitedFireRate)
            {
                // then the gun is no longer ready to shoot until firRate seconds have passed.
                canShoot = false;
                // Start the timer until gun can shoot again.
                StartCoroutine(nameof(CantFire));
            }
        }
        // Else, the rlfe cannot shoot because it was fired too recently.
        // Do nothing.
    }

    // Instantiates a bullet.
    private void InstantiateBullet()
    {
        // TODO: Instantiate bullet.
    }

    // Prevents firing the gun for a set period of time.
    private IEnumerator CantFire()
    {
        // Track the time since last fired.
        timeSinceFired += Time.deltaTime;

        // If time passed has not yet reached the fire rate,
        if (timeSinceFired < fireRate)
        {
            // then do nothing this frame.
            yield return null;
        }
        // Else, enough time has passed.
        else
        {
            // Reset the timer.
            timeSinceFired = 0.0f;
            // gun can now shoot.
            canShoot = true;
        }
    }
    #endregion Dev Methods
}
