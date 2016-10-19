using UnityEngine;
using System.Collections;

public class ElevatorPhysics : MonoBehaviour {

    ConstantForce2D myForce;
    int partsTally = 0;

	// Use this for initialization
	void Start () {
        myForce = GetComponent<ConstantForce2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Boom");
        if (col.gameObject.CompareTag("Stop"))
        {
            myForce.force = -myForce.force;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            partsTally++;
            FindObjectOfType<BeefyLeg>().transform.parent = transform;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            partsTally--;
            if(partsTally <= 0)
                FindObjectOfType<BeefyLeg>().transform.parent = null;
        }
    }
}
