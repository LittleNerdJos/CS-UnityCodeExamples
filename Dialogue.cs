using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//This script relies on variables in the ItemCollector and PlayerMovment scripts
public class Dialogue : MonoBehaviour
{
    /// 
    /// Variables
    ///

    //graphical user interfact; this will store the text box
    public TextMeshProUGUI textComponent;
    public GameObject dialoguePanel;
    public ItemCollector collectable;
    public GameObject player;

    //string array to store the individual lines
    //only one line will be shown on the screen at a time
    //string means it stores text
    private string[] preCoinLines = new string[]
    {"Hello!",
    "Please get that coin for me!" };

    private string[] postCoinLines = new string[]
    {"Thanks for bringing me that coin!"};

    //this will control how fast the text is typed out
    public float textSpeed;

    //this will keep track of what line we are on
    private int index;


    /// 
    /// Methods
    ///

    // Start is called before the first frame update
    void Start()
    {
        //empty and disable dialogue on start
        textComponent.text = string.Empty;
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsClose())
        {
            if (collectable.GetItemCount() < 1)
            {
                if (dialoguePanel.activeSelf == false)
                {
                    dialoguePanel.SetActive(true);
                    //start the dialogue when the game starts
                    StartDialogue(preCoinLines);

                    //PlayerMovement is the name of a custom script
                    //playerControlsEnables is a variable in that script
                    //turn off player movement when dialogue starts
                    PlayerMovement.playerControlsEnabled = false;
                }
                else
                {
                    //if the full line is showing, start the next line
                    //if someone hits space before the full line is howing
                    //then reveal the full line
                    if (textComponent.text == preCoinLines[index])
                    {
                        NextLine(preCoinLines);
                    }
                    else
                    {
                        StopAllCoroutines();
                        textComponent.text = preCoinLines[index];
                    }
                }
            }
            else
            {
                if (dialoguePanel.activeSelf == false)
                {
                    dialoguePanel.SetActive(true);
                    //start the dialogue when the game starts
                    StartDialogue(postCoinLines);

                    //PlayerMovement is the name of a custom script
                    //playerControlsEnables is a variable in that script
                    //turn off player movement when dialogue starts
                    PlayerMovement.playerControlsEnabled = false;
                }
                else
                {
                    //if the full line is showing, start the next line
                    //if someone hits space before the full line is howing
                    //then reveal the full line
                    if (textComponent.text == postCoinLines[index])
                    {
                        NextLine(postCoinLines);
                    }
                    else
                    {
                        StopAllCoroutines();
                        textComponent.text = postCoinLines[index];
                    }
                }
            }
        }
    }

    void StartDialogue(string[] lines)
    {
        //start at the begining
        index = 0;

        //type the line
        StartCoroutine(TypeLine(lines[index]));
    }

    //this will iterate through each character in a line
    //in other words it types each character 1 by 1
    IEnumerator TypeLine(string line)
    {

        foreach (char letter in line.ToCharArray())
        {
            //add the current letter to the text display
            textComponent.text += letter;
            //pause between typing each letter
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(string[] lines)
    {
        textComponent.text = string.Empty;
        //checks if the line we are on is not the last line
        if (index < lines.Length -1){
            //increase index
            index++;
            //type the next line
            StartCoroutine(TypeLine(lines[index]));
        } else
        {
            //if there are no more lines, turn off the dialogue
            dialoguePanel.SetActive(false);
            //let the player move again
            PlayerMovement.playerControlsEnabled = true;
        }
    }

    //checks if an object (player) is close enough to this object
    private bool IsClose()
    {
        // the player is within a radius of 2 units to this game object
        // change the 4 to change the radius
        // the number should be the square of the distance you want
        if ((player.transform.position - this.transform.position).sqrMagnitude < 9)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


}
