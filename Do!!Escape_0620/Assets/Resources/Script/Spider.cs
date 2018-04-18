using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
    bool Shotted=false;//거미줄을 쐈는지
    public Vector2 ShotDir;//발사 방향
    public GameObject BridgePoint;//연결지점이 되는 끝 포인트
    public GameObject BridgeTile;//거미와 연결지점 사이의 채울것들
    public GameObject ShotPoint;//발사 포인트
    GameObject WebStartPoint;//발사된 거미줄끝점
    bool Web_Sp_isGround;//거미줄 끝점이 착지했는지
    public float Power;//발사파워
    public float Interval_Web;//거미줄 타일 간의 간격
    public Transform DlPoint;//발사 포인트
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator WebCreating()
    {

        DialogueBoxPlayer.DialogueBoxPlayerIns().
             CreateDialogue("Dl_SpiderShot", DlPoint.position, 3.0f, 1);
        while (WebStartPoint.GetComponent<WebBullet>().BlockCollised != true)
        {
            
             yield return null;
        }
        Debug.Log("Start!!");
        RaycastHit2D HitPoint= Physics2D.Linecast(ShotPoint.transform.position, WebStartPoint.transform.position, 1 << LayerMask.NameToLayer("Ground"));
       
        //Vector3 tempdir = WebStartPoint.transform.position - gameObject.transform.position;
        Vector3 TargetPoint = new Vector3(HitPoint.point.x, HitPoint.point.y, gameObject.transform.position.z);
        Instantiate(BridgeTile, TargetPoint, transform.rotation);
        Vector3 tempdir = TargetPoint
            - gameObject.transform.position;
        tempdir.Normalize();
        Vector3 dirWb_Wsp;
        GameObject WebmiddlePoint;
        Vector3 Pos_WebmiddlePoint;
        yield return null;
        for (int number = 1; true; number++)
        {
            Debug.Log("bridge");
            Pos_WebmiddlePoint = gameObject.transform.position + new Vector3(tempdir.x * Interval_Web * number, tempdir.y * Interval_Web * number, 0);
            WebmiddlePoint = Instantiate(BridgeTile, Pos_WebmiddlePoint, transform.rotation) as GameObject;
            WebmiddlePoint.GetComponent<Rigidbody2D>().isKinematic = true;
            //dirWb_Wsp = WebStartPoint.transform.position-WebmiddlePoint.transform.position;
            dirWb_Wsp = TargetPoint - WebmiddlePoint.transform.position;
            if ((tempdir.x > 0 && dirWb_Wsp.x < 0) || (tempdir.y > 0 && dirWb_Wsp.y < 0) ||
                (tempdir.x < 0 && dirWb_Wsp.x > 0) || (tempdir.y < 0 && dirWb_Wsp.y > 0))
                break;
            yield return null;
        }
        Debug.Log("End");
        StopCoroutine(WebCreating());
        yield return null;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {


        if (coll.transform.tag == "Player" && !Shotted)
        {
            SoundsGenerator.SoundsGeneratorIns().PlaySound("WebShot", new Vector3(transform.position.x, transform.position.y, 0));
            Shotted = true;
            WebStartPoint= Instantiate(BridgePoint, ShotPoint.transform.position, transform.rotation)as GameObject;
            ShotDir = ShotPoint.transform.position - gameObject.transform.position;
            WebStartPoint.GetComponent<WebBullet>().ShooterSpider = gameObject;
            Web_Sp_isGround = WebStartPoint.GetComponent<WebBullet>().BlockCollised;
            WebStartPoint.GetComponent<Rigidbody2D>().AddForce(ShotDir.normalized * Power);
         
            StartCoroutine(WebCreating());
        }
    }
}
