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
    void Start()
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
        if (fpc.GetComponent<FirstPersonController>().m_CharacterController.isGrounded == true)
        {
            jumpText.text = "1";
            Debug.Log("text change jump");
        }

        if (fpc.GetComponent<FirstPersonController>().m_CharacterController.isGrounded == false)
        {
            jumpText.text = "0";
        }
    }
}
