using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    [Header("Projectile")]

    [SerializeField, Tooltip("The time after instantiation before the projectile destroys itself.")]
    private float lifespan = 2.0f;

    // The damage this projectile will do to Health targets it hits. Set at istantiation by the Weapon.
    public float damage = 1.0f;
    
    
    [Header("Object & Component References")]

    [Tooltip("The Rigidbody on this projectile gameObject.")]
    public Rigidbody rb;
    #endregion Fields


    #region Unity Methods
    // Called at instantiation.
    private void Awake()
    {
        // Start the lifespan timer to self-destroy the bullet.
        Destroy(gameObject, lifespan);
    }

    // Start is called before the first frame update
    void Start()
    {
        // If any of these are not set up, try to set them up.
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Called when the bullet comes into contact with a collider.
    private void OnCollisionEnter(Collision collision)
    {
        // Attempt to get the Health from the collided object.
        Health hitObject = collision.gameObject.GetComponent<Health>();

        // If the object it collided with has Health,
        if (hitObject != null)
        {
            // then damage that object.
            hitObject.Damage(damage);
        }
        
        // Destroy the bullet.
        Destroy(gameObject);
    }
    #endregion Unity Methods


    #region Dev Methods

    #endregion Dev Methods
}
