using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ResetButton : MonoBehaviour {
    public string Scene_Stage_1;
	// Use this for initialization
	void Start () {
	
	}
    public void ResetStage()
    {
        SceneManager.LoadScene(Scene_Stage_1);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
