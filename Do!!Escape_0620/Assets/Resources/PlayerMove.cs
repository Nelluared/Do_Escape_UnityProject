using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    GameBase GB;
    Animator PlayerAnim;

    Transform groundCheck;
    enum InputState
    {
        Up=0,
        Down,
        Left,
        Right,
        Jump,
        Active,
        Rolling,//이건진짜 추가하는 지 모르겠지만 일단 넣어둠
        _HowManyState
    }

    public float PlayerSpeed;//플레이어 기본속도
    public float PlayerMaxSpeed;//최대 속도
    public float PlayerAirSpeed;//플레이어 활공시 속도
    public float JumpPower;//점프시가하는힘
    Vector2 PlayerInputVelocity;//플레이어가 입력받은 움직임 값
    bool IsGround = false;
    bool JumpActive = false;
    float JumpDelayTimer = 0;//점프시 주는 딜레이 계산용
	// Use this for initialization
	void Start () {
        GB = GameObject.Find("GameBase").GetComponent<GameBase>();
        groundCheck = transform.Find("GroundChecker");
        PlayerAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if (GB.PlayerEnableControl)
            PlayerControl();
        PlayerTranslate();
    }
    void PlayerControl()//원래는 컨트롤만 받아 옮겨야 하는데 따로 거르는 처리가 불필요해 보여 그냥 운동처리도 겸임
    {
       
        PlayerInputVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (IsPressKey(InputState.Left))
        {
            if (IsGround)
                PlayerInputVelocity.x = -PlayerSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.
            else
                PlayerInputVelocity.x = -PlayerAirSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.
           

        }
        else if (IsPressKey(InputState.Right))
        {
            if (IsGround)
                PlayerInputVelocity.x = +PlayerSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.
            else
                PlayerInputVelocity.x = +PlayerAirSpeed;//어차피 벨로시티값을 바꾸기 때문에 브레이크 제한 조건이 의미가 없다.
          
        }

        if (IsPressKey(InputState.Jump))
        {
            if (IsGround)//&& JumpDelayTimer==0)//어차피 공중에 떠있을텐데 필요가 있냐마는 ,,,딜레이 넣을 때 쓸만할 것
            {
                JumpActive = true;
        
            }
        }
    }
    void PlayerTranslate()
    {
        IsGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
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
        Vector3 theScale = transform.localScale;
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x >= 0 && PlayerInputVelocity.x < 0)//방향을 바꾸면 스프라이트 뒤집기

        {

            theScale.x = -Mathf.Abs(theScale.x);
            transform.localScale = theScale;
        }
        else if (gameObject.GetComponent<Rigidbody2D>().velocity.x <= 0 && PlayerInputVelocity.x > 0)
        {
            theScale.x = Mathf.Abs(theScale.x);
            transform.localScale = theScale;
        }
       
        gameObject.GetComponent<Rigidbody2D>().velocity = PlayerInputVelocity;
  
        PlayerAnim.SetInteger("walk", (int)Mathf.Abs(PlayerInputVelocity.x));
     

        if (JumpActive)
        {
            JumpActive = false;//점프처리 완료
            JumpDelayTimer = 10;//일단 10으로..
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpPower));
        }
       
        
    }
   
    bool IsPressKey(InputState key)
    {
        bool decide = false;
        switch (key)
        { 
            case InputState.Up:
                decide = (Input.GetAxis("Vertical") > 0);
                break;
            case InputState.Down:
                decide = (Input.GetAxis("Vertical") < 0);
                break;
            case InputState.Left:
                decide = (Input.GetAxis("Horizontal") < 0);
                break;
            case InputState.Right:
                decide = (Input.GetAxis("Horizontal") > 0);
                break;
            case InputState.Jump:
                decide = (Input.GetButtonDown("Fire1"));
                //decide = (Input.GetButtonDown("Jump"));
                break;
            case InputState.Active:
                decide = (Input.GetButtonDown("Fire1"));
                break;
            case InputState.Rolling:
               // decide = (Input.GetButtonDown("Jump"));
                break;
        }
        return decide;

    }
}
