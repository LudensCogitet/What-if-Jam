using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelEndManager : MonoBehaviour {

    public static int currentLevel;
    public static int nextLevel;

    public Text[] winner;
    public Text[] dead;
	// Use this for initialization
	void Start () {
        if (winner != null && dead != null)
        {
            if (currentLevel == nextLevel)
            {
                for (int i = 0; i < winner.Length; i++)
                {
                    winner[i].enabled = false;
                }
                for (int i = 0; i < dead.Length; i++)
                {
                    dead[i].enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < winner.Length; i++)
                {
                    winner[i].enabled = true;
                }
                for (int i = 0; i < dead.Length; i++)
                {
                    dead[i].enabled = false;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(nextLevel);
        }
	}
}
