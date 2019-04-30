using UnityEngine;

/**
 * This class manipulates an enemy's health 
 * during gameplay
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/4/2019
 * */
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;             //Enemy's health at the start of the game
    public int currentHealth;                    //Enemy's current health
    public float sinkSpeed = 2.5f;               //How fast the enemy sinks through the floor when killed
    public int scoreValue = 10;                  //Amount of points added to the player's score when the enemy dies
    public AudioClip deathClip;                  //Sound played when the enemy dies

    private Animator _Animator;                  //Reference to the animator
    private AudioSource _enemyAudio;             //Reference to the audio source
    private ParticleSystem _hitParticles;        //Reference to the particle system
    private CapsuleCollider _capsuleCollider;    //Reference to the capsule collider
    private bool _isDead;                        //Is the enemy dead
    private bool _isSinking;                     //Is the enemy sinking


    /// <summary>
    /// Called regardless of whether the script is enabled or not.
    /// Used to set up references.
    /// </summary>
    void Awake()
    {
        _Animator = GetComponent<Animator>();
        _enemyAudio = GetComponent<AudioSource>();
        _hitParticles = GetComponentInChildren<ParticleSystem>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        //If the enemy is dieing, sink the enemy through the game floor
        //at the sink speed per second
        if(_isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Called when a player attacks the enemy
    /// </summary>
    /// <param name="amount">How much damage the enemy will take</param>
    /// <param name="hitPoint">The point the enemy was hit</param>
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        //If the enemy is dead, do not proceed
        if(_isDead)
        {
            return;
        }
        
        _enemyAudio.Play(); //play the hurt audio clip

        currentHealth -= amount; //reduce the enemy's current health
         
        //Make the hit particles appear from the point the enemy was hit
        _hitParticles.transform.position = hitPoint;
        _hitParticles.Play();

        //If the enemy is out of health, kill the enemy
        if(currentHealth<= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// Performs the operations needed to kill the enemy
    /// </summary>
    private void Death()
    {
        _isDead = true;

        _capsuleCollider.isTrigger = true; //Allow bullets to pass through the enemy

        _Animator.SetTrigger("Dead"); //Animate the enemy to the "dead" state

        //Play the death audio clip
        _enemyAudio.clip = deathClip;
        _enemyAudio.Play();
    }

    /// <summary>
    /// Called when the enemy is killed. Removes the enemy
    /// from the game and adds points to the player's score.
    /// </summary>
    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false; //Disable the AI navigator

        GetComponent<Rigidbody>().isKinematic = true; //This allows the enemy to sink

        _isSinking = true;

        ScoreManager.score += scoreValue; //Increase the player's score

        Destroy(gameObject, 2f); //Destroy the enemy game object after 2 seconds
    }
}
