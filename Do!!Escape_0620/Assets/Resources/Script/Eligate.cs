using UnityEngine;
using System.Collections;

public class Eligate : MonoBehaviour {
     bool Bited = false;
     bool Released = false;
    public Animator EligateAni;
    public Transform DlPoint;//말풍선 띄울 위치
    // Use this for initialization
    public void ReleaseMe() { Released= true; }
    IEnumerator ReleaseDelay()
    {
        
        yield return new WaitForSeconds(3.4f);
        Released = false;
        EligateAni.SetBool("Crab", false);
        StopCoroutine(ReleaseDelay());
        yield return null;
    }
	void Start () {
        EligateAni.SetBool("CharaStart", true);//따로 스타트에 벌려주고 싶으면 조건 추가하기

    }
	
	// Update is called once per frame
	void Update () {      

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player" && !Released&& !Bited)
        {
            SoundsGenerator.SoundsGeneratorIns().PlaySound("EligateBite", new Vector3(transform.position.x, transform.position.y, 0));
            coll.GetComponent<Player>().ChangeStatus("Bite");
            coll.GetComponent<Rigidbody2D>().isKinematic = true;
            Bited = true;
            coll.GetComponent<Player>().BiteTarget = gameObject;
            Debug.Log("Ani Sesst");
            EligateAni.SetBool("Crab", true);
            Debug.Log("Ani Set");
            DialogueBoxPlayer.DialogueBoxPlayerIns().
                CreateDialogue("Dl_EligateClose", DlPoint.position, 3.0f, 1);
        }
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (Bited&&coll.transform.tag == "Player" && Released)
        {
            
            coll.GetComponent<Player>().ChangeStatus("Normal");
            coll.GetComponent<Rigidbody2D>().isKinematic = false;
            DialogueBoxPlayer.DialogueBoxPlayerIns().
               CreateDialogue("Dl_EligateOpen", DlPoint.position, 3.0f, 1);
            StartCoroutine( ReleaseDelay());
            Bited = false;
        }
    }
}
