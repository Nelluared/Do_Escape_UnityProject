using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
public class GameBase : MonoBehaviour {
    public string Scene_Stage_1;
    public bool PlayerEnableControl=false;
    public bool CraneEnableControl = false;
    public bool OptionExt = false;
    public GameObject BugFix_joystick;
    public GameObject CraneModes;
    public BACKToMain AAA;
    public bool GameClear = false;
    bool CharaModeSetting_Block = false;
    bool OpIn;
    // Use this for initialization
    public enum GameState
    {
        ReadyMode=(int)0,
        PlayerMode,
        CraneMode,
        PauseMode,
        ClearMode,
        GhostMode,//암것도 아닌 널모드
        _HowManyState
    }
    public static GameState lastGameState;
    public static GameState nowGameState;
    public static bool UIUsing=false;
    public GameState StartGameState;
    public GameState testGameState;
    void Start () {
        BugFix_joystick.SetActive( false);
         Screen.SetResolution(1280, 720, true);
        lastGameState = StartGameState;
        nowGameState = StartGameState;
        BugFix_joystick.SetActive(true);
        StateRun();OpIn = false;
        
    }
    void InputState()//외부에서 변화시키는 상태를 수용
    {
        if (CrossPlatformInputManager.GetButton("EndCraneMode"))
        {
            lastGameState = nowGameState;
            nowGameState = GameState.PlayerMode;
            StateRun();
        }
        else if (CrossPlatformInputManager.GetButton("ResetButton"))
        {
            SceneManager.LoadScene(Scene_Stage_1);
        }
        else if (CrossPlatformInputManager.GetButton("OptionButton"))
        {
            Debug.Log("adadsasss");
            Time.timeScale = 0;
            if (OpIn == false)
            {
                OpIn = true;
                lastGameState = nowGameState;
            }
            nowGameState = GameState.PauseMode;
            StateRun();
        }
        else if (CrossPlatformInputManager.GetButton("OptionExitButton")||OptionExt)
        {
            OpIn = true;
            Debug.Log("dadadad");
            OptionExt = false;
            Time.timeScale = 1;
            nowGameState = lastGameState;
            lastGameState = GameState.PauseMode;
            StateRun();
        }

         if (GameClear)
        {
            Time.timeScale = 1;
            lastGameState = nowGameState;
            nowGameState = GameState.ClearMode;           
            StateRun();        
        }
    }
    // Update is called once per frame
    void Update () {
       
        if (Input.GetButton("Cancel"))//나가기
            Application.Quit();
        InputState();       
        //nowGameState = testGameState;
    }
    void StateRun()//현재 게임 상태에 따라 처리를 해준다.
    {
        switch (nowGameState)
        {
            case GameState.ReadyMode:
                PlayerEnableControl = false;
                CraneEnableControl = false;
                
                break;
            case GameState.PlayerMode:
                //BGMGenerator.BGMGeneratorIns().PlayBGM("StageChoice", 0);
                PlayerEnableControl = true;
                CraneEnableControl = false;
                if (!CharaModeSetting_Block)//블록들 키네마틱 설정
                {
                    CharaModeSetting_Block = true;
                    GameObject temp = GameObject.Find("Blocks");
                    Rigidbody2D[] temprigi = temp.GetComponentsInChildren<Rigidbody2D>();
                    for (int i = 0; i < temprigi.Length; i++)
                        temprigi[i].isKinematic = true;
                }

                // Transform[] temp2 = GameObject.Find("CraneMode").GetComponentsInChildren<Transform>(); CraneModes
                //for (int i = 0; i < temp2.Length; i++)
                //{
                //    temp2[i].gameObject.SetActive(false);
                //}
                //System.Array.Clear(temp2, 0, temp2.Length);
                CraneModes.SetActive(false);
                break;
            case GameState.CraneMode:
                BGMGenerator.BGMGeneratorIns().PlayBGM("CraneMode", 0);
                CraneEnableControl = true;
                PlayerEnableControl = false;
                
                break;
            case GameState.PauseMode:
                
                PlayerEnableControl = false;
                CraneEnableControl = false;
                break;
            case GameState.ClearMode:
                PlayerEnableControl = false;
                CraneEnableControl = false;
                break;
            
        }
    }
}
