using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinTrigger : MonoBehaviour {

    public float winTime = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        BeefyLeg beefy = col.gameObject.GetComponentInParent<BeefyLeg>();
        if (beefy)
        {
            if (beefy.onGround > 0)
            {
                Invoke("Win", winTime);
            }
        }
    }
    void onTriggerExit2D(Collider2D col)
    {
        BeefyLeg beefy = col.gameObject.GetComponentInParent<BeefyLeg>();
        if (beefy)
        {
            CancelInvoke("Win");
        }
    }

    void Win()
    {
        LevelEndManager.nextLevel = LevelEndManager.currentLevel+1;
        SceneManager.LoadScene("LevelEnd");
    }
}
