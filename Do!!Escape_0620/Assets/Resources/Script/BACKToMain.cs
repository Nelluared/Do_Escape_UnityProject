using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class BACKToMain : MonoBehaviour {
    public GameBase Base;
    public bool GAG;
	// Use this for initialization
	void Start () {
	
	}
    void Enable()
    {
        GAG = false;
    }
   public  void BackMain()
    {
        SoundsGenerator.SoundsGeneratorIns().PlaySound("BackToMain", new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0));
        SceneManager.LoadScene("Oppening");
    }
    void OnClick()
    {
        SceneManager.LoadScene("Oppening");
    }
    public void OptionClose()
    {
        Base.OptionExt = true;
        GAG = true;


    }

}
