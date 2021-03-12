using UnityEngine;

// Theese scripts are required.
[RequireComponent(typeof(AI_Controller))]

public class EnemyData : WeaponAgent
{
    #region Fields
    [Header("Object & Component References")]

    [SerializeField, Tooltip("The AI Controller for this enemy.")]
    AI_Controller ai;

    [SerializeField,
        Tooltip("Starting weapon chosen randomly from this array. Leave blank to start unarmed.")]
    private Weapon[] defaultWeapons;
    #endregion Fields


    #region Unity Methods
    // Called immediately when the gameObject is instantiated.
    public override void Awake()
    {
        base.Awake();

        // If the default weapons array is not empty,
        if (defaultWeapons.Length > 0)
        {
            // then equip a random weapon from the defaultWeapons array.
            EquipWeapon(defaultWeapons[Random.Range(0, defaultWeapons.Length)]);
        }
    }

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
