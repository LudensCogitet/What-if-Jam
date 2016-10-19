using UnityEngine;
using System.Collections;

public class OnElevator : MonoBehaviour {

    int contact = 0;
    Elevator myElevator;

	// Use this for initialization
	void Start () {
        myElevator = GetComponentInParent<Elevator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(contact == 0)
            {
                FindObjectOfType<BeefyLeg>().transform.parent = transform.parent;
                myElevator.beefyOn = true;
            }
            contact++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            contact--;
            if (contact == 0)
            {
                FindObjectOfType<BeefyLeg>().transform.parent = null;
                myElevator.beefyOn = false;
            }
        }
    }
}
