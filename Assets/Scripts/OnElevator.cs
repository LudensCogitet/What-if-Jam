using UnityEngine;
using System.Collections;

public class OnElevator : MonoBehaviour {

    int contact = 0;
    Elevator myElevator;
    BeefyLeg beefy;

	// Use this for initialization
	void Start () {
        myElevator = GetComponentInParent<Elevator>();
        beefy = FindObjectOfType<BeefyLeg>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(contact == 0)
            {
                beefy.transform.parent = transform.parent;
                myElevator.beefyOn = true;
                beefy.onElevator = true;
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
                beefy.onElevator = false;
            }
        }
    }
}
