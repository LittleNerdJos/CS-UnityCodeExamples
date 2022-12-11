using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    /// 
    /// Variables
    ///

    //graphical user interfact; this will store the text box
    public TextMeshProUGUI textComponent;

    //string array to store the individual lines
    //only one line will be shown on the screen at a time
    //string means it stores text
    public string[] lines;

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
        //empty dialogue on start
        textComponent.text = string.Empty;

        //start the dialogue when the game starts
        StartDialogue();

        //PlayerMovement is the name of a custom script
        //playerControlsEnables is a variable in that script
        //turn off player movement when dialogue starts
        PlayerMovement.playerControlsEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if the full line is showing, start the next line
            //if someone hits space before the full line is howing
            //then reveal the full line
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

        
    }

    void StartDialogue()
    {
        //start at the begining
        index = 0;

        //type the line
        StartCoroutine(TypeLine());
    }

    //this will iterate through each character in a line
    //in other words it types each character 1 by 1
    IEnumerator TypeLine()
    {

        foreach (char letter in lines[index].ToCharArray())
        {
            //add the current letter to the text display
            textComponent.text += letter;
            //pause between typing each letter
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        textComponent.text = string.Empty;
        //checks if the line we are on is not the last line
        if (index < lines.Length - 1){
            //increase index
            index++;
            //type the next line
            StartCoroutine(TypeLine());
        } else
        {
            //if there are no more lines, turn off the dialogue
            gameObject.SetActive(false);
            //let the player move again
            PlayerMovement.playerControlsEnabled = true;
        }
    }


}
