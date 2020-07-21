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
    float time;
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        time = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            GameObject ball =  Instantiate(myPrefab, ShotSpawn.position, transform.rotation);
            time = fireRate;
        }
       
    }
}
