using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player;

    public float cameraOut = 13;
    public float cameraIn = 10;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<BeefyLeg>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Camera.main.orthographicSize == cameraIn)
                Camera.main.orthographicSize = cameraOut;
            else
                Camera.main.orthographicSize = cameraIn;
        }
    }
}
