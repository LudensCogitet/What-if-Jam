using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<BeefyLeg>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
	}
}
