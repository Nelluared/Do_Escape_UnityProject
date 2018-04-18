using UnityEngine;
using System.Collections;

public class SoundsGenerator : MonoBehaviour {

    public float Bolum = 100;
    bool MusicChange = false;
    static SoundsGenerator instance;
    public static SoundsGenerator SoundsGeneratorIns()
    {
        if (!instance)
            instance = GameObject.FindObjectOfType(typeof(SoundsGenerator)) as SoundsGenerator;
        return instance;
    }
    public enum SElist
    {
        OptionButtonDown,
        ScrollSlide,
        BackToMain,
        TitleEnter,
        StageChoice,
        CraneModeStart,
        CharacterModeStart,
        CraneButtonDown,
        CraneActive,
        CraneMove,
        PnCraneAction,
        DollCrash,
        CharacterJump,
        WebShot,
        EligateBite,
        MomboBigger,
        UrchinDrag,
        SnailHide,
        SlimeBounce,
        ClearSound,
        Holefalling,
        HowManySE
    };
    public AudioClip[] Audioresource;
    public AudioClip TesetClip;
  
    // Use this for initialization
    void Start()
    {
     
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void PlaySE(SElist List,Vector3 position)
    {

        //  PlaySESource.PlayOneShot(Audioresource[(int)List], Bolum);
        //PlaySESource.PlayOneShot();
        AudioSource.PlayClipAtPoint(Audioresource[(int)List], position, Bolum);
    }
    public void PlaySound(string BgmName, Vector3 position)
    {
        switch (BgmName)
        {
            case "OptionButtonDown":
                PlaySE(SElist.OptionButtonDown, position);
                break;
            case "ScrollSlide":
                PlaySE(SElist.ScrollSlide, position);
                break;
            case "BackToMain":
                PlaySE(SElist.BackToMain, position);
                break;
            case "TitleEnter":
                PlaySE(SElist.TitleEnter, position);
                break;
            case "StageChoice":
                PlaySE(SElist.StageChoice, position);
                break;
            case "CraneModeStart":
                PlaySE(SElist.CraneModeStart, position);
                break;
            case "CharacterModeStart":
                PlaySE(SElist.CharacterModeStart, position);
                break;
            case "CraneButtonDown":
                PlaySE(SElist.CraneButtonDown, position);
                break;
            case "CraneActive":
                PlaySE(SElist.CraneActive, position);
                break;
            case "CraneMove":
                PlaySE(SElist.CraneMove, position);
                break;
            case "PnCraneAction":
                PlaySE(SElist.PnCraneAction, position);
                break;
            case "DollCrash":
                PlaySE(SElist.DollCrash, position);
                break;
            case "CharacterJump":
                PlaySE(SElist.CharacterJump, position);
                break;
            case "WebShot":
                PlaySE(SElist.WebShot, position);
                break;
            case "EligateBite":
                PlaySE(SElist.EligateBite, position);
                break;
            case "MomboBigger":
                PlaySE(SElist.MomboBigger, position);
                break;
            case "UrchinDrag":
                PlaySE(SElist.UrchinDrag, position);
                break;
            case "SnailHide":
                PlaySE(SElist.SnailHide, position);
                break;
            case "SlimeBounce":
                PlaySE(SElist.SlimeBounce, position);
                break;
            case "ClearSound":
                PlaySE(SElist.ClearSound, position);
                break;
            case "Holefalling":
                PlaySE(SElist.Holefalling, position);
                break;


        }
    }
}
