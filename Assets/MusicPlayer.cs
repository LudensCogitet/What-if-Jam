using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    public static MusicPlayer instance;

    // Use this for initialization
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
