using UnityEngine;

/**
 * Controls the game's pick up objects
 * 
 * Author - Jacqlyne Mba-Jonas
 **/

public class Rotator : MonoBehaviour
{
    // Called once per frame
    void Update()
    {
        //Rotate the pick up objects
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
