using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerImage : MonoBehaviour
{
    public Image image;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is close enough and presses space
        if (Input.GetKeyDown(KeyCode.Space) && IsClose())
        {
            //change the state of the image 
            image.enabled = !image.enabled;
        }

        //if someone is NOT close enough
        //then the image goes away
        if (!IsClose())
        {
            image.enabled = false;
        }
    }

    //custom method
    //checks if an object (player) is close enough to this object
    private bool IsClose()
    {
        // the player is within a radius of 2 units to this game object
        // change the 4 to change the radius
        // the number should be the square of the distance you want
        if ((player.transform.position - this.transform.position).sqrMagnitude < 4)
        { 
            return true;
        } else
        {
            return false;
        }
        
    } 
}
