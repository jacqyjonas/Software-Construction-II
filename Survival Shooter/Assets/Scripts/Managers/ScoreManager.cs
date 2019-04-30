using UnityEngine;
using UnityEngine.UI;

/**
 * This class manages the player's score during game play
 * and updates the UI components to display the score.
 * 
 * Author - Jacqlyne Mba-Jonas
 * Date - 4/6/2019
 * */
public class ScoreManager : MonoBehaviour
{
    public static int score; //The player's current score

    private Text _text;      //Reference the score text component 

    /// <summary>
    /// Called regardless of whether the script is enabled or not.
    /// Used to set up references.
    /// </summary>
    void Awake()
    {
        _text = GetComponent<Text>();
        score = 0; //Rest the score at the start of the game
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        //Every frame, update the UI with the score
        _text.text = "Score: " + score;
    }
}
