using UnityEngine;
using System.Collections;

public class SnailRolling : MonoBehaviour {
    Rigidbody2D myRigid;
    public Transform DLPoint;
    DialogueEffect MalPung;//말풍선 살아있나 없나
	// Use this for initialization
	void Start () {
        myRigid = gameObject.GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        if (myRigid.isKinematic == true)//언제나 움직인다.
            myRigid.isKinematic = false;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player"&& !MalPung)
        {
            MalPung= DialogueBoxPlayer.DialogueBoxPlayerIns().
                 CreateDialogue("Dl_SnailRolling", DLPoint.position, 3.0f, 1);
        }
    }
}
