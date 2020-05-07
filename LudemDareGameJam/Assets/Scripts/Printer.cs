using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    public GameObject myPrefab;
    public Transform ShotSpawn;

    private Quaternion rotation;
    private int count;
    private bool done;
    [SerializeField] int Delay;

    // Start is called before the first frame update
    void Start()
    {
        
        if (Delay == 0)
        {
            Instantiate(myPrefab, ShotSpawn.position, ShotSpawn.rotation);
            done = true;
        }
        else {
            count = 0;
            done = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {


        if (count > Delay && !done)
        {
            Instantiate(myPrefab, ShotSpawn.position, ShotSpawn.rotation);
            done = true;
        }
        else {
            count++;
        }

       
    }
}
