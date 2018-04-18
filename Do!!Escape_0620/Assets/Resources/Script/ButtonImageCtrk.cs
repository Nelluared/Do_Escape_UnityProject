using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
public class ButtonImageCtrk : MonoBehaviour {
    public Sprite ButtonImage_Down;
    public Sprite ButtonImage_Up;
    UnityEngine.UI.Image UsingImgae;
    public string ButtonName;
    bool ButtonPushed = false;
    // Use this for initialization
    void Start () {        
        UsingImgae = gameObject.GetComponent<UnityEngine.UI.Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown(ButtonName)&& ButtonPushed != true)
        {
            UsingImgae.sprite = ButtonImage_Down;
            ButtonPushed = true;
        }
        else if(CrossPlatformInputManager.GetButtonUp(ButtonName)&&ButtonPushed != false)
        {
            ButtonPushed = false;
            UsingImgae.sprite = ButtonImage_Up;
        }
	}
}
