using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{

    public Texture2D fadetexture;  //화면에 보여질 텍스쳐

    public float ChangeTime;
    public GameObject escape1;
    public GameObject escape2;
    public GameObject escape3;
    public GameObject escape4;

    public Color color;

    public AudioSource Enter;
    float fadespeed = 2.0f;
    int drawdepth = -1000;
    float alphavalue = 0.0f; //지금 상태는 밝은 상태의 화면
    float fadedir = -1;
    bool fadestart = false; //페이드 시작할 거 

    public void OnEnable()
    {
        escape1.SetActive(true);
        escape2.SetActive(false);
        escape3.SetActive(false);
        escape4.SetActive(false);
         fadespeed = 2.0f;
         drawdepth = -1000;
         alphavalue = 0.0f; //지금 상태는 밝은 상태의 화면
         fadedir = -1;
         fadestart = false; //페이드 시작할 거 
    }



    void fadein()
    {
        if (fadestart == true)
        {
            alphavalue -= fadedir * fadespeed * Time.deltaTime;
            alphavalue = Mathf.Clamp01(alphavalue);
        }
        else {   //false가 되면 아웃 시작 
            alphavalue += fadedir * fadespeed * Time.deltaTime;
            alphavalue = Mathf.Clamp01(alphavalue);
        }

    }


    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Enter.Play();
            fadestart = true;
            fadein();
            SceneManager.LoadScene("TestRoom");
           
        }
    }


    void OnGUI()
    {
      
        if (fadestart == true)
        {
            alphavalue -= fadedir * fadespeed * Time.deltaTime;
            alphavalue = Mathf.Clamp01(alphavalue);
        }
        else {
            alphavalue += fadedir * fadespeed * Time.deltaTime;
            alphavalue = Mathf.Clamp01(alphavalue);
        }

        ChangeTime += Time.deltaTime;

        if (ChangeTime >= 6.5f)
        {
            fadestart = true;
            fadein();
        }
        if (ChangeTime >= 7.0f)
        {
            escape1.SetActive(false);
            escape2.SetActive(true);
            if (ChangeTime >= 8.5f)
            {
                fadestart = false;
                fadein();
            }


        }

        if (ChangeTime >= 13.5f)
        {
            fadestart = true;
            fadein();
        }

        if (ChangeTime >= 14.0f)
        {

            escape2.SetActive(false);

            escape3.SetActive(true);
            if (ChangeTime >= 15.5f)
            {
                fadestart = false;
                fadein();
            }

        }

        if (ChangeTime >= 20.5f)
        {
            fadestart = true;
            fadein();
        }

        if (ChangeTime >= 21.0f)
        {
            escape3.SetActive(false);
            escape4.SetActive(true);
            if (ChangeTime >= 22.5f)
            {
                fadestart = false;
                fadein();
            }


        }
        if (ChangeTime >= 26.0f)
        {
            SceneManager.LoadScene("TestRoom");

        }

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alphavalue);
        GUI.depth = drawdepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadetexture);

    }
}
