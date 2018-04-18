using UnityEngine;
using System.Collections;

public class CraneUI : MonoBehaviour {

    GameObject Control_CraneUI;
    GameObject Control_PlayerUI;
    // Use this for initialization
    void Start () {
        Control_CraneUI= GameObject.Find("CraneUI")as GameObject;
        Control_PlayerUI = GameObject.Find("PlayerUI") as GameObject; ;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameBase.nowGameState == GameBase.GameState.PlayerMode)
        {
            Control_CraneUI.SetActive(false);
            Control_PlayerUI.SetActive(true);
        }
        else if (GameBase.nowGameState == GameBase.GameState.CraneMode)
        {
            Control_CraneUI.SetActive(true);
            Control_PlayerUI.SetActive(false);
        }       

    }
}
