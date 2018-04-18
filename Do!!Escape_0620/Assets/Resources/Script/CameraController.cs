using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CameraController : MonoBehaviour
{
    public GameBase GB;
    public GameObject playerObject = null;
    public GameObject Crane;
    public GameObject Player;
    public float cameraTrackingSpeed = 0.2f;
    public float CameraSize_Player;
    public float CameraSize_Crane;
    public Vector2 DefaultCameraColliderSize = new Vector2( (float)15.06, (float)8.7);
    private Vector3 lastTargetPosition = Vector3.zero;
    private Vector3 currTargetPosition = Vector3.zero;
    private float currLerpDistance = 0.0f;
    bool IsCollision;
    bool TargetCorrect;
    public float allowrange = 3.0f;
    public float allowScrol = 3.0f;
    private Vector3 currScrolVelocity = Vector3.zero;
    public float DragSpeed = 0;
    Drag DragMover;
    void Start()
    {
        GB = GameObject.Find("GameBase").GetComponent<GameBase>();
        // 이상하게 움직이는 일이 없도록 초기 카메라 위치를 지정한다.
        Vector3 playerPos = playerObject.transform.position;
        Vector3 cameraPos = transform.position;
        Vector3 startTargPos = playerPos;

        // Z값을 똑같이 설정해서 이 축으로는 움직이지 않도록 한다.
        startTargPos.z = cameraPos.z;
        lastTargetPosition = startTargPos;
        currTargetPosition = startTargPos;
        currLerpDistance = 1.0f;
        IsCollision = false;
        TargetCorrect = false;
        DragMover = new Drag();
        DragMover.dragSpeed = DragSpeed;
    }

    void ScrollVerticalControl()
    {
        //if (CrossPlatformInputManager.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") > 0)
        //{
        //    currScrolVelocity.y += allowScrol*Time.deltaTime;

        //}
        //else if (CrossPlatformInputManager.GetAxis("Vertical")< 0 || Input.GetAxis("Vertical") < 0)
        //{
        //    currScrolVelocity.y -= allowScrol * Time.deltaTime;
        //}
        //else { currScrolVelocity.y = 0; }    

        Vector3 tempDrag = DragMover.GetDragScreenMove();
            if (tempDrag != Vector3.zero&& !GameBase.UIUsing)
                currScrolVelocity += tempDrag;
            else
                currScrolVelocity = Vector3.zero;
        
    }

    void LateUpdate()
    {

        if (GB.PlayerEnableControl)
        {
            //playerObject = GameObject.FindGameObjectWithTag("Player");
            playerObject = Player;
            float lastorthosize = gameObject.GetComponent<Camera>().orthographicSize;
            gameObject.GetComponent<Camera>().orthographicSize = CameraSize_Player;

            gameObject.GetComponent<BoxCollider2D>().size = gameObject.GetComponent<BoxCollider2D>().size * (CameraSize_Player / lastorthosize);
            //gameObject.GetComponent<BoxCollider2D>().size = DefaultCameraColliderSize *(float) 0.5;
            ScrollVerticalControl();
        }
        else if (GB.CraneEnableControl)
        {
            playerObject = Crane;
            float lastorthosize = gameObject.GetComponent<Camera>().orthographicSize;
            gameObject.GetComponent<Camera>().orthographicSize = CameraSize_Crane;

            gameObject.GetComponent<BoxCollider2D>().size = gameObject.GetComponent<BoxCollider2D>().size * (CameraSize_Crane / lastorthosize);
            ScrollVerticalControl();
        }
        // 현재 상태를 기반으로 업데이트함
        trackPlayer();

        // 현재의 타겟 위치를 향해 계속 이동한다
        currLerpDistance += cameraTrackingSpeed;

        //if(!IsCollision|| !TargetCorrect)
        // if (!IsCollision)
        //Vector3 Velocity = Vector3.Lerp(lastTargetPosition, currTargetPosition, currLerpDistance) - transform.position;
        //transform.position = Vector3.Lerp(lastTargetPosition, currTargetPosition, currLerpDistance);
        //transform.Translate(Velocity.x, Velocity.y, 0);
        GetComponent<Rigidbody2D>().MovePosition(Vector3.Lerp(lastTargetPosition, currTargetPosition, currLerpDistance)+ currScrolVelocity);

    }

    // 엔진의 매 사이클마다 현재 상태를 업데이트한다


    void trackPlayer()
    {
        // 현재의 카메라와 플레이어의 월드 좌표를 얻어서 저장해둔다.
        Vector3 currCamPos = transform.position;
        Vector3 currPlayerPos = playerObject.transform.position;
        TargetCorrect = false;
        //if ((currCamPos.x == currPlayerPos.x && currCamPos.y == currPlayerPos.y))
        if (Vector3.Distance(currCamPos, currPlayerPos)<allowrange)
        {
            TargetCorrect = true;
                // 위치가 동일할 때에는 카메라에게 움직이지 말고 멈추도록 알려준다.
                currLerpDistance = 1.0f;
                lastTargetPosition = currCamPos;
                currTargetPosition = currCamPos;
            
            return;
        }

        // lerp할 이동 거리를 초기화한다.
        currLerpDistance = 0.0f;

        // lerp를 시작할 기준점이 될 현재 타겟 위치를 지정한다.
        lastTargetPosition = currCamPos;

        // 새로운 타겟 위치를 지정한다.
        currTargetPosition = currPlayerPos;

        // 타겟의 Z값을 현재의 값과 동일하게 변경한다.
        //Z값이 바뀌는 걸 원하지 않는다.
        currTargetPosition.z = currCamPos.z;
    }

    void stopTrackingPlayer()
    {
        // 타겟 위치를 카메라의 현재 위치로 지정하여 움직임을 멈춘다.
        Vector3 currCamPos = transform.position;
        currTargetPosition = currCamPos;
        lastTargetPosition = currCamPos;

        // lerp될 거리를 1.0으로 설정하여 lerp가 끝났음을 알려 준다.
        // 타겟 위치를 카메라의 현재 위치로 지정하였기 때문에,
        // 카메라는 단지 현재의 위치로 lerp한 후 거기서 멈추게 된다.
        currLerpDistance = 1.0f;
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
     
        IsCollision = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
       
        IsCollision = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        IsCollision = true;
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        
        IsCollision = false;
    }
    void UIScale()
    {
      
    }
}
