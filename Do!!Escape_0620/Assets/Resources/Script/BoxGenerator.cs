using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxGenerator : MonoBehaviour
{
     GameObject[] BlockArray;//배열로 선언할수도, 바깥에서 프리팹읽어올수도, 내용을 변경할 수도 있다.
  
    public GameObject SponePoint;//박스 생성 포인트
    public int DroppedBox;
    GameObject CurrentBlock;
    string[] BlockFileName;
    void CreateBlockNameList()//각 블록 타입마다의 프리팹이름 설정
    {
        BlockFileName =  new string [(int)BlockType.Count_BlockType];
        BlockFileName[(int)BlockType.Flower] = "Prefab/FlowerBlock";
        BlockFileName[(int)BlockType.Eligate] = "Prefab/EligateBlock";
        BlockFileName[(int)BlockType.Mombo] = "Prefab/MomboBlock";
        BlockFileName[(int)BlockType.Sheep] = "Prefab/ChikenBlock";
        BlockFileName[(int)BlockType.Slime] = "Prefab/SlimeBlock";
        BlockFileName[(int)BlockType.Snail] = "Prefab/SnailBlock";
        BlockFileName[(int)BlockType.Spider] = "Prefab/SpiderBlock";
        BlockFileName[(int)BlockType.Urchin_Close] = "Prefab/cloUrchinBlock";
        BlockFileName[(int)BlockType.Urchin_Open] = "Prefab/opUrchinBlock";
        BlockFileName[(int)BlockType.Bear] = "Prefab/BearBlock";
        BlockFileName[(int)BlockType.Snake] = "Prefab/SnakeBlock";
        BlockFileName[(int)BlockType.Turtle] = "Prefab/TurtleBlock";
      

    }
    void CreateBlockArray()
    {
        BlockArray = new GameObject[(int)BlockType.Count_BlockType];
        for (int i = 0; i < (int)BlockType.Count_BlockType; i++)
        {
            BlockArray[i] = Resources.Load(BlockFileName[i]) as GameObject;

        }
    }

    SpriteRenderer GeneratorRender;//잡고있는거 보여줄 스프라이트렌더러
    //[HideInInspector]
    public bool MakeBlock;//만드는 신호
    [HideInInspector]
    public   enum BlockType
    {
        Flower = 0,
      Eligate,
      Sheep,
      Mombo,
      Spider,
      Urchin_Close,
      Urchin_Open,
      Slime,
      Snail,
      Bear,
      Snake,
      Turtle,
      Count_BlockType
    }
    [HideInInspector]
    public    List<BlockType> CreateList;
    // Use this for initialization
    void Start () {
        CreateBlockNameList();
        CreateBlockArray();
        CreateList = new List<BlockType>();
        
        CreateList.Add(BlockType.Slime);
        CreateList.Add(BlockType.Mombo);
        CreateList.Add(BlockType.Spider);
        CreateList.Add(BlockType.Bear);        
        CreateList.Add(BlockType.Snail);
        CreateList.Add(BlockType.Eligate);

        CreateList.Add(BlockType.Flower);
        CreateList.Add(BlockType.Snake);
        CreateList.Add(BlockType.Turtle);
        
        CreateList.Add(BlockType.Sheep);
        CreateList.Add(BlockType.Mombo);
        
        CreateList.Add(BlockType.Urchin_Close);
        //CreateList.Add(BlockType.Urchin_Open);
        
        
       
        //SponePoint = GameObject.Find("GrabPoint");
        GeneratorRender = gameObject.GetComponent<SpriteRenderer>();
        CurrentBlock = null;
        MakeBlock = true;
        DroppedBox = 0;
    }
    
    void CreateAsList()
    {
        if (CreateList.Count > 0)
        {
            CreateBlock(CreateList[0]);
            CreateList.RemoveAt(0);
           // Debug.Log(CreateList.Count);
        }

    }
    public void DropBlock()//현재 블록을 떨군다
    {
        if (CurrentBlock != null)
        {

            CurrentBlock.transform.parent = GameObject.Find("Blocks").transform;
            CurrentBlock.GetComponent<Rigidbody2D>().isKinematic = false;
            CurrentBlock = null;
            DroppedBox++;
        }
    }
    void CreateBlock(BlockType type)
    {
        CurrentBlock = Instantiate(BlockArray[(int)type], gameObject.transform.position,
                   Quaternion.identity) as GameObject;
        CurrentBlock.transform.parent = gameObject.transform;
        CurrentBlock.GetComponent<Rigidbody2D>().isKinematic = true;
        //switch (type)
        //{
        //    case BlockType.Woo:
        //        CurrentBlock = Instantiate(Block_Woo, gameObject.transform.position,
        //            Quaternion.identity) as GameObject;
        //        CurrentBlock.transform.parent = gameObject.transform;
        //        CurrentBlock.GetComponent<Rigidbody2D>().isKinematic = true;
        //        break;
        //    case BlockType.Garo:
        //        CurrentBlock = Instantiate(Block_Garo, gameObject.transform.position,
        //              Quaternion.identity) as GameObject;
        //        CurrentBlock.transform.parent = gameObject.transform;
        //        CurrentBlock.GetComponent<Rigidbody2D>().isKinematic = true;
        //        break;
        //    case BlockType.Square:
        //        CurrentBlock = Instantiate(Block_Square, gameObject.transform.position,
        //            Quaternion.identity) as GameObject;
        //        CurrentBlock.transform.parent = gameObject.transform;
        //        CurrentBlock.GetComponent<Rigidbody2D>().isKinematic = true;
        //        break;
        //    case BlockType.Cross:
        //        CurrentBlock = Instantiate(Block_Cross, gameObject.transform.position,
        //            Quaternion.identity) as GameObject;
        //        CurrentBlock.transform.parent = gameObject.transform;
        //        CurrentBlock.GetComponent<Rigidbody2D>().isKinematic = true;
        //        break;
        //}
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.position = SponePoint.transform.position;
        if (MakeBlock)
        {
            CreateAsList();
            MakeBlock = false;
        }
    }
}
