using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelEndManager : MonoBehaviour {

    public static int currentLevel;
    public static int nextLevel;

    public Text[] winner;
    public Text[] dead;
    public Text loading;
	// Use this for initialization
	void Start () {
        if(MusicPlayer.instance.GetComponent<AudioSource>().isPlaying == false)
        {
            MusicPlayer.instance.GetComponent<AudioSource>().Play();
        }

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
                GameObject.Find("EEEUG").GetComponent<AudioSource>().Play();
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
                GameObject.Find("Yay").GetComponent<AudioSource>().Play();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            loading.enabled = true;
            SceneManager.LoadScene(nextLevel);
        }
	}
}
