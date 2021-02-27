using UnityEngine;
using System.Collections;

public class Gun : Weapon
{
    #region Fields
    [Header("Fire Rate")]

    [SerializeField, Tooltip("If false, bullet shoots as fast as can pull the trigger.")]
    private bool limitedFireRate = false;

    [SerializeField, Tooltip("How many rounds gun can fire / second.")]
    private float roundsPerMinute = 45;

    // Holds the variable for roundsPerMinute calculated into roundsPerSecond. Calculated in Start.
    private float roundsPerSecond;

    // How long it has been (in seconds) since the last time the gun was fired.
    private float timeSinceFired = 0.0f;

    // Whether or not the gun can currently shoot.
    private bool canShoot = true;


    [Header("Projectile Settings")]

    [SerializeField, Tooltip("The Bullet prefab that this gun fires as a projectile.")]
    private Projectile projectilePrefab;

    [SerializeField, Tooltip("Transform of the location projectiles should spawn (the barrel).")]
    private Transform barrel;

    [SerializeField, Tooltip("The damage that each projectile will deal to Health objects they hit.")]
    private float projectileDamage = 10.0f;

    [SerializeField, Tooltip("The amount of force with which the projectile is propelled from the barrel.")]
    private float muzzleVelocity = 1000.0f;

    [SerializeField, Tooltip("Whether or not the gun should have bullet spread.")]
    private bool hasSpread = true;

    [SerializeField, Tooltip("The amount of variance in initial trajectory of the bullets.")]
    private float spread = 3.0f;
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    public override void Start()
    {
        // Calculate the roundsPerSecond one time and store that number.
        roundsPerSecond = roundsPerMinute / 60;

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
        // Create a Projectile (bullet) at the barrel.
        Projectile projectile = Instantiate
            (
                projectilePrefab,
                barrel.position,
                GetTrajectory()
            ) as Projectile;

        // Assign the projectile its damage. Match its layer to the gun's layer.
        projectile.damage = projectileDamage;
        projectile.gameObject.layer = gameObject.layer;

        // Add force to the bullet to make it "shoot" out of the barrel.
        projectile.rb.AddRelativeForce
            (
                Vector3.forward * muzzleVelocity,
                ForceMode.VelocityChange
            );
    }

    // Calculates and returns the bullet's initial trajectory.
    private Quaternion GetTrajectory()
    {
        // If this weapon uses bullet spread,
        if (hasSpread)
        {
            // then include spread in the calculation.
            return barrel.rotation * Quaternion.Euler(Random.onUnitSphere * spread);
        }
        // Else, no dot use the spread.
        else
        {
            return barrel.rotation;

        }
    }

    // Prevents firing the gun for a set period of time.
    private IEnumerator CantFire()
    { 
        // Until enough time has passed for the gun to fire (based on roundsPerMinute),
        while (timeSinceFired < roundsPerSecond)
        {
            // Track the time since last fired.
            timeSinceFired += Time.deltaTime;
            // then do nothing this frame.
            yield return null;
        }

        // Once enough time has passed,
        // Reset the timer.
        timeSinceFired = 0.0f;
        // The gun can now shoot.
        canShoot = true;
    }
    #endregion Dev Methods
}
