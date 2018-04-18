using UnityEngine;
using System.Collections;

public class ParanomaDoll: MonoBehaviour
{
    SpriteRenderer SpRenderer;
    public GameObject BoxGenera;//제네레이터 가진 오브젝트
    BoxGenerator BoxGene;
    string[] SpriteNameArray;
    int[] SpriteIndexArray;//멀티플 말고 다 떨어뜨려놓는 방법을 추천.....
    // Use this for initialization
    public int IconNum = 0;//아이콘의 순서
    Sprite[] IconResource;//아이콘이 모인 멀티플 스프라이트에서 추출한 스프라이트
    void CreateSpriteNameArray()
    {
        //SpriteNameArray = new string[(int)BoxGenerator.BlockType.Count_BlockType];
        //SpriteNameArray[(int)BoxGenerator.BlockType.Eligate] = "Eligate_Dummy";
        //SpriteNameArray[(int)BoxGenerator.BlockType.Mombo] = "Mombo_Dummy";
        //SpriteNameArray[(int)BoxGenerator.BlockType.Sheep] = "Sheep_Dummy";
        //SpriteNameArray[(int)BoxGenerator.BlockType.Slime] = "Slime_Dummy";
        //SpriteNameArray[(int)BoxGenerator.BlockType.Snail] = "Snail_Dummy";
        //SpriteNameArray[(int)BoxGenerator.BlockType.Spider] = "Spider_Dummy";
        //SpriteNameArray[(int)BoxGenerator.BlockType.Urchin_Close] = "Urchincl_Dummy";
        //SpriteNameArray[(int)BoxGenerator.BlockType.Urchin_Open] = "Urchinop_Dummy";
        //SpriteNameArray[(int)BoxGenerator.BlockType.Woo] = "Block_WOO";

        SpriteIndexArray = new int[(int)BoxGenerator.BlockType.Count_BlockType];
        SpriteIndexArray[(int)BoxGenerator.BlockType.Eligate] = 3;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Mombo] = 4;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Sheep] = 11;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Slime] = 2;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Snail] = 1;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Spider] = 0;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Urchin_Close] = 8;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Urchin_Open] = 9;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Flower] = 7;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Bear] = 10;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Snake] = 6;
        SpriteIndexArray[(int)BoxGenerator.BlockType.Turtle] = 5;
    }
    void Start()
    {
        SpRenderer = gameObject.GetComponent<SpriteRenderer>();
      
        BoxGene = BoxGenera.GetComponent<BoxGenerator>();
        CreateSpriteNameArray();
        IconResource = Resources.LoadAll<Sprite>("Texture/Characters/Icon");

        //if (BoxGene.CreateList.Count> IconNum)
        //SpRenderer.sprite = Resources.Load(SpriteNameArray[(int)BoxGene.CreateList[IconNum]])as Sprite;

        if (BoxGene.CreateList.Count > IconNum)
            SpRenderer.sprite = IconResource[SpriteIndexArray[(int)BoxGene.CreateList[IconNum]]];
    }

    // Update is called once per frame
    void Update()
    {
        if (BoxGene.CreateList.Count > IconNum)
        {

            SpRenderer.sprite = IconResource[SpriteIndexArray[(int)BoxGene.CreateList[IconNum]]];
            // Debug.Log(SpRenderer.sprite.name);
        }
        else
        {
            SpRenderer.sprite = null;
        }

    }
}
