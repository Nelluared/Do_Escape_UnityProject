using UnityEngine;
using System.Collections;
public class GameOption : MonoBehaviour
{
    bool menu = true;
    bool options = false;
    bool sound = false;
    bool video = false;

    float sfxVol = 6;
    float musicVol = 6;

    float fieldOfView = 80;

    //GUI.Botton 에 자신이 선택할 버튼을 입력.

    void OnGUI()
    {

        if (menu)
        {


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), "Start Game"))
            {


                // play game
            }


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), "Options"))
            {


                menu = false;
                options = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 90, 100, 30), "Quit"))
            {


                Application.Quit();
            }
        }


        if (options)
        {


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), "Audio Settings"))
            {


                options = false;
                sound = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), "Video Settings"))
            {


                options = false;
                video = true;
            }


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 90, 100, 30), "Back"))
            {


                options = false;
                menu = true;
            }
        }


        if (sound)
        {

            sfxVol = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50, 
                Screen.height / 2, 100, 30), sfxVol, (float)0.0, (float)10.0);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110, Screen.height / 2 - 5, 100, 30), "SFX: " + sfxVol);

            musicVol = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), musicVol, (float)0.0, (float)10.0);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110, Screen.height / 2 + 25, 100, 30), "Music: " + musicVol);

            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 90, 100, 30), "Back"))
            {


                sound = false;
                options = true;
            }
        }


        if (video)
        {

            string[] qualities = QualitySettings.names;

            GUILayout.BeginVertical();

            for (int i = 0; i < qualities.Length; i++)
            {


                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 120 + i * 30, 100, 30), qualities[i]))
                {


                    QualitySettings.SetQualityLevel(i, true);
                }
            }

            GUILayout.EndVertical();


            fieldOfView = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 150, 100, 20), fieldOfView, 30, 120);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110, Screen.height / 2 - 155, 100, 30), "FOV: " + fieldOfView);


            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 90, 100, 30), "Back"))
            {


                video = false;
                options = true;
            }
        }
    }
}