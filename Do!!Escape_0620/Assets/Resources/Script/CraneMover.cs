using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CraneMover : MonoBehaviour
{

    GameBase GB;
    Animator CraneAnim;
    GameObject BoxCreator;
    Transform groundCheck;
    /*enum InputState
    {
        Up = 0,
        Down,
        Left,
        Right,
        Jump,
        Active,
        Rolling,//이건진짜 추가하는 지 모르겠지만 일단 넣어둠
        _HowManyState
    }*/

    public float CraneSpeed;//크레인 기본속도
    public float CraneMaxSpeed;//크레인 최대 속도    
    public float LowerHeight;
    public float HigherHeight;
    Vector2 CraneInputVelocity;//크레인이 입력받은 움직임 값
    float Angle_CraneL = 0;
    float Angle_CraneR = 0;
    public float Open_Angle;
    public float Close_Angle;
    bool CraneOperating;//크레인 떨구기 중
    bool DropActive = false;
    bool ReloadActive = false;
    float DropDelayTimer = 0;//점프시 주는 딜레이 계산용
                             // Use this for initialization
                             //에니메이션용 집게
    public GameObject CraneL;
    public GameObject CraneR;
    public GameObject Paranoma_Crane;//파라노마로 보이는 집어오는 크레인
    public GameObject Paranoma_Doll;//파라노마로 보이는 집어오는 크레인
    public float ParaCrane_DownHeight;//파라노마 내려오는 최하 높이
    public float ParaCrane_SpotRange;//크레인 중심 기준 크레인이 나타날 범위 플러스값
    public float ParaCrane_PlusY;//크레인보다 어느정도 높이에 있는지
    void Start()
    {
        GB = GameObject.Find("GameBase").GetComponent<GameBase>();
        BoxCreator = GameObject.Find("BoxGenerator");
        CraneAnim = gameObject.GetComponent<Animator>();
        Angle_CraneL = Close_Angle;
        Angle_CraneR = -Close_Angle;

        CraneOperating = false;
    }

    IEnumerator Crane_Open()
    {
        for (int i = 0; i < 8; i++)
        {

            CraneL.transform.rotation= Quaternion.Euler(0, 0,  Mathf.LerpAngle(Angle_CraneL, Open_Angle,0.125f*i));
            CraneR.transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(Angle_CraneR, -Open_Angle, 0.125f*i));           
           yield return new WaitForSeconds(0.05f);
        }
        CraneL.transform.rotation = Quaternion.Euler(CraneL.transform.rotation.x, CraneL.transform.rotation.y, Open_Angle);
        CraneR.transform.rotation = Quaternion.Euler(CraneL.transform.rotation.x, CraneL.transform.rotation.y, -Open_Angle);
        Angle_CraneL = Open_Angle;
        Angle_CraneR = -Open_Angle;
        yield return new WaitForSeconds(0.8f);

        DropActive = true;
        StopCoroutine(Crane_Open());

        yield return null;

    }

    IEnumerator Crane_Close()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        
        while (transform.position.y< HigherHeight)//1단계 크레인을 올린다.
        {

            transform.position+=new Vector3(0, Mathf.Lerp(LowerHeight, HigherHeight, 0.05f)- LowerHeight, 0 );
            yield return new WaitForSeconds(0.07f);
        }
       
        //2단계 배경에서 크레인이 집어온다.

        Paranoma_Crane.transform.Translate(new Vector3(gameObject.transform.position.x + Random.Range(-ParaCrane_SpotRange, ParaCrane_SpotRange), gameObject.transform.position.y, gameObject.transform.position.z) 
            - Paranoma_Crane.transform.position);
        int i = 1;
        while (i<20)  //2-1파라크레인이 내려온다
        {
            //Paranoma_Crane.transform.Translate(-new Vector3(0, gameObject.transform.position.y - Mathf.Lerp(gameObject.transform.position.y, ParaCrane_DownHeight, 0.05f), 0));
            Paranoma_Crane.transform.position = new Vector3(Paranoma_Crane.transform.position.x, Mathf.Lerp(gameObject.transform.position.y, ParaCrane_DownHeight, (i++)*0.05f), Paranoma_Crane.transform.position.z);
           yield return new WaitForSeconds(0.07f);
        }//2-2파라크레인이 뭔가를 집는다.

        Paranoma_Crane.GetComponent<Animator>().SetBool("Open", false);
        Paranoma_Doll.SetActive(true);
         i = 1;
        Debug.Log("dadsasdag");
        while (i < 20)//2-3파라크레인이 올라온다.
        {
           
            //Paranoma_Crane.transform.position = new Vector3(Paranoma_Crane.transform.position.x, Mathf.Lerp(ParaCrane_DownHeight, gameObject.transform.position.y, (i++) * 0.05f) - ParaCrane_DownHeight, Paranoma_Crane.transform.position.z);
            Paranoma_Crane.transform.position = new Vector3(Paranoma_Crane.transform.position.x, Mathf.Lerp(ParaCrane_DownHeight,gameObject.transform.position.y+ ParaCrane_PlusY,  (i++) * 0.05f), Paranoma_Crane.transform.position.z);
            yield return new WaitForSeconds(0.07f);
        }
        Paranoma_Crane.GetComponent<Animator>().SetBool("Open", true);
        Paranoma_Doll.SetActive(false);
        BoxCreator.GetComponent<BoxGenerator>().MakeBlock = true;//안보이는 3단계 블록 생성
      
        CraneL.transform.rotation = Quaternion.Euler(CraneL.transform.rotation.x, CraneL.transform.rotation.y, Close_Angle);
        CraneR.transform.rotation = Quaternion.Euler(CraneL.transform.rotation.x, CraneL.transform.rotation.y, -Close_Angle);
        Angle_CraneL = Close_Angle;
        Angle_CraneR = -Close_Angle;
      
        while (transform.position.y > LowerHeight)  //4단계 내려온다.
        {

            transform.position -= new Vector3(0, HigherHeight - Mathf.Lerp(HigherHeight, LowerHeight, 0.05f ), 0);
            yield return new WaitForSeconds(0.07f);
        }

        //transform.position = new Vector3(transform.position.x, LowerHeight, transform.position.z);
        yield return new WaitForSeconds(0.07f);

        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        ReloadActive = true;
        StopCoroutine(Crane_Close());

        yield return null;
    }
    // Update is called once per frame
    void Update()
    {

        if (GB.CraneEnableControl)
            CraneControl();
        CraneTranslate();


    }

    
    void CraneControl()//원래는 컨트롤만 받아 옮겨야 하는데 따로 거르는 처리가 불필요해 보여 그냥 운동처리도 겸임
    {
        CraneInputVelocity = Vector2.zero;
        CraneInputVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
     
        bool jumping = (CrossPlatformInputManager.GetButton("CraneDrop"));
       // Debug.Log(jumping ? true : false);
        if (CrossPlatformInputManager.GetAxis("Horizontal")<0)
        {
            CraneInputVelocity.x = -CraneSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.     
        }
        else if (CrossPlatformInputManager.GetAxis("Horizontal")>0)
        {

            CraneInputVelocity.x = +CraneSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.

        }

        if (!CraneOperating&&jumping)
        {

            jumping = false;
            CraneOperating = true;
            //DropActive = true;
            //DropDelayTimer = 5;
            BoxCreator.GetComponent<BoxGenerator>().DropBlock();
            StartCoroutine(Crane_Open());//오픈 애니 실행
            //CraneAnim.SetBool("Drop", true);
        }
    }
    void CraneTranslate()
    {

        gameObject.GetComponent<Rigidbody2D>().velocity = CraneInputVelocity;
        //gameObject.transform.Translate(CraneInputVelocity*Time.deltaTime);
        if (DropActive)//애니메이션기다림
        {
            //Debug.Log("End!!");
            DropActive = false;//떨구기 처리 완료
            SoundsGenerator.SoundsGeneratorIns().PlaySound("CraneActive", new Vector3(transform.position.x, transform.position.y, 0));
            StartCoroutine(Crane_Close());
        }

        if (ReloadActive)//새로생성  완료
        {
            ReloadActive = false;
            //CraneAnim.SetBool("Drop", false);
            //CraneAnim.SetBool(1, true);
            
            CraneOperating = false;
        }


    }

    
}
