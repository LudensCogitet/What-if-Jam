using UnityEngine;
using System.Collections;

public class LeftArm : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.Find("RightArm").GetComponent<Collider2D>());

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
