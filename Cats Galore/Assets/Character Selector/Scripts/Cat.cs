using System;
using UnityEngine;

/**
 *  This class is responsible for setting the fur and
 *  eye color of cats spawned in the game.
 *  
 *  Author - Jacqlyne Mba-Jonas
 *  Date - 4/10/2019
 * */
public class Cat : MonoBehaviour
{
    public Material[] furColor;                                                         //Holds the color materials for the a cats's fur
    public Material[] eyeColor;                                                         //Holds the color materials for the a cats's eyes
    public GameObject body;                                                             //Reference to the cat's body
    public GameObject rightEye;                                                         //Reference to the cat's right eye
    public GameObject leftEye;                                                          //Reference to the cat's left eye

    private System.Random _random = new System.Random(Guid.NewGuid().GetHashCode());    //Seeded Random number generator

    /// <summary>
    /// Start is called before the first frame update.
    /// Used for initialization when Instantiate is called from the spawner. 
    /// </summary>
    void Start()
    {
        int colorIndex = _random.Next(0, 3); //Get a random number between 0-2

        RenderCube(body, colorIndex, furColor); //Set the color of the cat's fur

        colorIndex = _random.Next(0, 3); //Get a random number between 0-2

        //Set the color of the cat's right and left eye. Make them the same color.
        RenderCube(rightEye, colorIndex, eyeColor);
        RenderCube(leftEye, colorIndex, eyeColor);
    }

    /// <summary>
    /// Set the color of the given game object.
    /// </summary>
    /// <param name="gameObject">The object whose color will be set</param>
    /// <param name="colorIndex">The index used with the color array</param>
    /// <param name="colors">Array of material colors</param>
    private void RenderCube(GameObject gameObject, int colorIndex, Material[] colors)
    {
        //Get the game object renderer component, enable it, and set its color.
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = colors[colorIndex];
    }

    /// <summary>
    /// Getter for the cat's fur color.
    /// </summary>
    /// <returns>The material representating the cat's fur color</returns>
    public Material GetFurColor()
    {        
        return body.GetComponent<Renderer>().sharedMaterial;
    }

    /// <summary>
    /// Getter for the cat's eye color. Since both eyes colors
    /// are always the same, check against only one eye.
    /// </summary>
    /// <returns>The material representating the cat's eye color</returns>
    public Material GetEyeColor()
    {
        return rightEye.GetComponent<Renderer>().sharedMaterial;
    }
}
