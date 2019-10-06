using UnityEngine; 
using System.Collections;

public class Move : MonoBehaviour
{
    float Target;

    public int Counter;
	void Start()
	{
        print("Thanks for buying this, if you need any support, email support@dilapidatedmeow.com. " +
            "Please note I cannot help with scripting related problems.");

        Counter = 0;
	}

	void Update()
	{
        Debug.Log(Counter);
        if (Counter < 1000)
        {
            Forward();
            Counter++;
        }

        else if (Counter >= 1000)
        {
            Reverse();
            Counter++;
        }

        else if (Counter > 2000)
        {
            Counter = 0;
        }
    }

    private void Forward()
    {
        Target += Time.deltaTime / 125;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, Target), 0.05f);
    }

    private void Reverse()
    {
        Target -= Time.deltaTime / 125;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, Target), 0.05f);
    }
}