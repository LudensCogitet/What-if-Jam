using UnityEngine;
using System.Collections;

public class HeadSpace : MonoBehaviour {

    public BeefyLeg player;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        player.hitHead = true;
    }
}
