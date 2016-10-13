using UnityEngine;
//using UnityEditor;
using System.Collections;

public class Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject BeefyLeg = GameObject.Find("BeefyLeg");

            Debug.Log(BeefyLeg);
            

            //PrefabUtility.CreatePrefab(FileUtil.GetProjectRelativePath(EditorUtility.SaveFilePanel("Save Prefab", Application.dataPath, "beefy", "prefab")), BeefyLeg);
        }
	}
}
