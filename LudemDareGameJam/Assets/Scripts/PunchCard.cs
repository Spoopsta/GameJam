using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCard : MonoBehaviour
{
    private int upInt;
    private int downInt;
    // Start is called before the first frame update
    void Start()
    {
        upInt = 0;
        downInt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (upInt < 25)
        {
            transform.Translate(0, 0.005f, 0, Space.Self);
            upInt++;
        }
        else if (downInt < 25)
        {
            transform.Translate(0, -0.005f, 0, Space.Self);
            downInt++;
        }
        else {
            upInt = 0;
            downInt = 0;
        }
    }
}
