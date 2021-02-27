using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Fields
    [Header("Spawn Logic")]

    [SerializeField, Tooltip("The method this spawner should use for spawns.")]
    private SpawnMode spawnMode = SpawnMode.Once;

    [SerializeField, Tooltip("The delay between spawns, for either Continuous or Maintain.")]
    private float spawnDelay = 4.0f;

    [SerializeField, Min(1), Tooltip("The number of units to be maintained for the Maintain mode.")]
    private int numUnitsToMaintain = 1;

    // The time since the last spawn occured.
    private float timeSinceSpawn = 0.0f;

    // Whether the Maintain mode is currently in the process of spawning a delayed unit.
    private bool spawningDelayedUnit = false;


    [Header("Object & Component References")]

    [SerializeField, Tooltip("The prefab that this spawner should create from.")]
    private GameObject spawnPrefab;

    [SerializeField, Tooltip("List of all units spawned by this spawner.")]
    private List<GameObject> spawnedUnits = new List<GameObject>();

    [SerializeField, Tooltip("The Transform on this gameObject.")]
    private Transform tf;

    [SerializeField, Tooltip("The Transform with the location where the spawn should occur.")]
    private Transform spawnLocation;


        #region Enum Definitions
    // Enum definition for the different spawn modes.
    // Once: Spawns once one time, then destroys itself.
    // Continuous: Continues to spawn over time.
    // Maintain: Operates as Continuous until a certain number of spawn reached.
    //     Maintains that number of spawns by spawning more when some are desroyed.
    public enum SpawnMode { Once, Continuous, Maintain }
        #endregion Enum Definitions
    #endregion Fields


    #region Unity Methods
    // Start is called before the first frame update
    public void Start()
    {
        // If any of these are null, try to set them up.
        if (tf == null)
        {
            tf = transform;
        }

        // If this spawner is set to only spawn one unit,
        if (spawnMode == SpawnMode.Once)
        {
            // then spawn one unit.
            SpawnUnit();
            // Destroy this spawner.
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        // Keep the unit list clear of null references.
        CleanList();

        // If the mode is set to Continuous,
        if (spawnMode == SpawnMode.Continuous)
        {
            // then perform the Continuous protocols.
            Continuous();
        }
        // Else, if set to Maintain,
        else
        {
            // then perform the Maintain protocols.
            Maintain();
        }
    }
    #endregion Unity Methods


    #region Dev Methods
    // Spawn a unit at the spawnLocation.
    private void SpawnUnit()
    {
        // Instantiate the spawnPrefab at the spawnLocation.
        GameObject unit = Instantiate(spawnPrefab, spawnLocation.position, spawnLocation.rotation);

        // If the spawnMode is anything other than Once,
        if (spawnMode != SpawnMode.Once)
        {
            // then add that spawn to the list of spawned units.
            spawnedUnits.Add(unit);

            // Reset the time since the last spawn.
            timeSinceSpawn = 0.0f;
        }
    }

    // Keeps the list of spawned units clean by removing those that are null. Called every frame.
    private void CleanList()
    {
        // Iterate through the list.
        for (int i = 0; i < spawnedUnits.Count; i++)
        {
            // If this unit is null,
            if (spawnedUnits[i] == null)
            {
                // then remove that unit from the list.
                spawnedUnits.RemoveAt(i);
            }
        }
    }

    // Perform all steps necessary for the Continuous mode each frame.
    private void Continuous()
    {
        // Track the time since last spawn.
        timeSinceSpawn += Time.deltaTime;

        // If enough time has passed to spawn another unit,
        if (timeSinceSpawn >= spawnDelay)
        {
            // then spawn another unit.
            SpawnUnit();
        }
    }

    // Perform all steps necessary for the Maintain mode each frame.
    private void Maintain()
    {
        // If there aren't too many units already, && not already spawning a delayed unit,
        if (spawnedUnits.Count < numUnitsToMaintain && !spawningDelayedUnit)
        {
            // then spawn a delayed unit.
            StartCoroutine(nameof(DelayedSpawn));
        }
    }

    // Spawn a unit only after a certain amount of time.
    private IEnumerator DelayedSpawn()
    {
        // Currently spawning a delayed unit.
        spawningDelayedUnit = true;

        // Until enough time has passed,
        while (timeSinceSpawn < spawnDelay)
        {
            // track the time.
            timeSinceSpawn += Time.deltaTime;

            // Yield.
            yield return null;
        }
        // Once enough time has passed,
        // no longer spawning delayed unit. Spawn the unit.
        spawningDelayedUnit = false;
        SpawnUnit();
    }
    #endregion Dev Methods
}
