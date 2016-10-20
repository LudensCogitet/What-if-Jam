using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

    enum movement {none,up,down};


    Rigidbody2D myBody;
    movement moving;
    public Vector2 direction;
    public Vector2 savedPos;
    public float speed = 0.1f;
    public float waitTime = 3f;
    public bool beefyOn = false;
    public BeefyLeg beefy;

    public BoxCollider2D leftElevator;
    public BoxCollider2D rightElevator;

	// Use this for initialization
	void Start () {
        beefy = FindObjectOfType<BeefyLeg>();
        moving = movement.up;
        direction = Vector2.up * speed;
        myBody = GetComponent<Rigidbody2D>();
        savedPos = myBody.position;

        leftElevator.enabled = true;
        rightElevator.enabled = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (moving != movement.none)
        {
            myBody.MovePosition(myBody.position + direction);
        }
        else
        {
            myBody.MovePosition(savedPos);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("BINK!");
        if (col.gameObject.CompareTag("Stop") || (col.gameObject.CompareTag("Player") && beefyOn == false))
        {
            if (moving == movement.up)
            {
                leftElevator.enabled = false;
                rightElevator.enabled = true;
                Invoke("MoveDown", waitTime);
            }
            else if (moving == movement.down)
            {
                leftElevator.enabled = true;
                rightElevator.enabled = false;
                Invoke("MoveUp", waitTime);
            }

            if (beefyOn)
            {
                beefy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            moving = movement.none;
            savedPos = myBody.position;
        }
    }

    void MoveDown()
    {
        leftElevator.enabled = true;
        rightElevator.enabled = true;
        moving = movement.down;
        direction = Vector3.down * speed;
    }

    void MoveUp()
    {
        leftElevator.enabled = true;
        rightElevator.enabled = true;
        moving = movement.up;
        direction = Vector3.up * speed;
    }
}
