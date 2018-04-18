using UnityEngine;
using System.Collections;

public class BGMGenerator : MonoBehaviour
{
    public float Bolum;
    bool MusicChange = false;
    static BGMGenerator instance;
    public static BGMGenerator BGMGeneratorIns()
    {
        if (!instance)
            instance = GameObject.FindObjectOfType(typeof(BGMGenerator)) as BGMGenerator;
        return instance;
    }
    public enum BGMlist
    {
        Title,
        StageChoice,
        StoryCut,
        CharacterMode,
        CraneMode,
        HowManyBGM
    };
    public GameObject[] Audioresource;
    AudioSource PlayingBGM;
    BGMlist PlayingBGMList;
    BGMlist ChoiceBGMList;
    float SwapMaxTime = 0;
    float NowSwapTime = 0;
    // Use this for initialization
    void Start()
    {
        Bolum = 100;
        
        PlayingBGM = null;
        PlayingBGMList = BGMlist.HowManyBGM;
        ChoiceBGMList = BGMlist.HowManyBGM;
      

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayingBGM!= null)
        PlayingBGM.volume = Bolum;
    }
    void ChangeBGM(BGMlist List)
    {
        Debug.Log("CC");
        if (PlayingBGM != null)
        {

            //ChoiceBGMList = BGMlist.HowManyBGM;
            Audioresource[(int)PlayingBGMList].gameObject.SetActive(false);
            PlayingBGM.gameObject.SetActive(false);
            PlayingBGM.Stop();
            PlayingBGM.mute = true;
            Audioresource[(int)PlayingBGMList].GetComponent<AudioSource>().mute = true;
            Audioresource[(int)PlayingBGMList].GetComponent<AudioSource>().mute = true;
            Audioresource[(int)PlayingBGMList].GetComponent<AudioSource>().mute = true;
            Debug.Log("TTTTTT");
            PlayingBGM.gameObject.SetActive(true);
            Audioresource[(int)List].gameObject.SetActive(true);
            PlayingBGM = Audioresource[(int)List].GetComponent<AudioSource>();
            PlayingBGM.volume = (float)(Bolum / 100);
            PlayingBGM.Play();
            PlayingBGM.mute = false;
            Audioresource[(int)List].GetComponent<AudioSource>().mute = false;
            PlayingBGMList = List;

        }
        else
        {
            PlayingBGM = Audioresource[(int)List].GetComponent<AudioSource>();
            PlayingBGM.volume = (float)(Bolum / 100);
            PlayingBGM.Play();
            PlayingBGMList = List;
        }
    }
    public void PlayBGM(string BgmName, float SwapTime)
    {
       
        
            switch (BgmName)
            {
                case "Title":
                     if (PlayingBGMList != BGMlist.Title)
                    ChangeBGM(BGMlist.Title);
                    break;
                case "StageChoice":
                if (PlayingBGMList != BGMlist.StageChoice)
                    ChangeBGM(BGMlist.StageChoice);
                    break;
                case "StoryCut":
                if (PlayingBGMList != BGMlist.StoryCut)
                    ChangeBGM(BGMlist.StoryCut);
                    break;
                case "CharacterMode":
                if (PlayingBGMList != BGMlist.CharacterMode)
                    ChangeBGM(BGMlist.CharacterMode);
                    break;
                case "CraneMode":
                if (PlayingBGMList != BGMlist.CraneMode)
                    ChangeBGM(BGMlist.CraneMode);
                    break;
            }
        



    }

}
