using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class JumpPad : MonoBehaviour
{
    private FirstPersonController fpc;

    public GameObject Player;


    public float jumpPadBoost;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player && this.gameObject.tag.Equals("JUMP"))
        {
            Player.gameObject.GetComponent<FirstPersonController>().m_MoveDir.y = jumpPadBoost;
        }
    }
}
