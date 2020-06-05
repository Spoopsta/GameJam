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
    [SerializeField] float Delay;
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > Delay)
        {
            Delay = Time.time + fireRate;
            Instantiate(myPrefab, ShotSpawn.position, ShotSpawn.rotation);
        }
       
    }
}
