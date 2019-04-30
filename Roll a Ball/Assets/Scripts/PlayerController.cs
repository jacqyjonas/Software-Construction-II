using UnityEngine;
using UnityEngine.UI;

/**
 * Controls the game's player object,
 * and handels it interacts with other
 * objects in the game
 * 
 * Author - Jacqlyne Mba-Jonas
 **/
public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    // Initializer, called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    //Called every fixed frame-rate frame
    void FixedUpdate()
    {
        //Control the player's movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
    
    //Called when game objects collide
    void OnTriggerEnter(Collider other)
    {
        //Deactivate a pick up object when the player object
        //collides with it and increment the counter
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }

    //Update the count text object in the game and 
    //display a message when the user has won the game
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
