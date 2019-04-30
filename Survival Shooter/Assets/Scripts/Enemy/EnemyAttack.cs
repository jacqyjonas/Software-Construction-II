using UnityEngine;

/**
 * This class gives the enemy the ability
 * to attack and cause damage to the player.
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/4/2019
 * */
public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;    //The time between each attack in seconds
    public int attackDamage = 10;              //The amount of health lost per attack

    private Animator _Animimator;              //Reference to the animator component
    private GameObject _player;                //Reference to the player game object
    private PlayerHealth _playerHealth;        //Reference to the player's health
    private EnemyHealth _enemyHealth;          //Reference to the enemy's health
    private bool _playerInRange;               //Is the player in contact with the enemy's trigger collider
    private float _timer;                      //Timer for pacing attacks

    /// <summary>
    /// Called regardless of whether the script is enabled or not.
    /// Used to set up references.
    /// </summary>
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent <PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _Animimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Called when game objects collide with each other
    /// </summary>
    /// <param name="other">Collider object</param>
    void OnTriggerEnter(Collider other)
    {
        //If the enemy collides with the player, this is an attack on the player
        if(other.gameObject == _player)
        {
            _playerInRange = true;
        }
    }

    /// <summary>
    /// Called after game objects collided with each other
    /// </summary>
    /// <param name="other">Collider object</param>
    void OnTriggerExit(Collider other)
    {
        //If the enemy is no longer colliding with the player, the player stops being attacked
        if(other.gameObject == _player)
        {
            _playerInRange = false;
        }
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        _timer += Time.deltaTime; //Add the time since this method was last called to the timer

        //Attack the player when it is in range with an enemy who is stll alive 
        //and the time between attacks has passed
        if(_timer >= timeBetweenAttacks && _playerInRange && _enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        //If the player is out of health, animate the player to the "dead" state
        if(_playerHealth.currentHealth <= 0)
        {
            _Animimator.SetTrigger("PlayerDead");
        }
    }

    /// <summary>
    /// Called when the player is attacked
    /// </summary>
    private void Attack()
    {
        _timer = 0f;

        //if the player is alive, damage the player
        if(_playerHealth.currentHealth > 0)
        {
            _playerHealth.TakeDamage(attackDamage);
        }
    }
}
