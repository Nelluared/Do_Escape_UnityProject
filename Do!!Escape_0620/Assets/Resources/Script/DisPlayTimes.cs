using UnityEngine;
using System.Collections;

public class DisPlayTimes : MonoBehaviour {
    public GameObject Number;
    public GameObject Stars;
    Sprite[] IconResource;
    Sprite[] IconResource2;
    public  BoxGenerator HowMuch;//시간정보를 가진 객체
    float UseBlocks;
    // Use this for initialization
    void Start () {
        IconResource= Resources.LoadAll<Sprite>("Texture/UI/GraphicNumber");
        IconResource2 = Resources.LoadAll<Sprite>("Texture/UI/stars 1");
    }
	
	// Update is called once per frame
	void Update () {
        UseBlocks = HowMuch.DroppedBox;
        Number.GetComponent<UnityEngine.UI.Image>().sprite = IconResource[(int)UseBlocks];
        if(UseBlocks>=5)
        Stars.GetComponent<UnityEngine.UI.Image>().sprite = IconResource2[0];
        else if(UseBlocks >= 3)
            Stars.GetComponent<UnityEngine.UI.Image>().sprite = IconResource2[1];
        else if (UseBlocks >= 1)
            Stars.GetComponent<UnityEngine.UI.Image>().sprite = IconResource2[2];
        else
            Stars.GetComponent<UnityEngine.UI.Image>().sprite = IconResource2[3];
    }
}
