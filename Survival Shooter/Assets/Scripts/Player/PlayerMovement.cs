using UnityEngine;

/**
 * This class handles the player's movement, 
 * turning and animation operations.
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/2/2019
 * */
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;                        //The speed at which the player will move

    private readonly float _camRayLength = 100f;    //The length of the ray from the camera into the scene
    private int _floorMask;                         //For casting a ray at game objects on the floor level
    private Vector3 _movement;                      //The direction of the player's movement
    private Animator _animator;                     //Reference to the animator component
    private Rigidbody _playerRigidbody;             //Reference to the player's rigidbody component

    /// <summary>
    /// Called regardless of whether the script is enabled or not.
    /// Used to set up references.
    /// </summary>
    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _animator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Called with every physics update for the player
    /// </summary>
    void FixedUpdate()
    {
        //Maps to the left and right keys
        float horizontal = Input.GetAxisRaw("Horizontal");

        //Maps to the up and down keys
        float vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);
        Turning();
        Animating(horizontal, vertical);
    }

    /// <summary>
    /// Controls the player's movement and speed
    /// </summary>
    /// <param name="horizontal">Current horizontal axis</param>
    /// <param name="vertical">Current vertical axis</param>
    private void Move(float horizontal, float vertical)
    {
        _movement.Set(horizontal, 0f, vertical); //Player movement is lateral

        //Player moves at the same speed per second regardless of direction
        _movement = _movement.normalized * speed * Time.deltaTime;

        //Apply the movement to the player
        _playerRigidbody.MovePosition(transform.position + _movement);
    }

    /// <summary>
    /// Makes the player turn to face the mouse position when it is moved
    /// </summary>
    private void Turning()
    {
        //Create a ray from the mouse position point to the camera
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Did the ray hit anything on the game floor?
        if(Physics.Raycast(cameraRay, out RaycastHit floorHit, _camRayLength, _floorMask))
        {
            //Vector from the position of the player to the point on the floor the ray cast hit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f; //Make it lateral

            //Rotate the player to face the mouse position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _playerRigidbody.MoveRotation(newRotation);
        }
    }

    /// <summary>
    /// Animates the player when the player is moving
    /// </summary>
    /// <param name="horizontal">Current horizontal axis</param>
    /// <param name="vertical">Current vertical axis</param>
    private void Animating(float horizontal, float vertical)
    {
        //At least one axis should have a non-zero input to indicate movement
        bool isWalking = horizontal != 0f || vertical != 0f;
        _animator.SetBool("IsWalking", true);
    }
}
