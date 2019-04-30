using UnityEngine;

/**
 * This class handles the camera's movement
 * causing it to follow the player around.
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/2/2019
 * */
public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget;  //The player's position which the camera will follow
    public float smoothing = 5f;    //The speed at which the camera will follow the player

    private Vector3 _offset;        //Distance from the player

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        //Distance between the camera and the player
        _offset = transform.position - playerTarget.position;
    }

    /// <summary>
    /// Called with every physics update for the camera
    /// </summary>
    void FixedUpdate()
    {
        //Determine the position the camera will face
        Vector3 targetCameraPosition = playerTarget.position + _offset;

        //Smoothly make the camera face the player
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
    }
}

