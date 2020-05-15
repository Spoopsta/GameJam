using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool isFallingPlatform = false;
    float downSpeed = 0;
    public GameObject Player;

    public float waitTime;

    public float fallingSpeed;

    
    // public GameObject spawnPosition;

    private float startPosX;
    private float startPosY;
    private float startPosZ;

    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        startPosZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFallingPlatform)
        {
            downSpeed += Time.deltaTime / fallingSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == Player)
        {  
            coroutine = FallingDelay();
            StartCoroutine(coroutine);
           // isFallingPlatform = true;
            Player.transform.parent = transform;
        }

        if (other.gameObject.tag.Equals("Boundary") || other.gameObject.tag.Equals("Wall"))
        {
            Debug.Log("OUICH");
            isFallingPlatform = false;
            StopCoroutine(coroutine);
            transform.position = new Vector3(startPosX, startPosY, startPosZ);
            downSpeed = 0;
            // gameObject.transform.position = spawnPosition.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.transform.parent = null;
    }

    private IEnumerator FallingDelay()
    {
        while (true)
        {

            yield return new WaitForSeconds(waitTime);
            isFallingPlatform = true;
        }
    }
}
