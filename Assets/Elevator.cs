using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

    enum movement {none,up,down};

    movement moving;
    public Vector3 direction;
    public float speed = 10;
    public float waitTime = 3f;

	// Use this for initialization
	void Start () {
        moving = movement.up;
        direction = Vector3.up * speed; 
	}
	
	// Update is called once per frame
	void Update () {
        if (moving != movement.none)
            transform.position += direction;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Boom");
        if (col.gameObject.CompareTag("Stop"))
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
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<BeefyLeg>().transform.parent = transform;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<BeefyLeg>().transform.parent = null;
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
