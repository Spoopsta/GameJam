using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FlashingText : MonoBehaviour
{

    private TextMeshProUGUI flashingText;


    // Start is called before the first frame update
    void Start()
    {
        //get text component
        flashingText = GetComponent<TextMeshProUGUI>();

        //start the coroutine for the blinking text at the start
        StartCoroutine(BlinkText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method to blink the text
    public IEnumerator BlinkText()
    {
        //Blink it forever. you can set a terminating condition event if need be some other time. (We dont)
        while (true)
        {
            //set text to blank
            //flashingText.text = " ";

            //display blank text for 0.5 seconds (can change if need be)
            yield return new WaitForSeconds(0.5f);

            //display "Press "space" to continue" for the next 0.5 seconds
            flashingText.text = "Press 'space' to continue";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
