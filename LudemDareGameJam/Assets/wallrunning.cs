using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class wallrunning : MonoBehaviour
{
    public Rigidbody rb;
    public float forceVec = 1f;
    public Vector3 m_NewForce = new Vector3 (0, 1, 0);
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_NewForce = new Vector3(0, forceVec, 0);

        
    }

    private void OnTriggerStay(Collider other)
    {
        Player.gameObject.GetComponent<FirstPersonController>().m_MoveDir.y -= forceVec * Time.deltaTime;
        Debug.Log("something happens");
    }
}
