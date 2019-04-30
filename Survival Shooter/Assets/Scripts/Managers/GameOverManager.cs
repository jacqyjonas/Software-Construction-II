using UnityEngine;

/**
 * This class is responsible for triggering
 * the GameOver animation
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/7/2019
 * */
public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;  //Reference to the player's health

    private Animator _animator;        //Reference to the animator component

    /// <summary>
    /// Called regardless of whether the script is enabled or not.
    /// Used to set up references.
    /// </summary>
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        //If the player is out of health, trigger the GameOver animation
        if(playerHealth.currentHealth <= 0)
        {
            _animator.SetTrigger("GameOver");
        }
    }
}
