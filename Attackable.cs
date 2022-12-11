using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    public GameObject player;
    private int destroyDistance = 3;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsClose())
        {
            Destroy(gameObject);
        }
    }

    //checks if an object (player) is close enough to this object
    private bool IsClose()
    {
        // the player is within a radius of 2 units to this game object
        if ((player.transform.position - this.transform.position).sqrMagnitude < destroyDistance * destroyDistance)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
