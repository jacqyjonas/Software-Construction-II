using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * This class is repsonsible for spawning a new cat
 * and starting a fight between the cats.
 * 
 *  Author - Jacqlyne Mba-Jonas
 *  Date - 4/10/2019
 * */
public class Spawner : MonoBehaviour
{
    public GameObject catObject;                                                    //Reference to a cat game object
                                                                                   
    private int _currentNumOfCats = 1;                                              //One cat is automatically added when the game starts
    private const int _MAX_NUM_OF_CATS = 7;                                         //The game can have up to 7 cats
    private const string _BLUE_MATERIAL_NAME = "_BlueMaterial";                     //Names of the color material components
    private const string _BLACK_MATERIAL_NAME = "_BlackMaterial";         
    private const string _BROWN_MATERIAL_NAME = "_BrownMaterial";
    private const string _GREEN_MATERIAL_NAME = "_GreenMaterial";
    private const string _CATS_ARE_NOT_FIGHTING = "The cats are getting along";     //Text displayed when cats are fighting
    private const string _CATS_ARE_FIGHTING = "The cats are fighting!";             //Text displayed when cats are not fighting
    
    [SerializeField]                                                 
    private Text _text;                                                             //Reference to the canvas StatusText component 

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        //If the left mouse button has been clicked...
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //And we have yet to hit the spawn limit...
            if (_currentNumOfCats < _MAX_NUM_OF_CATS) {
                //Get the position of the mouse at the time of the click
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Center the Z position to match that of the cat prefab
                Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, catObject.transform.transform.position.z);

                Spawn(adjustZ); //Add a new cat to the game

                //Wait 1 second to allow the new cat to completely get created
                //then check for a possible fight
                Invoke("CheckScenario", 1); 
            }
            else
            {
                Invoke("RestartLevel", 2); //If not, restart the game after 2 seconds.
            }
        }
    }

    /// <summary>
    /// Create a new cat in the game and place 
    /// it in the given position.
    /// </summary>
    /// <param name="position"></param>
    private void Spawn(Vector3 position)
    {
        Instantiate(catObject).transform.position = position;
        _currentNumOfCats += 1;
    }    

    /// <summary>
    /// Check if we are in the scenario necessary to start a fight
    /// </summary>
    private void CheckScenario()
    {
        Cat[] cats = FindObjectsOfType<Cat>(); //Get all the cats in the game

        if (cats.Length < 3)
        {
            return; //Not enough cats to start a possible fight
        }

        Dictionary<Material, int> furColorCount = new Dictionary<Material, int>();
        bool foundBlackGreenEyedCat = false;
        bool foundBlackBlueEyedCat = false;
        bool foundBlackBrownEyedCat = false;
        bool foundMulitpleBlackBrownEyedCats = false;

        foreach (Cat cat in cats)
        {
            Material furColor = cat.GetFurColor();

            //Count the number of cats that have this fur color
            furColorCount.TryGetValue(furColor, out int count);
            furColorCount[furColor] = count + 1;

            Material eyeColor =  cat.GetEyeColor();

            //Check for the requirements black cat scenario
            if(furColor.name == _BLACK_MATERIAL_NAME)
            {
                if(eyeColor.name == _GREEN_MATERIAL_NAME)
                {
                    foundBlackGreenEyedCat = true;  //Found at least one black cat with green eyes
                }
                if (eyeColor.name == _BLUE_MATERIAL_NAME)
                {
                    foundBlackBlueEyedCat = true; //Found at least one black cat with blue eyes
                }
                if (eyeColor.name == _BROWN_MATERIAL_NAME)
                {
                    if (!foundBlackBrownEyedCat)
                    {
                        foundBlackBrownEyedCat = true; //Found at least one black cat with brown eyes
                    }
                    else
                    {
                        foundMulitpleBlackBrownEyedCats = true; //Found more than one black cat with brown eyes
                    }
                }
            }
        }

        bool startFight = false;

        if (!foundMulitpleBlackBrownEyedCats && foundBlackGreenEyedCat 
            && foundBlackBlueEyedCat && foundBlackBrownEyedCat)
        {
            startFight = true; //If all conditions are met, start a fight.
            _text.text = _CATS_ARE_FIGHTING;
        }
        else
        {
            //If not, check for a fight scenario based on fur colors.
            startFight = CheckFurColorFightScenario(furColorCount);
        }

        //Incase there was an ongoing fight and the scenario has changed to a non-fighing scenario...
        if (!startFight) 
        {
            _text.text = _CATS_ARE_NOT_FIGHTING; //Stop the fight
        }
    }    

    /// <summary>
    /// Check if one color of cats outnumbers another. 
    /// </summary>
    /// <param name="furColorCount"></param>
    /// <returns></returns>
    private bool CheckFurColorFightScenario(Dictionary<Material, int> furColorCount)
    {
        //Get the first fur color and remove it form the collection to avoid a
        //redundant check.
        KeyValuePair<Material, int> FirstFurColor = furColorCount.First();
        furColorCount.Remove(FirstFurColor.Key);

        //Set the initial value to the count of the first fur color
        int numOfColor = FirstFurColor.Value;        
        bool startFight = false;

        foreach (KeyValuePair<Material, int> kvp in furColorCount)
        {
            //If there are more cats of a color....
            if(kvp.Value > numOfColor || kvp.Value < numOfColor)
            {
                startFight = true; //Start a fight
                break;
            }
        }

        if (startFight)
        {
            _text.text = _CATS_ARE_FIGHTING;
        }
        
        return startFight;
    }

    /// <summary>
    /// Restart the game.
    /// </summary>
    public void RestartLevel()
    {
        //Reload the active scene
        SceneManager.LoadScene(0);
    }
}
