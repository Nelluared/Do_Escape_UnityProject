using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


namespace UnityStandardAssets.CrossPlatformInput
{
    [ExecuteInEditMode]
    public class MobileControlRig : MonoBehaviour
    {
        // this script enables or disables the child objects of a control rig
        // depending on whether the USE_MOBILE_INPUT define is declared.

        // This define is set or unset by a menu item that is included with
        // the Cross Platform Input package.

        public GameObject CraneUI_DropButton;//각각 유아이 컨트롤 용
        public GameObject CraneUI_EndButton;
        public GameObject CharacterUI_JumpButton;
        public GameObject BlockListUI_JumpButton;
        public GameObject OptionUIFrame;
        public GameObject ClearUIFrame;
        public GameObject JoysticFrame;
        public GameObject Joystic;
        public GameObject ResetFrame;
        public GameObject OptionFrame;
#if !UNITY_EDITOR
	void OnEnable()
	{
		CheckEnableControlRig();
	}

        void Update()
        {
        CheckEnableControlRig();
        }
#endif

        private void Start()
        {
#if UNITY_EDITOR
            if (Application.isPlaying) //if in the editor, need to check if we are playing, as start is also called just after exiting play
#endif
            {
                UnityEngine.EventSystems.EventSystem system = GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();

                if (system == null)
                {//the scene have no event system, spawn one
                    GameObject o = new GameObject("EventSystem");

                    o.AddComponent<UnityEngine.EventSystems.EventSystem>();
                    o.GetComponent<UnityEngine.EventSystems.EventSystem>();
                    o.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
                    o.GetComponent<UnityEngine.EventSystems.StandaloneInputModule>();
                    //CraneUI_DropButton = GameObject.Find("JumpButton_Crane") as GameObject;
                    // CraneUI_EndButton = GameObject.Find("EndButton_Crane") as GameObject;
                    //CharacterUI_JumpButton = GameObject.Find("JumpButton_Chara") as GameObject;
                }
            }
        }

#if UNITY_EDITOR

        private void OnEnable()
        {
            EditorUserBuildSettings.activeBuildTargetChanged += Update;
            EditorApplication.update += Update;
        }


        private void OnDisable()
        {
            EditorUserBuildSettings.activeBuildTargetChanged -= Update;
            EditorApplication.update -= Update;
        }


        private void Update()
        {
            CheckEnableControlRig();
        }
#endif


        private void CheckEnableControlRig()
        {
#if MOBILE_INPUT
            EnableControlRig(true);
#else
            EnableControlRig(false);
#endif
        }


        private void EnableControlRig(bool enabled)
        {
            foreach (Transform t in transform)
            {
                //  t.gameObject.SetActive(enabled);
            }

            if (GameBase.nowGameState == GameBase.GameState.PlayerMode)
            {
                if (CharacterUI_JumpButton != null)
                    CharacterUI_JumpButton.SetActive(true);
                if (JoysticFrame != null)
                    JoysticFrame.SetActive(true);
                if (Joystic != null)
                    Joystic.SetActive(true);
                if (ResetFrame != null)
                    ResetFrame.SetActive(true);
                if (OptionFrame != null)
                    OptionFrame.SetActive(true);
            }
            else
            {
                if (CharacterUI_JumpButton != null)
                    CharacterUI_JumpButton.SetActive(false);
            }
            if (GameBase.nowGameState == GameBase.GameState.CraneMode)
            {
                Debug.Log("Crane");
                if (CraneUI_DropButton != null)
                    CraneUI_DropButton.SetActive(true);
                if (CraneUI_EndButton != null)
                    CraneUI_EndButton.SetActive(true);
                if (BlockListUI_JumpButton != null)
                    BlockListUI_JumpButton.SetActive(true);
                if (JoysticFrame != null)
                    JoysticFrame.SetActive(true);
                if (Joystic != null)
                    Joystic.SetActive(true);
                if (ResetFrame != null)
                    ResetFrame.SetActive(true);
                if (OptionFrame != null)
                    OptionFrame.SetActive(true);
            }
            else
            {
                if (CraneUI_DropButton != null)
                    CraneUI_DropButton.SetActive(false);
                if (CraneUI_EndButton != null)
                    CraneUI_EndButton.SetActive(false);
                if (BlockListUI_JumpButton != null)
                    BlockListUI_JumpButton.SetActive(false);
            }

            if (GameBase.nowGameState == GameBase.GameState.PauseMode)
            {
                Debug.Log("Pause");
                if (OptionUIFrame != null)
                    OptionUIFrame.SetActive(true);
            }
            else
            {
                Debug.Log("Down");
                if (OptionUIFrame != null)
                    OptionUIFrame.SetActive(false);
            }
            if (GameBase.nowGameState == GameBase.GameState.ClearMode)
            {
                if (CharacterUI_JumpButton != null)
                    CharacterUI_JumpButton.SetActive(false);
                if (ClearUIFrame != null)
                    ClearUIFrame.SetActive(true);

                if (JoysticFrame != null)
                    JoysticFrame.SetActive(false);
                if (Joystic != null)
                    Joystic.SetActive(false);
                if (ResetFrame != null)
                    ResetFrame.SetActive(false);
                if (OptionFrame != null)
                    OptionFrame.SetActive(false);               
            }
            else
                if (ClearUIFrame != null)
                ClearUIFrame.SetActive(false);

        }
    }
}
