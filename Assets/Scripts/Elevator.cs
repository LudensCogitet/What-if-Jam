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

	// Use this for initialization
	void Start () {
        moving = movement.up;
        direction = Vector2.up * speed;
        myBody = GetComponent<Rigidbody2D>();
        savedPos = myBody.position;
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
                Invoke("MoveDown", waitTime);
            }
            else if (moving == movement.down)
            {
                Invoke("MoveUp", waitTime);
            }
            moving = movement.none;
            savedPos = myBody.position;
        }
    }

    void MoveDown()
    {
        moving = movement.down;
        direction = Vector3.down * speed;
    }

    void MoveUp()
    {
        moving = movement.up;
        direction = Vector3.up * speed;
    }
}
