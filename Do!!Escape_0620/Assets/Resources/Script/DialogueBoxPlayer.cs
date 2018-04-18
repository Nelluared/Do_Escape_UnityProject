using UnityEngine;
using System.Collections;

public  class DialogueBoxPlayer : MonoBehaviour {
     static DialogueBoxPlayer instance;
    public static DialogueBoxPlayer DialogueBoxPlayerIns()
    {
        if (!instance)        
            instance = GameObject.FindObjectOfType(typeof(DialogueBoxPlayer))as DialogueBoxPlayer;
        return instance;
    }
    public DialogueEffect Base;//말풍선 디폴트 베이스 프리팹
    public Sprite Image_EligateClose;
    public Sprite Image_EligateOpen;
    public Sprite Image_BlowfishBlow;
    public Sprite Image_UrchinStick;
    public Sprite Image_SnailRolling;
    public Sprite Image_SpiderShot;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public DialogueEffect CreateDialogue(string DlName, Vector2 Pos, float LifeTime, float Anistyle)
    {
        DialogueEffect Temp;
        Temp = Instantiate(Base, Pos, Quaternion.identity) as DialogueEffect;
        Temp.LifeTime = LifeTime;
        Debug.Log("afaf");
        switch (DlName)
        {
            case "Dl_EligateClose":                              
                Temp.DialogueImage = Image_EligateClose;
                //애니스타일은 추가하기
                Debug.Log("afdddaf");
                break;
            case "Dl_EligateOpen":               
                Temp.DialogueImage = Image_EligateOpen;              
                //애니스타일은 추가하기
                break;
            case "Dl_BlowfishBlow":
                Temp.DialogueImage = Image_BlowfishBlow;
                //애니스타일은 추가하기
                break;
            case "Dl_UrchinStick":
                Temp.DialogueImage = Image_UrchinStick;
                //애니스타일은 추가하기
                break;
            case "Dl_SnailRolling":
                Temp.DialogueImage = Image_SnailRolling;
                //애니스타일은 추가하기
                break;
            case "Dl_SpiderShot":
                Temp.DialogueImage = Image_SpiderShot;
                //애니스타일은 추가하기
                break;
            default://요청한 말풍선이 없을경우
                Temp.DialogueImage = Image_EligateOpen;
                break;
        }
        Temp.gameObject.SetActive(true);
        return Temp;
    } 
}
