using UnityEngine;
using UnityEngine.AI;

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

    [SerializeField, Tooltip("Where the loot spawned by this player should spawn at.")]
    private Transform lootSpawnLocation;

    [SerializeField, Tooltip("The HealthBar attached to this enemy.")]
    private HealthBar healthBar;


    [Header("Item Drop Settings")]

    [SerializeField, Range(0, 100),
        Tooltip("The % chance that this enemy will drop an item when DropRandomItem is called.")]
    private int itemDropChance = 50;

    [SerializeField, Tooltip("Objects in this array can be dropped by this enemy." +
        " Each has a chance associated with it.")]
    private WeightedObject[] itemDrops;

    // Cumalitive Density Function array for the itemDrops array.
    private float[] cdfArray;
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

        // Register this enemy's health bar.
        UIManager.Instance.RegisterEnemy(health, healthBar);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        // If any of these are null, try to set them up.
        if (ai == null)
        {
            ai = GetComponent<AI_Controller>();
        }

        // Create the CDF array.
        CreateCDFArray();

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {


        base.Update();
    }
    #endregion Unity Methods


    #region Dev Methods
    // Call this via events when the enemy dies.
    public override void HandleDeath()
    {
        // Remove the enemy's Monobehaviors it won't need anymore.
        Destroy(this);
        Destroy(GetComponent<AI_Controller>());
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<Health>());
        Destroy(GetComponent<Spawner>());

        base.HandleDeath();
    }

    // Create the cdfArray for the itemDrops.
    private void CreateCDFArray()
    {
        // Set up the cdfArray for the itemDrops.
        cdfArray = new float[itemDrops.Length];
        // Look through the itemDrops array.
        for (int i = 0; i < itemDrops.Length; i++)
        {
            // If this is the first element,
            if (i == 0)
            {
                // then only add this element, not including the one before it.
                cdfArray[i] = itemDrops[i].chance;
            }
            // Else, this is not the first element.
            else
            {
                // Add this element's chance plus the last entry in the cdfArray.
                cdfArray[i] = itemDrops[i].chance + cdfArray[i - 1];
            }
        }
    }

    // Randomly choose a value from the itemDrops array using the weighted chances.
    private GameObject WeightedRandomDrop()
    {
        // Create the random number.
        int randNum = Random.Range(0, (int)cdfArray[cdfArray.Length - 1]);

        // Perform a binary search through the cdfArray with the random number.
        int selectedIndex = System.Array.BinarySearch(cdfArray, randNum);
        // If the result is a negative number (the search did not hit one of the thresholds *exactly*),
        if (selectedIndex < 0)
        {
            // then perform a bitwise NOT.
            selectedIndex = ~selectedIndex;
        }

        // Return the value of the result of the search.
        return itemDrops[selectedIndex].value;
    }

    // Drops a random itemDrop, either with a weighted chance or an equal chance between all items.
    public void DropRandomItem(bool isWeighted = true)
    {
        // Get a random int, 1 - 100.
        float randNum = Random.Range(1, 100);
        // If that number is higher than the itemDropChance,
        if (randNum > itemDropChance)
        {
            // then return. No NOT drop a random item.
            return;
        }
        
        GameObject dropItem;
        // If the drop should be weighted,
        if (isWeighted)
        {
            // then get a GameObject reference with a weighted random selection.
            dropItem = WeightedRandomDrop();
        }
        // Else, it should be non-weighted, an equal chance for all items.
        else
        {
            // Choose a random drop from the array of item drops without accounting for weighted chance.
            dropItem = itemDrops[Random.Range(0, itemDrops.Length)].value;
        }

        // Instantiate the dropItem.
        Instantiate(dropItem, lootSpawnLocation.position, lootSpawnLocation.rotation);
    }

    // Overrides the WeaponAgent's EquipWeapon method.
    public override void EquipWeapon(Weapon weapon)
    {
        base.EquipWeapon(weapon);
    }
    #endregion Dev Methods
}
