using UnityEngine;
using System.Collections;

public class WebBullet : MonoBehaviour {

    public bool BlockCollised = false;
    public GameObject ShooterSpider;//날 쏜  거미
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
    }
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject != ShooterSpider && (coll.transform.tag == "Block"|| coll.transform.tag == "Platform")
            && !BlockCollised)
        {
     
            BlockCollised = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
