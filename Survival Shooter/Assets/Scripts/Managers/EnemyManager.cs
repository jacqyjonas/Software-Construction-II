using UnityEngine;

/**
 * This class is responsible for spawning enemies
 * during game play.
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/6/2019
 * */
public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;  //Reference to the player's heatlh
    public GameObject enemy;           //Reference to the enemy prefab
    public float spawnTime = 3f;       //The time between each spawn
    public Transform[] spawnPoints;    //Array of points the enemy can spawn from

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        //Call the Spawn method repeatedly at the set repeat rate
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    /// <summary>
    /// When called, this method will spawn an enemy into the game
    /// if the player is still alive.
    /// </summary>
    void Spawn()
    {
        //Do not proceed if the player is dead
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //Generate a random spawn point
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        //Spawn an enemy at the spawn point and orient it
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
