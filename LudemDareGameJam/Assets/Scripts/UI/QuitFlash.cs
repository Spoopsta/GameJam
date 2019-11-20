using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class QuitFlash : MonoBehaviour
{

    private TextMeshProUGUI flashingQuitText;


    // Start is called before the first frame update
   private void Start()
    {
        //get text component
        flashingQuitText = GetComponent<TextMeshProUGUI>();

        //start the coroutine for the blinking text at the start
        StartCoroutine(BlinkQuitText());

    }

    //method to blink the text
    public IEnumerator BlinkQuitText()
    {
        //Blink it forever. you can set a terminating condition event if need be some other time. (We dont)
        while (true)
        {
            //set text to blank
            flashingQuitText.text = " ";

            //display blank text for 0.5 seconds (can change if need be)
            yield return new WaitForSeconds(0.5f);

            //display "Press "space" to continue" for the next 0.5 seconds
            flashingQuitText.text = "Press 'q' to continue";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
