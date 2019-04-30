using UnityEngine;

/**
 * This class enables the player to
 * shoot at an enemy
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/6/2019
 * */
public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                          //Damage done by a bullet
    public float timeBetweenBullets = 0.15f;                //Amount of time bewteen each shot
    public float range = 100f;                              //How far a bullet can travel

    private float _timer;                                   //Timer for pacing shots.
    private Ray _shootRay = new Ray();                      //A ray from the tip of the gun when fired
    private RaycastHit _shootHit;                           //The object that was hit by the shot ray
    private int _shootableMask;                             //For casting a ray at objects on the shootable layer.
    private ParticleSystem _gunParticles;                   //Reference to the particle system
    private LineRenderer _gunLine;                          //Reference to the line renderer
    private AudioSource _gunAudio;                          //Reference to the audio source
    private Light _gunLight;                                //Reference to the light component
    private readonly float _effectsDisplayTime = 0.2f;      //The amount of time the shooting effect will be displayed for


    /// <summary>
    /// Called regardless of whether the script is enabled or not.
    /// Used to set up references.
    /// </summary>
    void Awake()
    {
        _shootableMask = LayerMask.GetMask("Shootable");
        _gunParticles = GetComponent<ParticleSystem>();
        _gunLine = GetComponent<LineRenderer>();
        _gunAudio = GetComponent<AudioSource>();
        _gunLight = GetComponent<Light>();
    }


    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        _timer += Time.deltaTime; //Add the time since this method was last called to the timer

        //If the player has hit the left mouse button or the left control key 
        //and the set time has passed, shoot the gun.
        if (Input.GetButton("Fire1") && _timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        //If the set shoot effects time has elapsed after the player fires, disable the effects.
        if(_timer >= timeBetweenBullets * _effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    /// <summary>
    /// Turns the gun light and line rendering off
    /// </summary>
    public void DisableEffects()
    {
        _gunLine.enabled = false;
        _gunLight.enabled = false;
    }

    /// <summary>
    /// Handles the physics of firing the gun.
    /// </summary>
    private void Shoot()
    {
        _timer = 0f; //Reset the time
        _gunAudio.Play(); //Play the gun audio clip
        _gunLight.enabled = true; //Enable the gun light

        //If the gun particles are playing, stop them and start them again
        _gunParticles.Stop();
        _gunParticles.Play();

        //Enable the visual elelment of the bullet starting from the tip of the gun barrel
        _gunLine.enabled = true;
        _gunLine.SetPosition(0, transform.position);

        //Start from the tip of the gun barrel and fire in the forward direction of the player
        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        //Did the bullet hit something
        if(Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
        {
            //If what we hit was an enemy, we retrieve the enemy's health and make the
            //enemy take damage.
            EnemyHealth enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, _shootHit.point);
            }

            //Show the ray going form the gun to the object that was hit
            _gunLine.SetPosition(1, _shootHit.point);
        }
        else
        {
            //If there was no direct hit, shoot the bullet (ray) out as far as the gun range will allow
            _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
        }
    }
}
