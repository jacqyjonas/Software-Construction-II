using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * This class manipulates the player's health 
 * during gameplay
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/3/2019
 * */
public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                            //Player's health at the start of the game
    public int currentHealth;                                   //Player's current health
    public Slider healthSlider;                                 //Reference to the health bar
    public Image damageImage;                                   //Reference to an image that flashes when the player is damaged
    public AudioClip deathClip;                                 //Audio clip to play when the player dies
    public float flashSpeed = 5f;                               //The speed at which the damageImage will fade
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     //The colour the damageImage is set to when flashed

    private Animator _animator;                                 //Reference to the animator component
    private AudioSource _playerAudio;                           //Reference to the audioSource component
    private PlayerMovement _playerMovement;                     //Reference to the PlayerMaovement script
    private PlayerShooting _playerShooting;                     //Reference to the PlayerShooting script
    private bool _isDead;                                       //Is the player dead
    private bool _damaged;                                      //Did the player take damage


    /// <summary>
    /// Called regardless of whether the script is enabled or not.
    /// Used to set up references.
    /// </summary>
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        //If the player takes damage, flash the damage in red.
        //if not, fade the image back to transparent
        if(_damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        _damaged = false; //Set the damaged flag
    }

    /// <summary>
    /// Called when an enemy attacks the player
    /// </summary>
    /// <param name="amount">How much damage the player will take</param>
    public void TakeDamage(int amount)
    {
        _damaged = true; //Set the damge flag

        currentHealth -= amount; //reduce the player's current health

        healthSlider.value = currentHealth; //update the health slider on the UI

        _playerAudio.Play(); //play the hurt audio clip

        //If the player runs out of health and is not dead, kill the player
        if (currentHealth <= 0 && !_isDead)
        {
            Death();
        }
    }

    /// <summary>
    /// Performs the operations needed to kill the player
    /// </summary>
    void Death()
    {
        _isDead = true; //Set the dead flag

        _playerShooting.DisableEffects(); //Turn off the shooting effects

        _animator.SetTrigger("Die"); //Animate the player to the "dead" state

        //play the death audio clip
        _playerAudio.clip = deathClip;
        _playerAudio.Play();

        //disable the movement and shooting scripts
        _playerMovement.enabled = false;
        _playerShooting.enabled = false;
    }

    /// <summary>
    /// Restart the game after the player dies
    /// </summary>
    public void RestartLevel()
    {
        //Reload the active scene
        SceneManager.LoadScene(0);
    }
}
