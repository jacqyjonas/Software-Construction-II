using UnityEngine;
using UnityEngine.AI;

/**
 * This class handles the enemy's movement.
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/3/2019
 * */
public class EnemyMovement : MonoBehaviour
{
    private Transform _player;            //Reference to the player's position
    private PlayerHealth _playerHealth;   //Reference to the player's health
    private EnemyHealth _enemyHealth;     //Reference to this enemy's health
    private NavMeshAgent _navMeshAgent;   //Reference to the nav mesh agent

    /// <summary>
    /// Called regardless of whether the script is enabled or not.
    /// Used to set up references.
    /// </summary>
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        //Move the enemy towards the player as long as they are both alive
        if (_enemyHealth.currentHealth > 0 && _playerHealth.currentHealth > 0)
        {
            _navMeshAgent.SetDestination(_player.position);
        }
        else
        { //If either are dead, disable the AI navigator
            _navMeshAgent.enabled = false;
        }
    }
}
