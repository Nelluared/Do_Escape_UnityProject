using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {

        public string Name;

        void OnEnable()
        {

        }
        void OnDisable()//눌린 키상태를 해제한다.
        {
            if (CrossPlatformInputManager.GetButton(Name))
            {

                CrossPlatformInputManager.SetButtonUp(Name);
            }
            if (CrossPlatformInputManager.GetAxis(Name) != 0)
                CrossPlatformInputManager.SetAxisZero(Name);
        }

        public void SetDownState()
        {

            //SoundsGenerator.SoundsGeneratorIns().PlaySound("OptionButtonDown", new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0));            
            CrossPlatformInputManager.SetButtonDown(Name);
            GameBase.UIUsing = true;
        }


        public void SetUpState()
        {

            CrossPlatformInputManager.SetButtonUp(Name);
            GameBase.UIUsing = false;
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {

        }
    }
}
