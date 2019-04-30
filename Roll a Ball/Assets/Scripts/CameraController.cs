using UnityEngine;

/**
 * Controls the game's camera
 * 
 * Author - Jacqlyne Mba-Jonas
 **/
public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Initializer, called before the first frame update
    void Start()
    {
        //Difference betwen the player and camera position
        offset = transform.position - player.transform.position;
    }

    // Called once per frame after Update
    void LateUpdate()
    {
        //move the camera to a position aligned with the player object
        transform.position = player.transform.position + offset;
    }
}
