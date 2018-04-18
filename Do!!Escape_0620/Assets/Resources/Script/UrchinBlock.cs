using UnityEngine;
using System.Collections;

public class UrchinBlock : MonoBehaviour {
    public bool Change=false;
    public GameObject ChangedUrchin;
    public GameObject Target;
    // Use this for initialization
    void Start () {
	
	}   
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player" && !Change)
        {
            Change = true;
            GameObject TrunUrchin = Instantiate(ChangedUrchin, transform.position, transform.rotation) as GameObject;

            TrunUrchin.transform.parent = coll.transform;
            coll.GetComponent<Player>().PlayerSpeed *= 0.7f;
             //TrunUrchin.GetComponent<Rigidbody2D>().isKinematic = true;

             Destroy(gameObject);
        }
    }
}
