using UnityEngine;
using System.Collections;

public class EffectGenerator : MonoBehaviour
{

    static EffectGenerator instance;
    public static EffectGenerator EffectGeneratorIns()
    {
        if (!instance)
            instance = GameObject.FindObjectOfType(typeof(EffectGenerator)) as EffectGenerator;
        return instance;
    }
    enum EffectList
    {
        Dust,
        Count_EffList
    };
    public GameObject[] EffectPrefab;//문법상 리ㅡ
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject CreateEffect(string  ELName, Vector2 Pos, Vector3 Angle,float LifeTime)
    {
        GameObject Temp;
       // Debug.Log(Pos.x + " d " + Pos.y);
        switch (ELName)
        {
            case "Effect_Dust":
                Temp = Instantiate(EffectPrefab[(int)EffectList.Dust], Pos, Quaternion.Euler(Angle)) as GameObject;
                //애니스타일은 추가하기                
                break;            
            default://요청한 말풍선이 없을경우
                Temp = Instantiate(EffectPrefab[(int)EffectList.Dust], Pos, Quaternion.Euler(Angle)) as GameObject;
                break;
        }
        Temp.GetComponent<Effect_Default>().LiveTime = LifeTime;
        Temp.gameObject.SetActive(true);
        return Temp;
    }
}
