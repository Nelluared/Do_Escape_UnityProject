using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    GameBase GB;
    Animator PlayerAnim;

    Transform groundCheck;
    public Player_Trigger groundCheck_trigger;
    enum InputState
    {
        Up = 0,
        Down,
        Left,
        Right,
        Jump,
        Active,
        Rolling,//이건진짜 추가하는 지 모르겠지만 일단 넣어둠
        _HowManyState
    }
    enum CharState
    {
        Normal=0,
        Bited,//악어에게 물렸을때
        _HowManyState
    }
    public float PlayerSpeed;//플레이어 기본속도
    public float PlayerMaxSpeed;//최대 속도
    public float PlayerAirSpeed;//플레이어 활공시 속도
    public float JumpPower;//점프시가하는힘
    public GameObject BiteTarget;//물림 상태를 유발하는 대상,현재는 사실상 악어밖에 없으므로 클래스까지 답정너.
    Vector2 PlayerInputVelocity;//플레이어가 입력받은 움직임 값
 
    bool IsGround = false;
    bool JumpActive = false;
    float JumpDelayTimer = 0;//점프시 주는 딜레이 계산용
                             // Use this for initialization
    int ReleaseBiteTap = 0;//물림에서 벗어나기 위한 몸부림 횟수계산
                             
    CharState PlayerStatus;
   public void ChangeStatus(string StatusName)
    {
        switch (StatusName)
        {
            case "Bite":
                PlayerStatus = CharState.Bited;
                break;
            case "Normal":
                PlayerStatus = CharState.Normal;
                break;
        }
    }
    void Start()
    {
        GB = GameObject.Find("GameBase").GetComponent<GameBase>();
        groundCheck = transform.Find("GroundChecker");
        PlayerAnim = gameObject.GetComponent<Animator>();
        PlayerStatus = CharState.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (GB.PlayerEnableControl)
        {           
            PlayerControl();
        }
        PlayerTranslate();
    }
    void PlayerControl()//원래는 컨트롤만 받아 옮겨야 하는데 따로 거르는 처리가 불필요해 보여 그냥 운동처리도 겸임
    {
        PlayerInputVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Vector3 theScale = transform.localScale;
        if (IsPressKey(InputState.Left))
        {
            theScale.x = -Mathf.Abs(theScale.x);
            transform.localScale = theScale;
            if (IsGround)
            {
                PlayerInputVelocity.x = -PlayerSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.
            }
            else
                PlayerInputVelocity.x = -PlayerAirSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.
            if (PlayerStatus == CharState.Bited)
                ReleaseBiteTap++;
        }
        else if (IsPressKey(InputState.Right))
        {
            theScale.x = Mathf.Abs(theScale.x);
            transform.localScale = theScale;
            if (IsGround)
                PlayerInputVelocity.x = +PlayerSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.
            else
                PlayerInputVelocity.x = +PlayerAirSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.
            if (PlayerStatus == CharState.Bited)
                ReleaseBiteTap++;
        }

        if (IsPressKey(InputState.Jump))
        {
            if (IsGround)//&& JumpDelayTimer==0)//어차피 공중에 떠있을텐데 필요가 있냐마는 ,,,딜레이 넣을 때 쓸만할 것
            {
                JumpActive = true;
            }
        }

        //카메라 앵글 위아래 움직임
       
    }
    void PlayerTranslate()
    {
      //  IsGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        IsGround = groundCheck_trigger.Grounded_trigger;
        if (IsGround)
            PlayerAnim.SetBool("jump", false);
        else
            PlayerAnim.SetBool("jump", true);

        if (JumpDelayTimer != 0)//0이 아니면 작동 개시, 0아래면 0으로 만들어 멈춘다.
        {
            JumpDelayTimer -= Time.deltaTime;
            if (JumpDelayTimer <= 0)
                JumpDelayTimer = 0;
        }       
       

        gameObject.GetComponent<Rigidbody2D>().velocity = PlayerInputVelocity;
        PlayerAnim.SetInteger("walk", (int)Mathf.Abs(PlayerInputVelocity.x));
        if (JumpActive)
        {
            SoundsGenerator.SoundsGeneratorIns().PlaySound("CharacterJump", new Vector3(transform.position.x, transform.position.y, 0));
            // groundCheck_trigger.Grounded_trigger=false;
            JumpActive = false;//점프처리 완료
            JumpDelayTimer = 10;//일단 10으로..
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpPower));
        }
        if (PlayerStatus == CharState.Bited)//물린상태라면 모든 조작으로인한 움직임이 없어진다.
        {

            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;       

            if (ReleaseBiteTap > 300)
            {
                ReleaseBiteTap = 0;
                BiteTarget.GetComponent<Eligate>().ReleaseMe();
                Debug.Log("Release ME!!");
            }

        }
    }

    bool IsPressKey(InputState key)
    {
        bool decide = false;
        switch (key)
        {
            case InputState.Up:
                decide = (CrossPlatformInputManager.GetAxis("Vertical") > 0||Input.GetAxis("Vertical")>0);
                break;
            case InputState.Down:
                decide = (CrossPlatformInputManager.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") < 0);
                break;
            case InputState.Left:
                decide = (CrossPlatformInputManager.GetAxis("Horizontal") < 0|| Input.GetAxis("Horizontal") < 0);                
                break;
            case InputState.Right:
                decide = (CrossPlatformInputManager.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") > 0);
                break;
            case InputState.Jump:
                decide = (CrossPlatformInputManager.GetButton("Charajump") || Input.GetButton("Jump"));            
                break;
            case InputState.Active:
                decide = (Input.GetButtonDown("jump"));
                break;
            case InputState.Rolling:             
                break;
        }
        return decide;
    }
}
