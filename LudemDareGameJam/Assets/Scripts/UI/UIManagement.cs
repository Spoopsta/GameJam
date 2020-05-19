using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
public class UIManagement : MonoBehaviour
{
    public TextMeshProUGUI dashText, jumpText;

    public FirstPersonController fpc;

    // Start is called before the first frame update
  /*  void Start()
    {
        dashText.text = "1";
        jumpText.text = "1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Code needed to change the text
    /// </summary>
    private void UIResets()
    {
        if (fpc.GetComponent<FirstPersonController>().jumpTextCounter == 0)
        {
            jumpText.text = "1";
            Debug.Log("text change jump");
        }

        if (fpc.GetComponent<FirstPersonController>().jumpTextCounter == 1)
        {
            jumpText.text = "0";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            jumpText.text = "1";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        jumpText.text = "0";
    }
    */
}
